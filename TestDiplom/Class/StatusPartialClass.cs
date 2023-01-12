using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDiplom.Model
{
    public partial class Status
    {
        public string GetDescription
        {
            get
            {
                string desc = $"{PriceCoef}% Discount";
                return desc;
            }
        }
    }
}
