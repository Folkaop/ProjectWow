using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDiplom.Model
{
    public partial class Personal
    {
        public string GetName
        {
            get
            {
                string name = $"{FirstName} {LastName}";
                return name;
            }
        }

        public string GetFullName
        {
            get
            {
                string name = $"{LastName} {FirstName} {MIddleName}";
                return name;
            }
        }

        public string GetOrderName
        {
            get
            {
                string name = $"Сотрудник: {FirstName} {LastName}";
                return name;
            }
        }
    }
}
