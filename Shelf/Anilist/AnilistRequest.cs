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

namespace Shelf.Anilist
{
    public static class AnilistRequest
    {
        private static string AnilistURL = @"https://graphql.anilist.co";
        public static string MediaQuery(string userId, string media)
        {
            #region Query
            return @"
            query {
            MediaListCollection (userId: " + userId + @", type: " + media + @") { 
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
        public static async Task<string> RequestUserID(string userName)
        {
            string returnObject = String.Empty;
            var client = new RestClient(AnilistURL);
            string qryString = "query { User(search: \"" + userName + "\") { id } }";

            try
            {
                var request = new RestRequest("application/json");
                request.AddParameter("query", qryString);

                var response = client.Post(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = response.Content; // Raw content as string
                    returnObject = JsonConvert.DeserializeObject<AnilistData>(content).data.user.Id;
                }
            }
            catch (Exception ex)
            {
                returnObject = String.Empty;
                MessageBox.Show(ex.ToString());
            }
            return returnObject;
        }
        public static async Task<AnilistAnimeManga> RequestMediaList(string userID, string MEDIA = "ANIME")
        {
            AnilistAnimeManga returnObject = null;
            var client = new RestClient(AnilistURL);
            string qryString = MediaQuery(userID, MEDIA);

            try
            {
                var request = new RestRequest("application/json");
                request.AddParameter("query", qryString);

                var response = client.Post(request);
                var content = response.Content; // Raw content as string
                returnObject = JsonConvert.DeserializeObject<AnilistAnimeManga>(content);
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
