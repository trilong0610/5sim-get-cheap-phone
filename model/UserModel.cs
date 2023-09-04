using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetPhoneCode.model
{
    public class UserModel
    {
        public string id { get; set; }
        public string email { get; set; }
        public double balance { get; set; }

        public UserModel(string id, string email, double balance)
        {
            this.id = id;
            this.email = email;
            this.balance = balance;
        }

        public UserModel()
        {
        }
    }
}
