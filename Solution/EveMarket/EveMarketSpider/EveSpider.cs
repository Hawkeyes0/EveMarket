using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using EveMarketEntities;
using EveMarketSpider.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EveMarketSpider
{
    internal delegate Task SeekerAsync(int id);

    internal class EveSpider
    {
        private EveContext db = new EveContext();

        private object dblocker = new object();

        private string Domain { get; }

        private static Semaphore semaphore = new Semaphore(10, 10);

        private static Queue<HttpClient> clients = new Queue<HttpClient>();

        private static Queue<(int id, SeekerAsync seeker)> Queue { get; } = new Queue<(int id, SeekerAsync seeker)>();

        private static object queueLocker = new object();

        public EveSpider()
        {
            Domain = File.ReadAllText("domain.cfg");
        }

        internal void CatchData()
        {
            Queue.Enqueue((0, CatchMarketGroupsAsync));
            Queue.Enqueue((0, CatchRegionsAsync));

            // TODO: catch orders

            List<Task> tasks = new List<Task>();
            while (tasks.Count < 10)
            {
                tasks.Add(Task.Run(async () =>
                {
                    int fails = 0;
                    (int id, SeekerAsync seeker) item;
                    while (fails < 10)
                    {
                        try
                        {
                            lock (queueLocker) item = Queue.Dequeue();
                            await item.seeker(item.id);
                            fails = 0;
                        }
                        catch (InvalidOperationException)
                        {
                            fails++;
                            Thread.Sleep(500);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
        }

        private async Task CatchRegionsAsync(int _)
        {
            int[] regionIds = await GetObjectAsync<int[]>("rg.etag", "universe/regions/?datasource=serenity");
            if (regionIds == null) return;
            foreach (int regionId in regionIds)
            {
                lock (queueLocker) Queue.Enqueue((regionId, CatchRegionAsync));
            }
        }

        private async Task CatchRegionAsync(int id)
        {
            Region obj = await GetObjectAsync<Region>($"rg\\{id}.etag", $"universe/regions/{id}/?datasource=serenity&language=zh");
            if (obj == null) return;
            lock (dblocker)
            {
                db.AddOrUpdateAsync(obj).Wait();
                db.SaveChanges();
            }
            if (obj.Constellations != null && obj.Constellations.Length > 0)
            {
                foreach (int constellationId in obj.Constellations)
                {
                    lock (queueLocker) Queue.Enqueue((constellationId, CatchConstellationAsync));
                }
            }
        }

        private async Task CatchConstellationAsync(int id)
        {
            Constellation obj = await GetObjectAsync<Constellation>($"cl\\{id}.etag", $"universe/constellations/{id}/?datasource=serenity&language=zh");
            if (obj == null) return;
            lock (dblocker)
            {
                db.AddOrUpdateAsync(obj).Wait();
                db.SaveChanges();
            }
            if (obj.Systems != null && obj.Systems.Length > 0)
            {
                foreach (int systemId in obj.Systems)
                {
                    lock (queueLocker) Queue.Enqueue((systemId, CatchSystemAsync));
                }
            }
        }

        private async Task CatchSystemAsync(int id)
        {
            EveMarketEntities.System obj = await GetObjectAsync<EveMarketEntities.System>($"sy\\{id}.etag", $"universe/systems/{id}/?datasource=serenity&language=zh");
            if (obj == null) return;
            lock (dblocker)
            {
                db.AddOrUpdateAsync(obj).Wait();
                db.SaveChanges();
            }
            if (obj.Stations != null && obj.Stations.Length > 0)
            {
                foreach (int systemId in obj.Stations)
                {
                    lock (queueLocker) Queue.Enqueue((systemId, CatchStationAsync));
                }
            }
        }

        private async Task CatchStationAsync(int id)
        {
            Station obj = await GetObjectAsync<Station>($"st\\{id}.etag", $"universe/systems/{id}/?datasource=serenity&language=zh");
            if (obj == null) return;
            lock (dblocker)
            {
                db.AddOrUpdateAsync(obj).Wait();
                db.SaveChanges();
            }
            if (obj.TypeId > 0)
            {
                lock (queueLocker) Queue.Enqueue((obj.TypeId, CatchTypeAsync));
            }
        }

        private async Task CatchTypeAsync(int id)
        {
            EveMarketEntities.Type t = await GetObjectAsync<EveMarketEntities.Type>($"ty\\{id}.etag", $"universe/types/{id}/?datasource=serenity&language=zh");
            if (t == null) return;
            lock (dblocker)
            {
                db.AddOrUpdateAsync(t).Wait();
                t.DogmaAttributes?.ForEach(e => { e.TypeId = t.TypeId; db.AddOrUpdateAsync(e).Wait(); });
                t.DogmaEffects?.ForEach(e => { e.TypeId = t.TypeId; db.AddOrUpdateAsync(e).Wait(); });
                db.SaveChanges();
            }
        }

        private async Task CatchMarketGroupsAsync(int _)
        {
            try
            {
                int[] groupIds = await GetObjectAsync<int[]>("mg.etag", "markets/groups/?datasource=serenity&language=zh");
                if (groupIds == null) return;
                foreach (int groupId in groupIds)
                {
                    lock (queueLocker) Queue.Enqueue((groupId, CatchMarketGroupAsync));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async Task CatchMarketGroupAsync(int id)
        {
            MarketGroup marketGroup = await GetObjectAsync<MarketGroup>($"mg\\{id}.etag", $"markets/groups/{id}/?datasource=serenity&language=zh");
            if (marketGroup == null) return;
            lock (dblocker)
            {
                db.AddOrUpdateAsync(marketGroup).Wait();
                db.SaveChanges();
            }
            if (marketGroup.Types != null && marketGroup.Types.Length > 0)
            {
                foreach (int tid in marketGroup.Types)
                    lock (queueLocker) Queue.Enqueue((tid, CatchTypeAsync));
            }
        }

        private async Task<T> GetObjectAsync<T>(string tagFile, string url)
        {
            var client = GetHttpClient(tagFile);
            try
            {
                var resp = await client.GetAsync(url);

                if (resp.StatusCode == HttpStatusCode.NotModified)
                {
                    return default(T);
                }
                await SaveEtagAsync(tagFile, resp.Headers.ETag.Tag);

                return JsonConvert.DeserializeObject<T>(await resp.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(url);
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                ReleaseHttpClient(client);
            }
        }

        private async Task SaveEtagAsync(string etagName, string tag)
        {
            string path = $"..\\etags\\{etagName}";
            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            await File.WriteAllTextAsync(path, tag);
        }

        private HttpClient GetHttpClient(string etagName = null)
        {
            semaphore.WaitOne();
            HttpClient client;
            try
            {
                lock (clients) client = clients.Dequeue();
            }
            catch (InvalidOperationException)
            {
                client = new HttpClient
                {
                    BaseAddress = new Uri(Domain)
                };
            }
            string path = $"..\\etags\\{etagName}";
            client.DefaultRequestHeaders.IfNoneMatch.Clear();
            if (!string.IsNullOrEmpty(etagName) && File.Exists(path))
            {
                client.DefaultRequestHeaders.IfNoneMatch.Add(new EntityTagHeaderValue(File.ReadAllText(path)));
            }
            return client;
        }

        private void ReleaseHttpClient(HttpClient client)
        {
            lock (clients) clients.Enqueue(client);
            semaphore.Release();
        }
    }
}