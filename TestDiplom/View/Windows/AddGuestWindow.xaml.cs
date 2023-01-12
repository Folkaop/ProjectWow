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
    /// Логика взаимодействия для AddGuestWindow.xaml
    /// </summary>
    public partial class AddGuestWindow : Window
    {
        List<Status> statuses = new List<Status>();
        Guest curGuest { get; set; }
        NavigationService service { get; set; }
        bool isEmpty;
        public AddGuestWindow(NavigationService navigationService, Guest guest = null)
        {
            InitializeComponent();
            curGuest = new Guest();
            service = navigationService;
            if(guest != null)
            {
                AcceptBtn.Content = "Редактировать";
                isEmpty = false;
                curGuest = guest;
            }
            else
            {
                isEmpty = true;
            }

            MainGrid.DataContext = curGuest;

            statuses = Context._con.Status.ToList().Where(p => p.NameStatus != "Отсутствует").ToList();
            statuses.Insert(0, new Status { NameStatus = "Отсутствует" });
            StatusCB.ItemsSource = statuses;
        }

        private void AddGuest(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(curGuest.FirstName) || string.IsNullOrWhiteSpace(curGuest.LastName))
            {
                MessageBox.Show("Вы не ввели имя или фамилию!");
            }
            else
            {
                if (isEmpty)
                {
                    Context._con.Guest.Add(curGuest);
                    Context._con.SaveChanges();
                    MessageBox.Show("Гость успешно добавлен!");
                    service.Navigate(new GuestsPage());
                    this.Close();
                }
                else
                {
                    Context._con.SaveChanges();
                    MessageBox.Show("Гость успешно изменён!");
                    service.Navigate(new GuestsPage());
                    this.Close();
                }
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
