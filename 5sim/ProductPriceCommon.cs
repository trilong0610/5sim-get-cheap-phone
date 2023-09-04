using GetPhoneCode.model;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GetPhoneCode._5sim
{
    public class ProductPriceCommon
    {
        public static XDocument xmldoc;
        public static string URL_XML_FILE = @"..\..\xml\ProductXML.xml";
        public static List<ProductModel> loadProduct()
        { 
            // load xml file
            xmldoc = XDocument.Load(URL_XML_FILE);
            var data = xmldoc.Descendants("Product").Select(p => new ProductModel
            {
                id = Int32.Parse(p.Element("id").Value),
                name = p.Element("name").Value,

            }).ToList();
            return data;
        
        }

        public static List<ProductPriceModel> loadProductPrices(string product, int count) {
            var prices = new List<ProductPriceModel>();
            Uri Url = new Uri(@"https://5sim.net/v1/guest/prices?product=" + product);
            var restClient = new RestClient(Url);
            RestRequest restRequest = new RestRequest("", Method.Get);
            var restResponse = restClient.Execute(restRequest);
            var json = JObject.Parse(restResponse.Content);
            var countrys = json[product];
            //progressBar.Invoke((Action)(() => progressBar.MaximumValue = 100));
            foreach (var country in countrys)
            {
                //progressBar.Invoke((Action)(() => progressBar.Value = Convert.ToInt32(countPrcBar * (100.0 / countrys.Count()))));
                // afghanistan 
                var operators = country.ElementAt(0);
                foreach (var _operator in operators)
                {

                    //virtual18
                    var _count = Int32.Parse(_operator.ElementAt(0)["count"].ToString());

                    // Neu khong con sim thi bo qua
                    if (_count <= 1 && count == 0)
                    {
                        continue;
                    }

                    if (count != 0 && _count < count)
                    {
                        continue;
                    }
                    var _cost = Math.Floor(Double.Parse(_operator.ElementAt(0)["cost"].ToString()) * 410);
                    
                    var _country = operators.Path.Split('.')[1];
                    var tempOperator = _operator.ElementAt(0).Path.Split('.')[2];
                    prices.Add(new ProductPriceModel(product, _country, tempOperator, _cost, _count));

                }
            }
            return prices;
        }
    }
}
