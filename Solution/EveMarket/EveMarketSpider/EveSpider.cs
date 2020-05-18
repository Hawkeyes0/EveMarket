using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EveMarketEntities;
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
            // TODO: catch types
            // TODO: catch regions
            // TODO: catch orders
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
                Dictionary<int, MarketGroup> allMarketGroups = new Dictionary<int, MarketGroup>();
                List<MarketGroup> roots = new List<MarketGroup>();

                foreach (int groupId in groupIds)
                {
                    json = await Client.GetStringAsync($"markets/groups/{groupId}/?datasource=serenity&language=zh");
                    MarketGroup marketGroup = JsonConvert.DeserializeObject<MarketGroup>(json);
                    /// TODO: save to db
                    allMarketGroups[marketGroup.MarketGroupId] = marketGroup;
                }

                foreach (int groupId in groupIds)
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

                Console.WriteLine(JsonConvert.SerializeObject(roots));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return types.ToArray();
        }
    }
}