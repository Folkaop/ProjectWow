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
    /// Логика взаимодействия для GuestsPage.xaml
    /// </summary>
    public partial class GuestsPage : Page
    {
        private List<Guest> guests = new List<Guest>();
        public GuestsPage()
        {
            InitializeComponent();
            guests = Context._con.Guest.ToList().Where(p => p.FirstName != "Неизвестный").ToList();
            GuestList.ItemsSource = guests;
        }

        private void EditGuest(object sender, RoutedEventArgs e)
        {
            if(GuestList.SelectedItem != null)
            {
                AddGuestWindow addGuestWindow = new AddGuestWindow(NavigationService, GuestList.SelectedItem as Guest);
                addGuestWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите гостя!");
            }
        }

        private void AddGuest(object sender, RoutedEventArgs e)
        {
            AddGuestWindow addGuestWindow = new AddGuestWindow(NavigationService);
            addGuestWindow.ShowDialog();
            
        }

        private void DeleteGuest(object sender, RoutedEventArgs e)
        {
            if (GuestList.SelectedItem != null)
            {
                if(MessageBox.Show("Вы точно хотите удалить гостя?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Guest guest = GuestList.SelectedItem as Guest;
                    Context._con.Guest.Remove(guest);
                    Context._con.SaveChanges();
                    MessageBox.Show("Гость успешно удалён!");
                    NavigationService.Navigate(new GuestsPage());
                }
            }
            else
            {
                MessageBox.Show("Выберите гостя!");
            }
        }
    }
}
