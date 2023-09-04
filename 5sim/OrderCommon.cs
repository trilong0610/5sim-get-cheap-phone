using GetPhoneCode.model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetPhoneCode._5sim
{
    public class OrderCommon
    {
        public static OrderModel checkOrder(string api, string id)
        {
            //string url = String.Format("https://5sim.net/v1/user/check/{0}", "335186974"); // co sms
            string url = String.Format("https://5sim.net/v1/user/check/{0}", id); // khong co sms
            var headers = RestCCommon.getHeaders(api);
            var jsonRes = RestCCommon.requestGet(url, headers);

            var phone = jsonRes["phone"].ToString();
            var product = jsonRes["product"].ToString();
            var price = jsonRes["price"].ToString();
            var status = jsonRes["status"].ToString();
            DateTime expires = DateTime.Parse(jsonRes["expires"].ToString());
            var sms = jsonRes["sms"];
            // kiem tra da nhan duoc code chua
            if (sms.Count() > 0)
            {
                sms = sms[0];
            }
            else
            {
                sms = null;
            }
            
            
            var country = jsonRes["country"].ToString();

            return new OrderModel(id, phone, product, price, status, expires, sms, country);
        }
    }
}
