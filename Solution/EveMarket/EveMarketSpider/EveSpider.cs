using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EveMarketEntities;
using EveMarketSpider.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EveMarketSpider
{
    internal class EveSpider
    {
        private HttpClient Client { get; }

        public EveSpider()
        {
            string domain = File.ReadAllText("domain.cfg");
            Client = new HttpClient();
            Client.BaseAddress = new Uri(domain);
        }

        internal async Task CatchDataAsync()
        {
            if (File.Exists("mg.etag"))
            {
                Client.DefaultRequestHeaders.IfNoneMatch.Clear();
                Client.DefaultRequestHeaders.IfNoneMatch.Add(new System.Net.Http.Headers.EntityTagHeaderValue(File.ReadAllText("mg.etag")));
            }
            int[] types = await CatchMarketGroupsAsync();

            if (File.Exists("ty.etag"))
            {
                Client.DefaultRequestHeaders.IfNoneMatch.Clear();
                //Client.DefaultRequestHeaders.IfNoneMatch.Add(new System.Net.Http.Headers.EntityTagHeaderValue(File.ReadAllText("ty.etag")));
            }
            await CatchTypesAsync(types);
            // TODO: catch regions
            // TODO: catch orders
        }

        private async Task CatchTypesAsync(int[] types)
        {
            try
            {
                using (EveContext context = new EveContext())
                {
                    if (types.Length == 0)
                    {
                        types = context.Types.Select(t => t.TypeId).ToArray();
                    }
                    int i = 0;
                    foreach (int tid in types)
                    {
                        string json;
                        if (tid == types[0])
                        {
                            var resp = await Client.GetAsync($"universe/types/{tid}/?datasource=serenity&language=zh");
                            if (resp.StatusCode == HttpStatusCode.NotModified)
                            {
                                Console.WriteLine("No data modified.");
                                return;
                            }
                            await File.WriteAllTextAsync("ty.etag", resp.Headers.ETag.Tag);
                            json = await resp.Content.ReadAsStringAsync();
                        }
                        else
                        {
                            json = await Client.GetStringAsync($"universe/types/{tid}/?datasource=serenity&language=zh");
                        }
                        EveMarketEntities.Type t = JsonConvert.DeserializeObject<EveMarketEntities.Type>(json);

                        if (context.Types.Any(t => t.TypeId == tid))
                        {
                            await context.AddOrUpdateAsync(t);
                            t.DogmaAttributes?.ForEach(e => { e.TypeId = t.TypeId; context.AddOrUpdateAsync(e).Wait(); });
                            t.DogmaEffects?.ForEach(e => { e.TypeId = t.TypeId; context.AddOrUpdateAsync(e).Wait(); });
                        }
                        else
                        {
                            context.Add(t);
                        }

                        if (++i % 10000 == 0)
                        {
                            await context.SaveChangesAsync();
                        }
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async Task<int[]> CatchMarketGroupsAsync()
        {
            List<int> types = new List<int>();
            try
            {
                var resp = await Client.GetAsync("markets/groups/?datasource=serenity&language=zh");
                if (resp.StatusCode == HttpStatusCode.NotModified)
                {
                    Console.WriteLine("No data modified.");
                    return new int[0];
                }
                var json = await resp.Content.ReadAsStringAsync();
                await File.WriteAllTextAsync("mg.etag", resp.Headers.ETag.Tag);
                Console.WriteLine($"market group ids: {json}");
                int[] groupIds = JsonConvert.DeserializeObject<int[]>(json);
                //Dictionary<int, MarketGroup> allMarketGroups = new Dictionary<int, MarketGroup>();
                List<MarketGroup> roots = new List<MarketGroup>();

                using (EveContext context = new EveContext())
                {
                    int i = 0;
                    foreach (int groupId in groupIds)
                    {
                        json = await Client.GetStringAsync($"markets/groups/{groupId}/?datasource=serenity&language=zh");
                        MarketGroup marketGroup = JsonConvert.DeserializeObject<MarketGroup>(json);
                        await context.AddOrUpdateAsync(marketGroup);
                        //allMarketGroups[marketGroup.MarketGroupId] = marketGroup;
                        if (marketGroup.Types != null)
                            types.AddRange(marketGroup.Types);
                        if (++i % 10000 == 0)
                            await context.SaveChangesAsync();
                    }
                    await context.SaveChangesAsync();
                }
                /*foreach (int groupId in groupIds)
                {
                    var marketGroup = allMarketGroups[groupId];
                    types.AddRange(marketGroup.Types);
                    if (marketGroup.ParentGroupId == 0)
                    {
                        roots.Add(marketGroup);
                    }
                    else
                    {
                        allMarketGroups[marketGroup.ParentGroupId].Children.Add(marketGroup);
                    }
                }

                Console.WriteLine(JsonConvert.SerializeObject(roots));*/
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine(JsonConvert.SerializeObject(types));
            return types.ToArray();
        }
    }
}