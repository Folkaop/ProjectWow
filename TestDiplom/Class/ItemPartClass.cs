using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDiplom.Model
{
    public partial class Item
    {
        public string GetPrice
        {
            get
            {
                string price = $"Стоимость: {Price}";
                return price;
            }
        }
    }
}
