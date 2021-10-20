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
using JerloPH_CSharp;

namespace Shelf.Anilist
{
    public static class AnilistRequest
    {
        public static string RedirectUrl { get; set; } = "https://anilist.co/api/v2/oauth/pin";
        // Private properties
        private static string AniClient { get; set; } = "";
        private static string AniSecret { get; set; } = "";
        private static readonly string QueryMediaList = @"query ($userID: Int, $type: MediaType) {
          MediaListCollection(userId: $userID, type: $type) {
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
                string content = FileHelper.ReadFromFile(GlobalFunc.FILE_ANILIST_CONFIG);
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
                if (FileHelper.WriteFile(GlobalFunc.FILE_ANILIST_CONFIG, jsonstring))
                    return Initialize(config);
            }
            catch (Exception ex) { Logs.Err(ex); }
            return false;
        }
        public static string GetConfig(int index = 0)
        {
            return (index == 0) ? AniClient : AniSecret;
        }
        public static async Task<string> RequestPublicToken(string authCode, bool isSaveTkn)
        {
            if (String.IsNullOrWhiteSpace(authCode))
                throw new Exception("No Authorization Code!");

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
                    if (isSaveTkn)
                        FileHelper.WriteFile(GlobalFunc.FILE_PUB_TKN, content);
                }
                else
                {
                    FileHelper.WriteFile(GlobalFunc.FILE_PUB_TKN, "");
                    throw new Exception($"Unsuccesful request! " +
                        $"Status code: {(int)response.StatusCode}," +
                        $" Response: {response.Content}");
                }
            }
            catch { throw; }
            return returnString;
        }
        public static async Task<string> RequestUserId(string pubTkn)
        {
            if (String.IsNullOrWhiteSpace(pubTkn))
                throw new Exception("No Public Token!");

            string returnString = "";
            try
            {
                var client = new RestClient("https://graphql.anilist.co");
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                var request = new RestRequest("application/json", Method.POST);
                request.Timeout = 0;
                request.AddJsonBody(new
                {
                    query = "{Viewer{id}}"
                });
                request.AddHeader("Authorization", $"Bearer {pubTkn}");
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");

                var response = await client.ExecuteAsync(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = response.Content; // Raw content as string
                    var json = JObject.Parse(content);
                    returnString = (string)json["data"]["Viewer"]["id"];
                }
            }
            catch { throw; }
            return returnString;
        }
        public static async Task<AnilistAnimeManga> RequestMediaList(string publicTkn, string userId, string MEDIA = "ANIME")
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
                    variables = new
                    {
                        userID = userId,
                        type = MEDIA
                    }
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
            catch { throw; }
            return returnObject;
        }
    }
}
