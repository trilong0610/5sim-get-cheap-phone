using GetPhoneCode.model;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetPhoneCode._5sim
{
    public class UserCommon
    {
        public static UserModel loadUserBalance(string api)
        {
            var user = new UserModel();
            Uri Url = new Uri(@"https://5sim.net/v1/user/profile");
            var restClient = new RestClient(Url);
            RestRequest restRequest = new RestRequest("", Method.Get);
            restClient.AddDefaultHeader("Authorization", string.Format("Bearer {0}", api));
            restClient.AddDefaultHeader("Accept", "application/json");
            var restResponse = restClient.Execute(restRequest);
            if (!restResponse.IsSuccessful)
            {
                
                user.id = "API Sai";
                user.email = "API Sai";
                user.balance = 0;
                return user;
            }
            var json = JObject.Parse(restResponse.Content);
            var id = json["id"].ToString();
            var email = json["email"].ToString();
            var balance = json["balance"].ToString();
            //progressBar.Invoke((Action)(() => progressBar.MaximumValue = 100));
            user.id = id;
            user.email = email;
            user.balance = Double.Parse(balance) * 410;
            return user;
        }

        public static List<OrderModel> loadOrderHistory(string api, List<string> blackLists, string limit = "50")
        {
            var historys = new List<OrderModel>();
            string url = string.Format(@"https://5sim.net/v1/user/orders?category=activation&limit={0}&order=id&reverse=true", limit);
            var headers = RestCCommon.getHeaders(api);
            var jsonRes = RestCCommon.requestGet(url, headers);

            var data = jsonRes["Data"];
            for (int i = 0; i < data.Count(); i++)
            {
                var item = new OrderModel(data[i]);

                // Neu co blackList thi loc trang thai
                if (blackLists.Count > 0)
                {
                    bool flag = false;
                    // Neu status co trong blacklist thi continue
                    foreach (var wList in blackLists)
                    {
                        if (wList.Equals(item.Status))
                        {
                            flag = true;
                            break;
                        }
                    }

                    // Neu status khong co trong blacklist thi them vao mang
                    if(!flag)
                        historys.Add(item);
                }
                // Khong co blacklist thi hien thi toan bo trang thai
                else
                {
                    historys.Add(item);
                }
                
                    
            }

            // Loc trang thai
            
            

            return historys;

            
        }
    }
}
