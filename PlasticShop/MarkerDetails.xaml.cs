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
    /// Interaction logic for MarkerDetails.xaml
    /// </summary>
    public partial class MarkerDetails : Window
    {
        private PRODUCT p;
        private List<COLOUR> colours;
        public MarkerDetails(PRODUCT product)
        {
            InitializeComponent();
            using (var context = new Entities())
            {
                colours = new List<COLOUR>();
                foreach (COLOUR colour in context.COLOURS)
                {
                    colours.Add(new COLOUR() { COLOUR_ID = colour.COLOUR_ID, COLOUR_NAME = colour.COLOUR_NAME });
                }
                colourMarker.ItemsSource = colours;
                p = context.PRODUCTS.Find(product.PRODUCT_ID);
                var marker = new MARKER();
                marker = context.MARKERS.Find(product.PRODUCT_ID);
                MarkerName.Text = p.PRODUCT_NAME;
                MarkerInStock.Text = p.PRODUCTS_IN_STOCK.ToString();
                discountMarker.Text = p.DISCOUNT.ToString();
                priceMarker.Text = p.PRICE.ToString();
                producerMarker.Text = p.PRODUCER;
                typeMarker.Text = marker.MARKER_TYPE;
                var c = context.COLOURS.Find(marker.COLOUR_ID);
                colourMarker.SelectedItem = c;
            }
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Entities())
            {
                var product = context.PRODUCTS.Find(p.PRODUCT_ID);
                var marker = context.MARKERS.Find(p.PRODUCT_ID);

                if (string.IsNullOrEmpty(MarkerName.Text))
                {
                    MessageBox.Show("Invalid data", "Name");
                    return;
                }
                else
                {
                    try
                    {
                        product.PRODUCT_NAME = MarkerName.Text;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Invalid data", "Name");
                        return;
                    }
                }
                try
                {
                    product.PRODUCTS_IN_STOCK = int.Parse(MarkerInStock.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid data", "Products in stock");
                    return;
                }
                try
                {
                    product.DISCOUNT = int.Parse(discountMarker.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalide data", "Discount");
                    return;
                }
                try
                {
                    product.PRICE = decimal.Parse(priceMarker.Text);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Invalid data", "Price");
                    return;
                }
                if (string.IsNullOrEmpty(producerMarker.Text))
                {
                    MessageBox.Show("Invalid data", "Producer");
                    return;
                }
                else
                {
                    try
                    {
                        product.PRODUCER = producerMarker.Text;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Invalid data", "Producer");
                        return;
                    }
                }
                if (string.IsNullOrEmpty(typeMarker.Text))
                {
                    MessageBox.Show("Invalid data", "Crayon Type");
                    return;
                }
                else
                {
                    try
                    {
                        marker.MARKER_TYPE = typeMarker.Text;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Invalid data", "Crayon Type");
                        return;
                    }
                }

                marker.COLOUR_ID = ((COLOUR)colourMarker.SelectedItem).COLOUR_ID;
                context.SaveChanges();
                this.Close();
            }
        }
    }
}
