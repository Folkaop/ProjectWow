using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDiplom.Model
{
    public partial class Order
    {
        public string GetStatus
        {
            get
            {
                string result = "x";
                if (Status == true)
                {
                    result = "Оплачен";
                }
                else
                {
                    result = "Не оплачен";
                }
                return result;
            }
        }

        public string GetColor
        {
            get
            {
                if(Status == false)
                {
                    return "Red";
                }
                else
                {
                    return "Green";
                }
            }
        }

        public string GetOrderId
        {
            get
            {
                string order = $"№ заказа: {IdOrder}";
                return order;
            }
        }

        public string GetData
        {
            get
            {
                string data = $"Дата: {Datetime.Value.Day}.{Datetime.Value.Month}.{Datetime.Value.Year}";
                return data;
            }
        }

        public string GetFinalPrice
        {
            get
            {
                int price = 0;
                int totalPrice = 0;

                if (Guest.FirstName != "Неизвестный" && Guest.IdStatus != null)
                {
                    var order = Context._con.OrderItem.ToList().Where(p => p.IdOrder == IdOrder).ToList();
                    foreach (var item in order)
                    {
                        totalPrice += Convert.ToInt32(item.Item.Price);
                    }
                    double aa = Convert.ToDouble(Guest.Status.PriceCoef) / 100;
                    double discount = totalPrice * aa;
                    price = Convert.ToInt32(totalPrice - discount);
                }
                else
                {
                    var order = Context._con.OrderItem.ToList().Where(p => p.IdOrder == IdOrder).ToList();
                    foreach (var item in order)
                    {
                        totalPrice += Convert.ToInt32(item.Item.Price);
                    }
                    price = totalPrice;
                }
                return $"{price} рублей";
            }
        }

        public int GetSortPrice
        {
            get
            {
                int price = 0;
                int totalPrice = 0;
                try
                {
                    if (Guest?.FirstName != "Неизвестный" && Guest.IdStatus != null)
                    {
                        var order = Context._con.OrderItem.ToList().Where(p => p.IdOrder == IdOrder).ToList();
                        foreach (var item in order)
                        {
                            totalPrice += Convert.ToInt32(item.Item.Price);
                        }
                        double aa = Convert.ToDouble(Guest?.Status?.PriceCoef) / 100;
                        double discount = totalPrice * aa;
                        price = Convert.ToInt32(totalPrice - discount);
                    }
                    else
                    {
                        var order = Context._con.OrderItem.ToList().Where(p => p.IdOrder == IdOrder).ToList();
                        foreach (var item in order)
                        {
                            totalPrice += Convert.ToInt32(item.Item.Price);
                        }
                        price = totalPrice;
                    }
                }
                catch
                {
                    price = 0;
                }
                
                return price;
            }
        }
    }
}
