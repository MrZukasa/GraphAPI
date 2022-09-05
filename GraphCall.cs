using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Space
{
    public class GraphAPI
    {
        public static async Task<JObject> GraphCall(HttpClient client, string API, string type, string content = null)
        {
            HttpResponseMessage response;
            string responseString = "";
            JObject obj = new JObject();
            string json = "";
            HttpRequestMessage request;

            switch (type)
            {
                case "POST":
                    json = "{\"name\":\"" + content + "\",\"folder\":{ },\"@microsoft.graph.conflictBehavior\":\"fail\"}";
                    response = await client.PostAsync(API, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
                    responseString = await response.Content.ReadAsStringAsync();
                    obj = JObject.Parse(responseString);
                    break;

                case "PATCH":
                    request = new HttpRequestMessage(new HttpMethod("PATCH"), API);
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    request.Content = new StringContent(content, Encoding.UTF8, "application/json");
                    HttpResponseMessage updateResponse = await client.SendAsync(request);
                    responseString = await updateResponse.Content.ReadAsStringAsync();
                    obj = JObject.Parse(responseString);
                    break;

                case "GET":
                    response = await client.GetAsync(API);
                    responseString = await response.Content.ReadAsStringAsync();
                    obj = JObject.Parse(responseString);
                    break;

                default:
                    break;
            }
            return obj;
        }
    }
}
