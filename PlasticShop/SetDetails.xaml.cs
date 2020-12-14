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
    /// Interaction logic for SetDetails.xaml
    /// </summary>
    public partial class SetDetails : Window
    {
        private PRODUCT p;
        public SetDetails(PRODUCT product)
        {
            InitializeComponent();
            using (var context = new Entities())
            {
                p = context.PRODUCTS.Find(product.PRODUCT_ID);
                var set = new SET();
                set = context.SETS.Find(product.PRODUCT_ID);
                SetName.Text = p.PRODUCT_NAME;
                SetInStock.Text = p.PRODUCTS_IN_STOCK.ToString();
                discountSet.Text = p.DISCOUNT.ToString();
                priceSet.Text = p.PRICE.ToString();
                producerSet.Text = p.PRODUCER;
                typeSet.Text = set.TYPE;
            }

        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Entities())
            {
                var product = context.PRODUCTS.Find(p.PRODUCT_ID);
                var set = context.SETS.Find(p.PRODUCT_ID);
                if (string.IsNullOrEmpty(SetName.Text))
                {
                    MessageBox.Show("Invalid data", "Name");
                    return;
                }
                else
                {
                    try
                    {
                        product.PRODUCT_NAME = SetName.Text;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Invalid data", "Name");
                        return;
                    }
                }
                try
                {
                    product.PRODUCTS_IN_STOCK = int.Parse(SetInStock.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid data", "Products in stock");
                    return;
                }
                try
                {
                    product.DISCOUNT = int.Parse(discountSet.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalide data", "Discount");
                    return;
                }
                try
                {
                    product.PRICE = decimal.Parse(priceSet.Text);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Invalid data", "Price");
                    return;
                }
                if (string.IsNullOrEmpty(producerSet.Text))
                {
                    MessageBox.Show("Invalid data", "Producer");
                    return;
                }
                else
                {
                    try
                    {
                        product.PRODUCER = producerSet.Text;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Invalid data", "Producer");
                        return;
                    }
                }
                if (string.IsNullOrEmpty(typeSet.Text))
                {
                    MessageBox.Show("Invalid data", "Crayon Type");
                    return;
                }
                else
                {
                    try
                    {
                        set.TYPE = typeSet.Text;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Invalid data", "Crayon Type");
                        return;
                    }
                }
                context.SaveChanges();
                this.Close();
            }
        }
    }
}
