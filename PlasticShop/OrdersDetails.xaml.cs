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

namespace PlasticShop
{
    /// <summary>
    /// Interaction logic for OrdersDetails.xaml
    /// </summary>
    public partial class OrdersDetails : Window
    {
        private INFOORDERCUSTOMER orderInfo;
        private List<PRODUCT> products;
        public OrdersDetails(INFOORDERCUSTOMER order)
        {
            InitializeComponent();
            orderInfo = new INFOORDERCUSTOMER();
            products = new List<PRODUCT>();
            using (var context = new Entities())
            {
                orderInfo = context.INFOORDERCUSTOMERs.Find(order.ORDER_ID);
                orderDate.Text = orderInfo.ORDER_DATE.ToString();
                summaryDiscount.Text = orderInfo.SUMMARY_DISCOUNT.ToString();
                foreach(var item in context.CUSTOMERORDERS)
                {
                    if(orderInfo.ORDER_ID == item.ORDER_ID)
                    {
                        var product = context.PRODUCTS.Find(item.PRODUCT_ID);
                        products.Add(new PRODUCT() { PRODUCT_ID = product.PRODUCT_ID, PRODUCT_NAME = product.PRODUCT_NAME });
                    }
                }
                productsInOrderList.ItemsSource = products;
            }
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void orderDone_Click(object sender, RoutedEventArgs e)
        {
            shippingDate.SelectedDate = System.DateTime.Now;
            using(var context = new Entities())
            {
                orderInfo.SHIPPING_DATE = System.DateTime.Now;
                context.SaveChanges();
            }
        }
    }
}
