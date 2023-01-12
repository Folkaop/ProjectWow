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
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        List<Item> items = new List<Item>();
        public ProductsPage()
        {
            InitializeComponent();
            items = Context._con.Item.ToList();
            ProductList.ItemsSource = items;
        }

        private void DeleteProduct(object sender, RoutedEventArgs e)
        {
            if (ProductList.SelectedItem != null)
            {
                if (MessageBox.Show("Вы точно хотите удалить товар?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Item item = ProductList.SelectedItem as Item;
                    Context._con.Item.Remove(item);
                    Context._con.SaveChanges();
                    MessageBox.Show("Товар успешно удалён!");
                    NavigationService.Navigate(new ProductsPage());
                }
            }
            else
            {
                MessageBox.Show("Выберите товар!");
            }
        }

        private void AddProduct(object sender, RoutedEventArgs e)
        {
            AddProductWindow addProductWindow = new AddProductWindow(NavigationService);
            addProductWindow.ShowDialog();
        }
    }
}
