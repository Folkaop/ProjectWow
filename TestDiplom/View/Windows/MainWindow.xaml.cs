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
using TestDiplom.View.Windows;

namespace TestDiplom
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PersonalNameTB.Text = Authorization.curUser.GetName;
            PersonalRoleTB.Text = Authorization.curUser.Role.NameRole;
            MainFrame.Navigate(new OrdersPage());
            int price = Context._con.Order.ToList().OrderByDescending(p => p.IdOrder).FirstOrDefault().GetSortPrice;
            Console.WriteLine(price.ToString());
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ToGuests(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new GuestsPage());
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            Authorization.curUser = null;
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Close();
        }

        private void ToEmployee(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new EmployeesPage());
        }

        private void ToStatus(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new StatusPage());
        }

        private void ToProduct(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ProductsPage());
        }

        private void ToOrder(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new OrdersPage());
        }
    }
}
