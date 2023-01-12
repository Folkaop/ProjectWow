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
    /// Логика взаимодействия для StatusPage.xaml
    /// </summary>
    public partial class StatusPage : Page
    {
        List<Status> statuses = new List<Status>();
        public StatusPage()
        {
            InitializeComponent();
            statuses = Context._con.Status.ToList();
            StatusList.ItemsSource = statuses.Where( p => p.NameStatus != "Отсутствует");
        }

        private void AddStatus(object sender, RoutedEventArgs e)
        {
            AddStatusWindow addStatusWindow = new AddStatusWindow(NavigationService);
            addStatusWindow.ShowDialog();
        }

        private void EditStatus(object sender, RoutedEventArgs e)
        {
            if (StatusList.SelectedItem != null)
            {
                AddStatusWindow addStatusWindow = new AddStatusWindow(NavigationService, StatusList.SelectedItem as Status);
                addStatusWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите статус!");
            }
        }

        private void DeleteStatus(object sender, RoutedEventArgs e)
        {
            if (StatusList.SelectedItem != null)
            {
                if (MessageBox.Show("Вы точно хотите удалить статус?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Status status = StatusList.SelectedItem as Status;
                    Context._con.Status.Remove(status);
                    Context._con.SaveChanges();
                    MessageBox.Show("Статус успешно удалён!");
                    NavigationService.Navigate(new StatusPage());
                }
            }
            else
            {
                MessageBox.Show("Выберите статус!");
            }
        }
    }
}
