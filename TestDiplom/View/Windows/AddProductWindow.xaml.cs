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
    /// Логика взаимодействия для AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        Item curItem { get; set; }
        NavigationService service { get; set; }
        bool isEmpty;
        public AddProductWindow(NavigationService navigationService, Item item = null)
        {
            InitializeComponent();
            curItem = new Item();
            service = navigationService;
            if (item != null)
            {
                AcceptBtn.Content = "Редактировать";
                isEmpty = false;
                curItem = item;
            }
            else
            {
                isEmpty = true;
            }

            MainGrid.DataContext = curItem;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AcceptItem(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(curItem.NameItem) || string.IsNullOrWhiteSpace(curItem.Price.ToString()))
            {
                MessageBox.Show("Вы не ввели название или цену!");
            }
            else
            {
                if (isEmpty)
                {
                    Context._con.Item.Add(curItem);
                    Context._con.SaveChanges();
                    MessageBox.Show("Товар успешно добавлен!");
                    service.Navigate(new ProductsPage());
                    this.Close();
                }
                else
                {
                    Context._con.SaveChanges();
                    MessageBox.Show("Товар успешно изменён!");
                    service.Navigate(new StatusPage());
                    this.Close();
                }
            }
        }
    }
}
