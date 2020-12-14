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
    /// Interaction logic for PenDetails.xaml
    /// </summary>
    public partial class PenDetails : Window
    {
        private PRODUCT p;
        private List<COLOUR> colours;
        public PenDetails(PRODUCT product)
        {
            InitializeComponent();
            using (var context = new Entities())
            {
                colours = new List<COLOUR>();
                foreach (COLOUR colour in context.COLOURS)
                {
                    colours.Add(new COLOUR() { COLOUR_ID = colour.COLOUR_ID, COLOUR_NAME = colour.COLOUR_NAME });
                }
                colourPen.ItemsSource = colours;
                p = context.PRODUCTS.Find(product.PRODUCT_ID);
                var pen = new PEN();
                pen = context.PENS.Find(product.PRODUCT_ID);
                PenName.Text = p.PRODUCT_NAME;
                PenInStock.Text = p.PRODUCTS_IN_STOCK.ToString();
                discountPen.Text = p.DISCOUNT.ToString();
                pricePen.Text = p.PRICE.ToString();
                producerPen.Text = p.PRODUCER;
                typePen.Text = pen.PEN_TYPE;
                var c = context.COLOURS.Find(pen.COLOUR_ID);
                colourPen.SelectedItem = c;
            }
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Entities())
            {
                var product = context.PRODUCTS.Find(p.PRODUCT_ID);
                var pen = context.PENS.Find(p.PRODUCT_ID);
                if (string.IsNullOrEmpty(PenName.Text))
                {
                    MessageBox.Show("Invalid data", "Name");
                    return;
                }
                else
                {
                    try
                    {
                        product.PRODUCT_NAME = PenName.Text;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Invalid data", "Name");
                        return;
                    }
                }
                try
                {
                    product.PRODUCTS_IN_STOCK = int.Parse(PenInStock.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid data", "Products in stock");
                    return;
                }
                try
                {
                    product.DISCOUNT = int.Parse(discountPen.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalide data", "Discount");
                    return;
                }
                try
                {
                    product.PRICE = decimal.Parse(pricePen.Text);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Invalid data", "Price");
                    return;
                }
                if (string.IsNullOrEmpty(producerPen.Text))
                {
                    MessageBox.Show("Invalid data", "Producer");
                    return;
                }
                else
                {
                    try
                    {
                        product.PRODUCER = producerPen.Text;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Invalid data", "Producer");
                        return;
                    }
                }
                if (string.IsNullOrEmpty(typePen.Text))
                {
                    MessageBox.Show("Invalid data", "Crayon Type");
                    return;
                }
                else
                {
                    try
                    {
                        pen.PEN_TYPE = typePen.Text;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Invalid data", "Crayon Type");
                        return;
                    }
                }

                pen.COLOUR_ID = ((COLOUR)colourPen.SelectedItem).COLOUR_ID;
                context.SaveChanges();
                this.Close();
            }
        }
    }
}
