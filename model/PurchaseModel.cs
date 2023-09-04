using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetPhoneCode.model
{
    public class PurchaseModel
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Operator { get; set; }
        public string Product { get; set; }
        public double Price { get; set; }
        public DateTime Expires { get; set; }
        public string Sms { get; set; }
        public string country { get; set; }
    }
}
