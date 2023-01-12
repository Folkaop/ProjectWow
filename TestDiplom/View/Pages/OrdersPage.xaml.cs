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
using TestDiplom.View.Windows;

namespace TestDiplom.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        List<Order> orders = new List<Order>();
        List<string> sortList = new List<string>()
        {
            "По номеру(по возрастанию)",
            "По номеру(по убыванию)",
            "По сумме(по возрастанию)",
            "По сумме(по убыванию)"
        };
        public OrdersPage()
        {
            InitializeComponent();
            Filter();
            SortCB.ItemsSource = sortList;
            SortCB.SelectedIndex = 1;
        }

        private void AddOrder(object sender, RoutedEventArgs e)
        {
            AddOrderWindow addOrderWindow = new AddOrderWindow(NavigationService);
            addOrderWindow.ShowDialog();
        }

        private void EditOrder(object sender, RoutedEventArgs e)
        {
            if (OrderList.SelectedItem is Order)
            {
                if(OrderList.SelectedItem is Order order && order.Status == false)
                {
                    AddOrderWindow addOrderWindow = new AddOrderWindow(NavigationService, OrderList.SelectedItem as Order);
                    addOrderWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Заказ уже оплачен, его нельзя редактировать!");
                }
                
            }
            else
            {
                MessageBox.Show("Выберите заказ!");
            }
        }

        private void DeleteOrder(object sender, RoutedEventArgs e)
        {
            if (OrderList.SelectedItem != null)
            {
                var curOrder = OrderList.SelectedItem as Order;
                if(curOrder.Status == false)
                {
                    if (MessageBox.Show("Вы точно хотите удалить заказ?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Order order = OrderList.SelectedItem as Order;
                        foreach (var item in Context._con.OrderItem.ToList())
                        {
                            if (item.IdOrder == order.IdOrder)
                            {
                                Context._con.OrderItem.Remove(item);
                                Context._con.SaveChanges();
                            }
                        }
                        Context._con.Order.Remove(order);
                        Context._con.SaveChanges();
                        MessageBox.Show("Заказ успешно удалён!");
                        NavigationService.Navigate(new OrdersPage());
                    }
                }
                else
                {
                    MessageBox.Show("Этот заказ нельзя удалить!");
                }
                
            }
            else
            {
                MessageBox.Show("Выберите заказ!");
            }
        }

        private void ToPayment(object sender, RoutedEventArgs e)
        {
            if(OrderList.SelectedItem is Order)
            {
                PaymentWindow paymentWindow = new PaymentWindow(NavigationService, OrderList.SelectedItem as Order);
                paymentWindow.ShowDialog();

            }
            else
            {
                MessageBox.Show("Выберите заказ!");
            }
        }

        public void Filter()
        {
            orders = Context._con.Order.ToList();
            OrderList.ItemsSource = orders;

            if (!string.IsNullOrEmpty(SearchTB.Text))
            {
                orders = orders.Where(p => p.IdOrder.ToString().Contains(SearchTB.Text) || p.Guest.LastName.Contains(SearchTB.Text) 
                                                                                        || p.Guest.FirstName.Contains(SearchTB.Text)
                                                                                        || p.Personal.LastName.Contains(SearchTB.Text)
                                                                                        || p.Personal.FirstName.Contains(SearchTB.Text)
                                                                                        || p.GetStatus.Contains(SearchTB.Text)
                                                                                        || p.IdTable.ToString().Contains(SearchTB.Text)
                                                                                        || p.Datetime.Value.Date.ToString()
                                                                                                                .Contains(SearchTB.Text)).ToList();
            }
            OrderList.ItemsSource = orders;

            if(SortCB.SelectedIndex >= 0)
            {
                switch (SortCB.SelectedIndex)
                {
                    case 0:
                        orders = orders.OrderBy(p => p.IdOrder).ToList();
                        break;
                    case 1:
                        orders = orders.OrderByDescending(p => p.IdOrder).ToList();
                        break;
                    case 2:
                        orders = orders.OrderBy(p => p.GetSortPrice).ToList();
                        break;
                    case 3:
                        orders = orders.OrderByDescending(p => p.GetSortPrice).ToList();
                        break;
                }
            }
            OrderList.ItemsSource = orders;


        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void SortCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void ToStatistic(object sender, RoutedEventArgs e)
        {
            StatisticWindow statisticWindow = new StatisticWindow();
            statisticWindow.ShowDialog();
        }
    }
}
