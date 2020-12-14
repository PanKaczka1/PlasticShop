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
    /// Interaction logic for DeliverDetails.xaml
    /// </summary>
    public partial class DeliverDetails : Window
    {
        private INFOSTOREORDER orderInfo;
        private List<PRODUCT> products;
        public DeliverDetails(INFOSTOREORDER order)
        {
            InitializeComponent();
            products = new List<PRODUCT>();
            using (var context = new Entities())
            {
                orderInfo = context.INFOSTOREORDERs.Find(order.ORDER_ID);
                orderDate.Text = orderInfo.ORDER_DATE.ToString();
                foreach (var item in context.STOREORDERS)
                {
                    if (orderInfo.ORDER_ID == item.ORDER_ID)
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
            deliveryDate.SelectedDate = System.DateTime.Now;
            using (var context = new Entities())
            {
                orderInfo.DELIVERY_DATE = System.DateTime.Now;
                context.SaveChanges();
            }
        }
    }
}
