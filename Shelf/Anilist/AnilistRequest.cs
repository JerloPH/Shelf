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
        public static string AnilistURL { get; set; } = "https://graphql.anilist.co";
        public static string RedirectUrl { get; set; } = "https://anilist.co/api/v2/oauth/pin";
        private static string AniClient { get; set; } = "";
        private static string AniSecret { get; set; } = "";

        public static string MediaQuery(string media, string userName = "")
        {
            #region Query
            string userQuery = (!String.IsNullOrWhiteSpace(userName)) ? $"userName: \"{userName}\", " : "";
            return @"
            query {
            MediaListCollection (" + userQuery + @"type: " + media + @") { 
            lists {
                status
                entries
                {
                    status
                    completedAt { year month day }
                    startedAt { year month day }
                    progress
                    progressVolumes
                    score
                    notes
                    private
                    media
                    {
                        id
                        idMal
                        season
                        seasonYear
                        format
                        source
                        episodes
                        chapters
                        volumes
                        title
                        {
                            english
                            romaji
                        }
                        description
                        coverImage { medium }
                        synonyms
                    }
                }
            }
        }
        }";
            #endregion
        }
        public static bool Initialize()
        {
            try
            {
                string configFile = Path.Combine(Application.StartupPath, "Data\\anilistConfig.json");
                string content = GlobalFunc.ReadFromFile(configFile);
                var jsonConfig = JsonConvert.DeserializeObject<AnilistConfig>(content);
                AniClient = jsonConfig?.clientId;
                AniSecret = jsonConfig?.clientSecret;
            }
            catch { }
            return false;
        }
        public static string GetConfig(int index = 0)
        {
            return (index == 0) ? AniClient : AniSecret;
        }
        public static async Task<string> RequestAccessToken(string publicTkn)
        {
            string returnString = String.Empty;
            //var client = new RestClient($"https://anilist.co/api/v2/oauth/authorize?clientid={AniClient}&responsetype=token");
            var client = new RestClient(@"https://anilist.co/api/v2/oauth/token");
            var requestBody = new[]
            {
                new {
                    grant_type = "authorization_code",
                    client_id = AniClient,
                    client_secret = AniSecret,
                    redirect_uri = RedirectUrl,
                    code =  publicTkn.Replace("/", "\\/")
                }
            };

            try
            {
                var request = new RestRequest(Method.POST);
                request.AddJsonBody(JsonConvert.SerializeObject(requestBody));
                //request.AddBody(JsonConvert.SerializeObject(requestBody));
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");

                var response = await client.ExecuteAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = response.Content; // Raw content as string
                    var returnJsonObject = JObject.Parse(content);
                    returnString = (string)returnJsonObject["access_token"];
                }
                else { GlobalFunc.Alert("Not 200!"); }
                //response = null;
                //request = null;
            }
            catch (Exception ex)
            {
                returnString = String.Empty;
                MessageBox.Show(ex.ToString());
            }
            return returnString;
        }
        public static async Task<AnilistAnimeManga> RequestMediaList(string accessToken, string MEDIA = "ANIME")
        {
            AnilistAnimeManga returnObject = null;
            var client = new RestClient(AnilistURL);
            string qryString = MediaQuery(MEDIA);

            try
            {
                var request = new RestRequest("application/json", Method.POST);
                request.AddParameter("query", qryString);
                request.AddHeader("Authorization", $"Bearer {accessToken}");
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
                MessageBox.Show(ex.ToString());
            }
            return returnObject;
        }
    }
}
