using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Shelf.Json;
using Newtonsoft.Json;
using System.Windows.Forms;
using RestSharp;
using System.Net;
using System.IO;
using Shelf.Functions;
using Newtonsoft.Json.Linq;

namespace Shelf.Anilist
{
    public static class AnilistRequest
    {
        public static string RedirectUrl { get; set; } = "https://anilist.co/api/v2/oauth/pin";
        // Private properties
        private static string AniClient { get; set; } = "";
        private static string AniSecret { get; set; } = "";
        private static readonly string QueryMediaList = @"query ($name: String, $type: MediaType) {
          MediaListCollection(userName: $name, type: $type) {
            lists {
              name
              entries {
                status
                completedAt { year month day }
                startedAt {  year month day }
                progress
                progressVolumes
                score
                notes
                private
                media {
                  id
                  idMal
                  season
                  seasonYear
                  format
                  episodes
                  chapters
                  volumes
                  title { english romaji }
                  description
                  coverImage { medium large }
                  synonyms
                }
              }
            }
          }
        }";

        public static bool Initialize(AnilistConfig jsonConfig = null)
        {
            try
            {
                string content = GlobalFunc.ReadFromFile(GlobalFunc.FILE_ANILIST_CONFIG);
                if (jsonConfig == null)
                    jsonConfig = JsonConvert.DeserializeObject<AnilistConfig>(content);

                AniClient = jsonConfig?.clientId;
                AniSecret = jsonConfig?.clientSecret;
                return (!String.IsNullOrWhiteSpace(AniClient) && !String.IsNullOrWhiteSpace(AniSecret));
            }
            catch (Exception ex) { Logs.Err(ex); }
            return false;
        }
        public static bool UpdateConfig(string client, string secret)
        {
            string jsonstring;
            var config = new AnilistConfig();
            config.clientId = client.Trim();
            config.clientSecret = secret.Trim();
            try
            {
                jsonstring = JsonConvert.SerializeObject(config, Formatting.Indented);
                if (GlobalFunc.WriteFile(GlobalFunc.FILE_ANILIST_CONFIG, jsonstring))
                    return Initialize(config);
            }
            catch (Exception ex) { Logs.Err(ex); }
            return false;
        }
        public static string GetConfig(int index = 0)
        {
            return (index == 0) ? AniClient : AniSecret;
        }
        public static async Task<string> RequestPublicToken(string authCode)
        {
            if (String.IsNullOrWhiteSpace(authCode))
                return "";

            string returnString = "";
            try
            {
                var client = new RestClient(@"https://anilist.co/api/v2/oauth/token");
                var request = new RestRequest("/", Method.POST)
                {
                    Timeout = 0,
                    RequestFormat = DataFormat.Json
                };
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                request.AddJsonBody(new
                {
                    grant_type = "authorization_code",
                    client_id = AniClient,
                    client_secret = AniSecret,
                    redirect_uri = RedirectUrl,
                    code = authCode.Replace("/", "\\/")
                });
                var response = await client.ExecuteAsync(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = response.Content; // Raw content as string
                    var returnJsonObject = JObject.Parse(content);
                    returnString = (string)returnJsonObject["access_token"];
                    GlobalFunc.WriteFile(GlobalFunc.FILE_PUB_TKN, content);
                }
                else
                {
                    GlobalFunc.WriteFile(GlobalFunc.FILE_PUB_TKN, "");
                    GlobalFunc.Alert("Unsuccessful on Fetching Public Token!");
                    returnString = "";
                }
            }
            catch (Exception ex)
            {
                returnString = "";
                GlobalFunc.Alert(ex.ToString());
            }
            return returnString;
        }
        public static async Task<AnilistAnimeManga> RequestMediaList(string publicTkn, string userName, string MEDIA = "ANIME")
        {
            AnilistAnimeManga returnObject = null;
            try
            {
                var client = new RestClient("https://graphql.anilist.co");
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                var request = new RestRequest("application/json", Method.POST);
                request.Timeout = 0;
                request.AddJsonBody(new
                {
                    query = QueryMediaList,
                    variables = "{ \"name\": \""+userName+"\", \"type\": \""+MEDIA+"\"}"
                });
                request.AddHeader("Authorization", $"Bearer {publicTkn}");
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");

                var response = await client.ExecuteAsync(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = response.Content; // Raw content as string
                    returnObject = JsonConvert.DeserializeObject<AnilistAnimeManga>(content);
                }
            }
            catch (Exception ex)
            {
                returnObject = null;
                GlobalFunc.Alert(ex.ToString());
            }
            return returnObject;
        }
    }
}
