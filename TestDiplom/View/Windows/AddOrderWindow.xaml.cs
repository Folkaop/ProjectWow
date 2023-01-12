using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для AddOrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {
        Order curOrder { get; set; }
        NavigationService service { get; set; }
        bool isEmpty;

        List<Item> items = new List<Item>();
        List<Personal> personals = new List<Personal>();
        List<Guest> guests = new List<Guest>();
        List<Model.Table> tables = new List<Model.Table>();
        List<Item> addedItems = new List<Item>();

        List<OrderItem> orderItems = new List<OrderItem>();
        public AddOrderWindow(NavigationService navigationService, Order order = null)
        {
            InitializeComponent();
            items = Context._con.Item.ToList();
            ItemCB.ItemsSource = items;
            personals = Context._con.Personal.ToList();
            PersonalCB.ItemsSource = personals;
            guests = Context._con.Guest.ToList().Where(p => p.FirstName != "Неизвестный").ToList();
            guests.Insert(0, new Guest { FirstName = "Неизвестный", LastName = "Гость" }); 
            GuestCB.ItemsSource = guests;
            tables = Context._con.Table.ToList();
            TableCB.ItemsSource = tables;

            curOrder = new Order();
            service = navigationService;
            if (order != null)
            {
                AcceptBtn.Content = "Редактировать";
                isEmpty = false;
                curOrder = order;
                orderItems = Context._con.OrderItem.ToList().Where(p => p.IdOrder == order.IdOrder).ToList();
                foreach(var orderItem in orderItems)
                {
                    var item = Context._con.Item.ToList().Where(p => p.IdItem == orderItem.IdItem).FirstOrDefault();
                    addedItems.Add(item);
                }
                ItemsList.ItemsSource = addedItems.ToList();
            }
            else
            {
                isEmpty = true;
            }

            if (curOrder?.Guest?.FirstName == "Неизвестный")
            {
                GuestCB.SelectedIndex = 0;
            }

            MainGrid.DataContext = curOrder;

        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            if (isEmpty)
            {
                if (ItemCB.SelectedItem is Item item)
                {
                    addedItems.Add(item);
                }
                else
                {
                    MessageBox.Show("Выберите товар!");
                }
                ItemsList.ItemsSource = addedItems.ToList();
            }
            else
            {
                if (ItemCB.SelectedItem is Item item)
                {
                    addedItems.Add(item);
                }
                else
                {
                    MessageBox.Show("Выберите товар!");
                }

                ItemsList.ItemsSource = addedItems.ToList();
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            service.Navigate(new OrdersPage());
            this.Close();
        }

        private void AcceptOrder(object sender, RoutedEventArgs e)
        {
            if (!(GuestCB.SelectedItem is Guest) || !(PersonalCB.SelectedItem is Personal) 
                                                 || !(TableCB.SelectedItem is Model.Table)
                                                 || addedItems.Count == 0)
            {
                MessageBox.Show("Вы не заполнили данные о заказе!");
            }
            else
            {
                if (isEmpty)
                {
                    curOrder.Datetime = DateTime.Now;
                    curOrder.Status = false;
                    if(GuestCB.SelectedIndex == 0)
                    {
                        curOrder.IdGuest = null;
                    }
                    Context._con.Order.Add(curOrder);
                    Context._con.SaveChanges();

                    foreach(var item in addedItems)
                    {
                        OrderItem orderItem = new OrderItem();
                        orderItem.IdItem = item.IdItem;
                        orderItem.IdOrder = curOrder.IdOrder;
                        Context._con.OrderItem.Add(orderItem);
                        Context._con.SaveChanges();
                    }
                    MessageBox.Show("Заказ успешно добавлен!");
                    service.Navigate(new OrdersPage());
                    this.Close();
                }
                else
                {
                    var previousOrders = Context._con.OrderItem.ToList().Where(p => p.IdOrder == curOrder.IdOrder).ToList();
                    foreach(var item in previousOrders)
                    {
                        Context._con.OrderItem.Remove(item);
                    }

                    Context._con.SaveChanges();

                    foreach (var item in addedItems)
                    {
                        OrderItem orderItem = new OrderItem();
                        orderItem.IdItem = item.IdItem;
                        orderItem.IdOrder = curOrder.IdOrder;
                        Context._con.OrderItem.Add(orderItem);
                        Context._con.SaveChanges();
                    }
                    Context._con.SaveChanges();
                    MessageBox.Show("Заказ успешно изменён!");
                    service.Navigate(new OrdersPage());
                    this.Close();
                }
            }
        }


        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if(ItemsList.SelectedItem is Item item)
            {
                addedItems.Remove(item);
                MessageBox.Show("Товар успешно удалён!");
                ItemsList.ItemsSource = addedItems.ToList();
            }
            else
            {
                MessageBox.Show("Выберите товар!");
            }
        }

        
    }
}
