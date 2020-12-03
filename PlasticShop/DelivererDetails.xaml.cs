using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for DelivererDetails.xaml
    /// </summary>
    public partial class DelivererDetails : Window
    {
        ObservableCollection<INFOSTOREORDER> orders;
        private DELIVERER c;
        public DelivererDetails(DELIVERER deliverer)
        {
            InitializeComponent();
            using (var context = new Entities())
            {
                c = context.DELIVERERS.Find(deliverer.DELIVERER_ID);
                delivererName.Text = c.DELIVERER_NAME;
                delivererBankAccount.Text = c.BANK_ACCOUNT;
                delivererEmail.Text = c.EMAIL;
                delivererPhoneNumber.Text = c.PHONE_NUMBER;
                delivererPostCode.Text = c.POST_CODE;
                delivererCity.Text = c.CITY;
                delivererStreetName.Text = c.STREET_NAME;
                delivererHouseNumber.Text = c.HOUSE_NUMBER;
                foreach (var order in context.INFOSTOREORDERs)
                {
                    var item = context.INFOSTOREORDERs.Find(order.ORDER_ID);
                    if (item.DELIVERER_ID == c.DELIVERER_ID)
                    {
                        orders.Add(new INFOSTOREORDER() { ORDER_DATE = item.ORDER_DATE, ORDER_ID = item.ORDER_ID });
                    }
                }
            }
        }
        private void EditClick(object sender, RoutedEventArgs e)
        {
            using (var context = new Entities())
            {
                var deliverer = context.DELIVERERS.Find(c.DELIVERER_ID);
                deliverer.DELIVERER_NAME = delivererName.Text;
                deliverer.BANK_ACCOUNT = delivererBankAccount.Text;
                deliverer.EMAIL = delivererEmail.Text;
                deliverer.PHONE_NUMBER = delivererPhoneNumber.Text;
                deliverer.POST_CODE = delivererPostCode.Text;
                deliverer.CITY = delivererCity.Text;
                deliverer.STREET_NAME = delivererStreetName.Text;
                deliverer.HOUSE_NUMBER = delivererHouseNumber.Text;
                context.SaveChanges();
                this.Close();
            }
        }

        private void DoubleClickOrder(object sender, RoutedEventArgs e)
        {

        }
    }
}
