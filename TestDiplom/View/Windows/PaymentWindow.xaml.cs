using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestDiplom.Model;
using TestDiplom.View.Pages;

namespace TestDiplom.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для PaymentWindow.xaml
    /// </summary>
    public partial class PaymentWindow : Window
    {
        Order curOrder { get; set; }
        NavigationService service { get; set; }
        bool isEmpty;
        public PaymentWindow(NavigationService navigationService, Order order)
        {
            InitializeComponent();
            curOrder = new Order();
            service = navigationService;
            if (order != null)
            {
                AcceptBtn.Content = "Оплатить";
                isEmpty = false;
                curOrder = order;
            }
            else
            {
                isEmpty = true;
            }

            MainGrid.DataContext = curOrder;

            FinalTB.Text = curOrder.GetFinalPrice;

            if(curOrder.Guest.CountBonus == 0 || curOrder.Guest.CountBonus == null)
            {
                BonusSum.IsEnabled = false;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(BonusSum.Text))
            {
                if (Convert.ToInt32(BonusSum.Text) > curOrder.Guest.CountBonus)
                {
                    MessageBox.Show("Недостаточно бонусов!");
                    BonusSum.Text = "";
                    FinalTB.Text = curOrder.GetFinalPrice;
                }
                else
                {
                    var price = curOrder.GetFinalPrice.Split(' ');
                    var summa = Convert.ToInt32(price[0]);
                    if (Convert.ToInt32(BonusSum.Text) > summa)
                    {
                        MessageBox.Show("Введите количество баллов, которое меньше, чем сумма чека!");
                        BonusSum.Text = "";
                        FinalTB.Text = curOrder.GetFinalPrice;
                    }
                    else
                    {
                        FinalTB.Text = CountPrice(Convert.ToInt32(BonusSum.Text));
                    }
                }
            }
            else 
            {
                FinalTB.Text = curOrder.GetFinalPrice;
            }
        }

        private string CountPrice(int sum)
        {
            var price = curOrder.GetFinalPrice.Split(' ');
            var summa = Convert.ToInt32(price[0]);
            string finalSum = (summa - sum).ToString();
            return $"{finalSum} рублей";
        }

        private void BonusSum_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AcceptPayment(object sender, RoutedEventArgs e)
        {
            if(curOrder.Guest.FirstName != "Неизвестный" && curOrder.Guest.IdStatus != null)
            {
                if (!string.IsNullOrWhiteSpace(BonusSum.Text))
                {
                    curOrder.Guest.CountBonus -= Convert.ToInt32(BonusSum.Text);
                }
                var price = FinalTB.Text.Split(' ');
                int bonus = Convert.ToInt32(0.05 * Convert.ToInt32(price[0]));
                curOrder.Guest.CountBonus += bonus;
                curOrder.Status = true;
                Context._con.SaveChanges();
                MessageBox.Show($"Оплата произведена! Начисленно {bonus} баллов!");
                service.Navigate(new OrdersPage());
                this.Close();
            }
            else
            {
                curOrder.Status = true;
                Context._con.SaveChanges();
                MessageBox.Show($"Оплата произведена!");
                service.Navigate(new OrdersPage());
                this.Close();
            }

        }
    }
}
