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
    /// Interaction logic for CustomerDetails.xaml
    /// </summary>
    public partial class CustomerDetails : Window
    {
        ObservableCollection<INFOORDERCUSTOMER> orders;
        private CUSTOMER c;
        public CustomerDetails(CUSTOMER customer)
        {
            InitializeComponent();
            using (var context = new Entities())
            {
                c = context.CUSTOMERS.Find(customer.CUSTOMER_ID);
                customerName.Text = c.NAME;
                customerBankAccount.Text = c.BANK_ACCOUNT;
                customerEmail.Text = c.EMAIL;
                customerPhoneNumber.Text = c.PHONE_NUMBER;
                customerPostCode.Text = c.POST_CODE;
                customerCity.Text = c.CITY;
                customerStreetName.Text = c.STREET_NAME;
                customerHouseNumber.Text = c.HOUSE_NUMBER;
                customerLocalNumber.Text = c.LOCAL_NUMBER;
                customerDiscountPoints.Text = c.DISCOUNT_POINTS.ToString();
                foreach (var order in context.INFOORDERCUSTOMERs)
                {
                    var item = context.INFOORDERCUSTOMERs.Find(order.ORDER_ID);
                    if(item.CUSTOMER_ID == c.CUSTOMER_ID)
                    {
                        orders.Add(new INFOORDERCUSTOMER() { ORDER_DATE = item.ORDER_DATE, ORDER_ID = item.ORDER_ID });
                    }
                }
            }
        }
        private void EditClick(object sender, RoutedEventArgs e)
        {
            using (var context = new Entities())
            {
                var customer = context.CUSTOMERS.Find(c.CUSTOMER_ID);
                customer.NAME = customerName.Text;
                customer.BANK_ACCOUNT = customerBankAccount.Text;
                customer.EMAIL = customerEmail.Text;
                customer.PHONE_NUMBER = customerPhoneNumber.Text;
                customer.POST_CODE = customerPostCode.Text;
                customer.CITY = customerCity.Text;
                customer.STREET_NAME = customerStreetName.Text;
                customer.HOUSE_NUMBER = customerHouseNumber.Text;
                customer.LOCAL_NUMBER = customerLocalNumber.Text;
                customer.DISCOUNT_POINTS = decimal.Parse(customerDiscountPoints.Text);
                context.SaveChanges();
                this.Close();
            }
        }

        private void DoubleClickOrder(object sender, RoutedEventArgs e)
        {

        }
    }
}
