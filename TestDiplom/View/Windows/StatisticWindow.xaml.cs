using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestDiplom.Model;

namespace TestDiplom.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для StatisticWindow.xaml
    /// </summary>
    public partial class StatisticWindow : Window
    {
        List<string> sortList = new List<string>()
        {
            "Сегодня",
            "Неделя",
            "Месяц",
            "3 месяца",
            "Год",
            "За всё время",
            "Свой период"
        };
        public StatisticWindow()
        {
            InitializeComponent();
            SortCB.ItemsSource = sortList;
            SortCB.SelectedIndex = 0;
            Filter();
            FirstDateDP.SelectedDate = DateTime.Now;
            SecondDateDP.SelectedDate = DateTime.Now;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        int curOverallMostPrice = 0;


        private void Count(DateTime dateStart, DateTime dateEnd)
        {
            var orders = Context._con.Order.ToList()
                                           .Where(p => p.Datetime.Value.Date >= dateStart.Date && p.Datetime.Value.Date <= dateEnd.Date)
                                           .ToList();
            int overallMoney = 0;
            foreach (var item in orders)
            {
                overallMoney += item.GetSortPrice;
            }
            OverallMoneyTB.Text = $"{overallMoney} рублей";

            bool isFirst = true;
            DateTime mostPay = new DateTime();
            DateTime date = dateStart.Date;
            int overallMostPrice = 0;
            while (date <= dateEnd.Date)
            {
                var dayOrders = Context._con.Order.ToList()
                                                  .Where(p => p.Datetime.Value.Date == date.Date)
                                                  .ToList();
                foreach (var order in dayOrders)
                {
                    overallMostPrice += order.GetSortPrice;
                }
                
                if (isFirst)
                {
                    mostPay = date.Date;
                    curOverallMostPrice = overallMostPrice;
                    isFirst = !isFirst;
                }
                else
                {
                    if (curOverallMostPrice < overallMostPrice)
                    {
                        mostPay = date.Date;
                        curOverallMostPrice = overallMostPrice;
                    }
                }
                DateTime curDate = date.Date.AddHours(24);
                date = curDate;
                overallMostPrice = 0;
            }

            isFirst = true;
            DateTime worstPay = new DateTime();
            date = dateStart.Date;
            int overallWorstPrice = 0;
            int curOverallWorstPrice = 0;
            while (date <= dateEnd.Date)
            {
                var dayOrders = Context._con.Order.ToList().Where(p => p.Datetime.Value.Date == date.Date).ToList();
                foreach (var order in dayOrders)
                {
                    overallWorstPrice += order.GetSortPrice;
                }

                if (isFirst)
                {
                    worstPay = date.Date;
                    curOverallWorstPrice = overallWorstPrice;
                    isFirst = !isFirst;
                }
                else
                {
                    if (curOverallWorstPrice > overallWorstPrice)
                    {
                        worstPay = date.Date;
                        curOverallWorstPrice = overallWorstPrice;
                    }
                }
                DateTime curDate = date.Date.AddHours(24);
                date = curDate;
                overallWorstPrice = 0;
            }

            DateTime mostPayable = mostPay.Date;
            MostPayableDay.Text = mostPayable.Date.ToString();

            DateTime worstPayable = worstPay.Date;
            WorstPayableDay.Text = worstPayable.Date.ToString();

        }

        private void Filter()
        {
            switch (SortCB.SelectedIndex)
            {
                case 0:
                    Count(DateTime.Now, DateTime.Now);
                    OwnPeriodSP.Visibility = Visibility.Collapsed;
                    break;
                case 1:
                    Count(DateTime.Now.AddHours(-168), DateTime.Now);
                    OwnPeriodSP.Visibility = Visibility.Collapsed;

                    break;
                case 2:
                    Count(DateTime.Now.AddHours(-720), DateTime.Now);
                    OwnPeriodSP.Visibility = Visibility.Collapsed;

                    break;
                case 3:
                    Count(DateTime.Now.AddHours(-2160), DateTime.Now);
                    OwnPeriodSP.Visibility = Visibility.Collapsed;

                    break;
                case 4:
                    Count(DateTime.Now.AddHours(-8760), DateTime.Now);
                    OwnPeriodSP.Visibility = Visibility.Collapsed;

                    break;
                case 5:
                    var order = Context._con.Order.First();
                    Count(order.Datetime.Value.Date, DateTime.Now);
                    OwnPeriodSP.Visibility = Visibility.Collapsed;

                    break;
                case 6:
                    OwnPeriodSP.Visibility = Visibility.Visible;
                    Count(FirstDateDP.SelectedDate.Value, SecondDateDP.SelectedDate.Value);
                    break;
            }
        }

        private void SortCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void FirstDateDP_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void SecondDateDP_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }
    }
}
