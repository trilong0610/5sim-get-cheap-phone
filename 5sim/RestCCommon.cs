using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetPhoneCode._5sim
{
    public class RestCCommon
    {
        public static JObject requestGet(string url, Dictionary<string, string> headers = null)
        {
            JObject json;
            Uri Url = new Uri(@url);
            var restClient = new RestClient(Url);

            if (headers != null)
               restClient.AddDefaultHeaders(headers);

            RestRequest restRequest = new RestRequest("", Method.Get);
            var restResponse = restClient.Execute(restRequest);

            // Kiem tra trang thai response
            if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (restResponse.Content.Equals("no free phones"))
                {
                    json = JObject.Parse("{\"rescode\" : 401}");
                    return json;
                }

                if (JObject.Parse(restResponse.Content) == null)
                {
                    json = JObject.Parse("{\"rescode\" : 201}");
                    return json;
                }
                
            }
            if (restResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                json = JObject.Parse("{\"rescode\" : 400}");
                return json;
            }
            if (restResponse.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                json = JObject.Parse("{\"rescode\" : 500}");
                return json;
            }

            json = JObject.Parse(restResponse.Content);
            json.Add("rescode", JObject.Parse(@"{""rescode"":""200""}"));
            return json;
        }
        public static Dictionary<string, string> getHeaders(string api, Dictionary<string, string> header = null)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            if (header == null)
            {
                
                headers.Add("Authorization", String.Format("Bearer {0}", api));
                headers.Add("Accept", "application/json");
            }
            else
            {
                foreach (var item in header)
                {
                    headers.Add(item.Key, item.Value);
                }
            }
            
            return headers;
        }
    }
}
