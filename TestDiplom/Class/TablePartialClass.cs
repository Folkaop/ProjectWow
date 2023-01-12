using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDiplom.Model
{
    public partial class Table
    {
        public string GetTable
        {
            get
            {
                string table = $"Номер стола: {IdTable}";
                return table;
            }
        }
    }
}
