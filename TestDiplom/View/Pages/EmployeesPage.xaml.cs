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
    /// Логика взаимодействия для EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : Page
    {
        List<Personal> personals = new List<Personal>();
        public EmployeesPage()
        {
            InitializeComponent();
            personals = Context._con.Personal.ToList();
            EmployeeList.ItemsSource = personals;
        }

        private void AddEmployee(object sender, RoutedEventArgs e)
        {
            AddPersonalWindow addPersonalWindow = new AddPersonalWindow(NavigationService);
            addPersonalWindow.ShowDialog();
        }

        private void EditEmployee(object sender, RoutedEventArgs e)
        {
            if (EmployeeList.SelectedItem != null)
            {
                var personal = EmployeeList.SelectedItem as Personal;
                if (Authorization.curUser.IdRole == 1 && personal.IdRole == 2)
                {
                    MessageBox.Show("Вы не можете редактировать администратора!");
                }
                else
                {
                    AddPersonalWindow addPersonalWindow = new AddPersonalWindow(NavigationService, EmployeeList.SelectedItem as Personal);
                    addPersonalWindow.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника!");
            }
        }

        private void DeleteEmployee(object sender, RoutedEventArgs e)
        {
            if (EmployeeList.SelectedItem != null)
            {
                var curpersonal = EmployeeList.SelectedItem as Personal;
                if (Authorization.curUser.IdRole == 1 && curpersonal.IdRole == 2)
                {
                    MessageBox.Show("Вы не можете удалить администратора!");
                }
                else
                {
                    if (MessageBox.Show("Вы точно хотите удалить сотрудника?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Personal personal = EmployeeList.SelectedItem as Personal;
                        Context._con.Personal.Remove(personal);
                        Context._con.SaveChanges();
                        MessageBox.Show("Сотрудник успешно удалён!");
                        NavigationService.Navigate(new EmployeesPage());
                    }
                }

                    
            }
            else
            {
                MessageBox.Show("Выберите сотрудника!");
            }
        }
    }
}
