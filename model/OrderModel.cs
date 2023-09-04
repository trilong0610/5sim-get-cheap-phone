using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetPhoneCode.model
{
    public class OrderModel
    {
        public string Id { get; set; }
        public string Phone { get; set; }
        public string Product { get; set; }
        public string Price { get; set; }
        public string Status { get; set; }
        public DateTime Expires { get; set; }
        public JToken Sms { get; set; }
        public string Country { get; set; }

        public OrderModel(string id, string phone, string product, string price, string status, DateTime expires, JToken sms, string country)
        {
            Id = id;
            Phone = phone;
            Product = product;
            Price = price;
            Status = status;
            Expires = expires;
            Sms = sms;
            Country = country;
        }

        public OrderModel()
        {
        }

        public OrderModel(JToken json)
        {
            Id = json["id"].ToString();
            Phone = json["phone"].ToString();
            Product = json["product"].ToString();
            Price = json["price"].ToString();
            Status = json["status"].ToString();
            Expires = DateTime.Parse(json["expires"].ToString());
            Sms = json["sms"];
            // kiem tra da nhan duoc code chua
            if (Sms.Count() > 0)
            {
                Sms = json["sms"][0]["code"];
            }
            else
            {
                Sms = null;
            }
            Country = json["country"].ToString();
        }
    }
}
