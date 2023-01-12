using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDiplom.Model
{
    public partial class Guest
    {
        public string GetName
        {
            get
            {
                string name = $"{FirstName} {LastName}";
                return name;
            }
        }

        public string GetBonus
        {
            get
            {
                string bonus = $"Всего бонусов: {CountBonus}";
                return bonus;
            }
        }

        public string GetOrderName
        {
            get
            {
                string name = $"Гость: {FirstName} {LastName}";
                return name;
            }
        }
    }
}
