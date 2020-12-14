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
    /// Interaction logic for InkDetails.xaml
    /// </summary>
    public partial class InkDetails : Window
    {
        private PRODUCT p;
        private List<COLOUR> colours;
        public InkDetails(PRODUCT product)
        {
            InitializeComponent();
            using (var context = new Entities())
            {
                colours = new List<COLOUR>();
                foreach (COLOUR colour in context.COLOURS)
                {
                    colours.Add(new COLOUR() { COLOUR_ID = colour.COLOUR_ID, COLOUR_NAME = colour.COLOUR_NAME });
                }
                colourInk.ItemsSource = colours;
                p = context.PRODUCTS.Find(product.PRODUCT_ID);
                var ink = new INK();
                ink = context.INKS.Find(product.PRODUCT_ID);
                InkName.Text = p.PRODUCT_NAME;
                InkInStock.Text = p.PRODUCTS_IN_STOCK.ToString();
                discountInk.Text = p.DISCOUNT.ToString();
                priceInk.Text = p.PRICE.ToString();
                producerInk.Text = p.PRODUCER;
                typeInk.Text = ink.INT_TYPE;
                var c = context.COLOURS.Find(ink.COLOUR_ID);
                colourInk.SelectedItem = c;
            }
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Entities())
            {
                var product = context.PRODUCTS.Find(p.PRODUCT_ID);
                var ink = context.INKS.Find(p.PRODUCT_ID);
                if (string.IsNullOrEmpty(InkName.Text))
                {
                    MessageBox.Show("Invalid data", "Name");
                    return;
                }
                else
                {
                    try
                    {
                        product.PRODUCT_NAME = InkName.Text;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Invalid data", "Name");
                        return;
                    }
                }
                try
                {
                    product.PRODUCTS_IN_STOCK = int.Parse(InkInStock.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid data", "Products in stock");
                    return;
                }
                try
                {
                    product.DISCOUNT = int.Parse(discountInk.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalide data", "Discount");
                    return;
                }
                try
                {
                    product.PRICE = decimal.Parse(priceInk.Text);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Invalid data", "Price");
                    return;
                }
                if (string.IsNullOrEmpty(producerInk.Text))
                {
                    MessageBox.Show("Invalid data", "Producer");
                    return;
                }
                else
                {
                    try
                    {
                        product.PRODUCER = producerInk.Text;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Invalid data", "Producer");
                        return;
                    }
                }
                if (string.IsNullOrEmpty(typeInk.Text))
                {
                    MessageBox.Show("Invalid data", "Crayon Type");
                    return;
                }
                else
                {
                    try
                    {
                        ink.INT_TYPE = typeInk.Text;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Invalid data", "Crayon Type");
                        return;
                    }
                }

                ink.COLOUR_ID = ((COLOUR)colourInk.SelectedItem).COLOUR_ID;
                context.SaveChanges();
                this.Close();
            }
        }
    }
}
