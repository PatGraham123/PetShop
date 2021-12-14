using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop
{
    public class CartList
    {
        public bool loggedIn = Login.loggedIn;
        public string Name { get; set; }
        public string Type { get; set; }
        public double Qty { get; set; }
        public double Cost { get; set; }
    }

}
