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
    /// Логика взаимодействия для AddStatusWindow.xaml
    /// </summary>
    public partial class AddStatusWindow : Window
    {
        Status curStatus { get; set; }
        NavigationService service { get; set; }
        bool isEmpty;
        public AddStatusWindow(NavigationService navigationService, Status status = null)
        {
            InitializeComponent();
            curStatus = new Status();
            service = navigationService;
            if (status != null)
            {
                AcceptBtn.Content = "Редактировать";
                isEmpty = false;
                curStatus = status;
            }
            else
            {
                isEmpty = true;
            }

            MainGrid.DataContext = curStatus;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AcceptStatus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(curStatus.NameStatus) || string.IsNullOrWhiteSpace(curStatus.PriceCoef.ToString()))
            {
                MessageBox.Show("Вы не ввели название или процент!");
            }
            else
            {
                if (isEmpty)
                {
                    Context._con.Status.Add(curStatus);
                    Context._con.SaveChanges();
                    MessageBox.Show("Статус успешно добавлен!");
                    service.Navigate(new StatusPage());
                    this.Close();
                }
                else
                {
                    Context._con.SaveChanges();
                    MessageBox.Show("Статус успешно изменён!");
                    service.Navigate(new StatusPage());
                    this.Close();
                }
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
                MessageBox.Show("Введите целое число!(10, 20, 30 и т.д.)");
            }
        }
    }
}
