using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetPhoneCode._5sim
{
    public class PurchaseCommon
    {
        public static JObject buyNumber(string api, string country, string _operator, string product)
        {
            string url = string.Format("https://5sim.net/v1/user/buy/activation/{0}/{1}/{2}", country, _operator, product);
            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("Authorization", String.Format("Bearer {0}", api));
            var res = RestCCommon.requestGet(url, header);
            var code = res["rescode"].ToString();
            if (code.Equals("401"))
            {
                MessageBox.Show(String.Format("{0}-{1}-{2} đã hết số", product, country, _operator));
            }
            return res;
            
        }


    }
}
