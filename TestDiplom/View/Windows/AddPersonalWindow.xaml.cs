using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для AddPersonalWindow.xaml
    /// </summary>
    public partial class AddPersonalWindow : Window
    {
        Personal curPersonal { get; set; }
        NavigationService service { get; set; }
        bool isEmpty;
        public AddPersonalWindow(NavigationService navigationService, Personal personal = null)
        {
            InitializeComponent();
            curPersonal = new Personal();
            service = navigationService;
            if (personal != null)
            {
                AcceptBtn.Content = "Редактировать";
                isEmpty = false;
                curPersonal = personal;
            }
            else
            {
                isEmpty = true;
            }

            MainGrid.DataContext = curPersonal;
            StatusCB.ItemsSource = Context._con.Role.ToList();
        }

        private void AcceptPersonal(object sender, RoutedEventArgs e)
        {

            if (Authorization.curUser.IdRole == 1 && StatusCB.SelectedIndex == 1)
            {

                MessageBox.Show("Вы не можете добавить администратора!");

                StatusCB.SelectedIndex = 0;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(curPersonal.FirstName) || string.IsNullOrWhiteSpace(curPersonal.LastName))
                {
                    MessageBox.Show("Вы не ввели имя или фамилию!");
                }
                else
                {
                    if (isEmpty)
                    {
                        Context._con.Personal.Add(curPersonal);
                        Context._con.SaveChanges();
                        MessageBox.Show("Сотрудник успешно добавлен!");
                        service.Navigate(new EmployeesPage());
                        this.Close();
                    }
                    else
                    {
                        Context._con.SaveChanges();
                        MessageBox.Show("Сотрудник успешно изменён!");
                        service.Navigate(new EmployeesPage());
                        this.Close();
                    }
                }
            }




        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
