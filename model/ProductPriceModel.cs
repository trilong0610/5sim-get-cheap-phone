using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetPhoneCode.model
{
    public class ProductPriceModel
    {

        public string Product { get; set; }
        public string Country { get; set; }
        public string Operator { get; set; }
        public double Cost { get; set; }
        public int Count { get; set; }

        public ProductPriceModel(string product, string country, string @operator, double cost, int count)
        {
            Product = product;
            Country = country;
            Operator = @operator;
            Cost = cost;
            Count = count;
        }

        public ProductPriceModel()
        {
        }
    }
}
