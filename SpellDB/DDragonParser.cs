using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;


namespace SpellDB
{
    public class DDragonParser
    {
        private const string VersionRequestUrl = "https://ddragon.leagueoflegends.com/api/versions.json";
        private static string ChampionRequestUrl
        {
            get { return string.Format("http://ddragon.leagueoflegends.com/cdn/{0}/data/en_US/championFull.json", GetLatestLeaguePatchVersion()); }
        }
        public static void ParseAndBuildSpellDB()
        {
            List<string> SpellDB = new List<string>();

            Console.WriteLine("Parsing DDragoon Data...");
            JObject ParsedData = ParseChampionData();
            Console.WriteLine("Sucessfully Parsed DDragon Data");

            Console.WriteLine("Building SpellDB...");

            foreach (string ChampionName in ParsedData["keys"].ToObject<Dictionary<string, string>>().Values)
            {
                JToken[] SpellData = ParsedData["data"][ChampionName]["spells"].ToArray();
                Champion ChampionObject = GenerateChampionObject(SpellData);

                if(ChampionName == ParsedData["keys"].ToObject<Dictionary<string, string>>().Values.Last())
                {
                    SpellDB.Add(
                    $"    {(char)34}{ChampionName}{(char)34}:\n" +
                    $"    {JsonConvert.SerializeObject(ChampionObject)}\n"
                    );
                }
                else
                {
                    SpellDB.Add(
                    $"    {(char)34}{ChampionName}{(char)34}:\n" +
                    $"    {JsonConvert.SerializeObject(ChampionObject)},\n"
                    );
                }
            }

            using(StreamWriter SpellDBJson = new StreamWriter(File.Create(Directory.GetCurrentDirectory() + @"/SpellDB.json")))
            {
                SpellDBJson.WriteLine("{");

                foreach(string ChampionSpellData in SpellDB)
                {
                    if(ChampionSpellData == SpellDB.Last())
                    {
                        SpellDBJson.WriteLine(ChampionSpellData + "}");
                    }
                    else SpellDBJson.WriteLine(ChampionSpellData);
                }
            }

            Console.WriteLine("Successfully built SpellDB");
        }

        private static Champion GenerateChampionObject(JToken[] SpellData)
        {
            return new Champion()
            {
                Q = new Champion.QSlot()
                {
                    Name = SpellData[0]["id"].ToString(),
                    MaxRank = SpellData[0]["maxrank"].ToObject<int>(),
                    CoolDown = SpellData[0]["cooldown"].ToObject<int[]>(),
                    CoolDownBurn = SpellData[0]["cooldownBurn"].ToString(),
                    Cost = SpellData[0]["cost"].ToObject<int[]>(),
                    CostType = SpellData[0]["costType"].ToString(),
                    MaxAmmo = SpellData[0]["maxammo"].ToString(),
                    Range = SpellData[0]["range"].ToObject<int[]>(),
                    RangeBurn = SpellData[0]["rangeBurn"].ToString(),
                },

                W = new Champion.WSlot()
                {
                    Name = SpellData[1]["id"].ToString(),
                    MaxRank = SpellData[1]["maxrank"].ToObject<int>(),
                    CoolDown = SpellData[1]["cooldown"].ToObject<int[]>(),
                    CoolDownBurn = SpellData[1]["cooldownBurn"].ToString(),
                    Cost = SpellData[1]["cost"].ToObject<int[]>(),
                    CostType = SpellData[1]["costType"].ToString(),
                    MaxAmmo = SpellData[1]["maxammo"].ToString(),
                    Range = SpellData[1]["range"].ToObject<int[]>(),
                    RangeBurn = SpellData[1]["rangeBurn"].ToString(),
                },

                E = new Champion.ESlot()
                {
                    Name = SpellData[2]["id"].ToString(),
                    MaxRank = SpellData[2]["maxrank"].ToObject<int>(),
                    CoolDown = SpellData[2]["cooldown"].ToObject<int[]>(),
                    CoolDownBurn = SpellData[2]["cooldownBurn"].ToString(),
                    Cost = SpellData[2]["cost"].ToObject<int[]>(),
                    CostType = SpellData[2]["costType"].ToString(),
                    MaxAmmo = SpellData[2]["maxammo"].ToString(),
                    Range = SpellData[2]["range"].ToObject<int[]>(),
                    RangeBurn = SpellData[2]["rangeBurn"].ToString(),
                },

                R = new Champion.RSlot()
                {
                    Name = SpellData[3]["id"].ToString(),
                    MaxRank = SpellData[3]["maxrank"].ToObject<int>(),
                    CoolDown = SpellData[3]["cooldown"].ToObject<int[]>(),
                    CoolDownBurn = SpellData[3]["cooldownBurn"].ToString(),
                    Cost = SpellData[3]["cost"].ToObject<int[]>(),
                    CostType = SpellData[3]["costType"].ToString(),
                    MaxAmmo = SpellData[3]["maxammo"].ToString(),
                    Range = SpellData[3]["range"].ToObject<int[]>(),
                    RangeBurn = SpellData[3]["rangeBurn"].ToString(),
                }
            };
        }

        private static JObject ParseChampionData()
        {
            string ChampionDataString = new WebClient().DownloadString(ChampionRequestUrl);

            try
            {
                return JObject.Parse(ChampionDataString);
            }
            catch (Exception Ex)
            {
                Console.WriteLine($"Error: \n{Ex.ToString()}");
                throw new Exception("ChampionDataParseFailedException");
            }
        }

        private static string GetLatestLeaguePatchVersion()
        {
            try
            {
                return JArray.Parse(new WebClient().DownloadString(VersionRequestUrl))[0].ToString();
            }
            catch (Exception Ex)
            {
                Console.WriteLine($"Error: \n{Ex.ToString()}");
                throw new Exception("FailedRetrievePatchVersionException");
            }
        }
    }
}
