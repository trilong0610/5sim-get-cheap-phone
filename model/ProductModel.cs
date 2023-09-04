using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetPhoneCode.model
{
    public class ProductModel
    {
        public int id { get; set; }
        public string name { get; set; }

        public ProductModel(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public ProductModel()
        {
        }
    }
}
