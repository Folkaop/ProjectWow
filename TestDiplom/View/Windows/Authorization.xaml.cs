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
using System.Windows.Shapes;
using TestDiplom.Model;

namespace TestDiplom.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public static Personal curUser { get; set; }
        public Authorization()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Auth(object sender, RoutedEventArgs e)
        {
            var user = Context._con.Personal.Where(p => p.UserName == AuthLoginTbx.Text && p.Password == AuthPassPbx.Text).FirstOrDefault();
            if(user != null)
            {
                curUser = user;
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Пользователь не найден!");
            }
        }

        private void AuthLoginTbx_GotFocus(object sender, RoutedEventArgs e)
        {
            if(AuthLoginTbx.Text == "Login")
            {
                AuthLoginTbx.Text = string.Empty;
            }
        }

        private void AuthLoginTbx_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AuthLoginTbx.Text))
            {
                AuthLoginTbx.Text = "Login";
            }
        }

        private void AuthPassPbx_GotFocus(object sender, RoutedEventArgs e)
        {
            if (AuthPassPbx.Text == "Password")
            {
                AuthPassPbx.Text = string.Empty;
            }
        }

        private void AuthPassPbx_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AuthPassPbx.Text))
            {
                AuthPassPbx.Text = "Password";
            }
        }
    }
}
