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
    /// Interaction logic for PaintDetails.xaml
    /// </summary>
    public partial class PaintDetails : Window
    {
        private PRODUCT p;
        private List<COLOUR> colours;
        public PaintDetails(PRODUCT product)
        {
            InitializeComponent();
            using (var context = new Entities())
            {
                colours = new List<COLOUR>();
                foreach (COLOUR colour in context.COLOURS)
                {
                    colours.Add(new COLOUR() { COLOUR_ID = colour.COLOUR_ID, COLOUR_NAME = colour.COLOUR_NAME });
                }
                colourPaint.ItemsSource = colours;
                p = context.PRODUCTS.Find(product.PRODUCT_ID);
                var paint = new PAINT();
                paint = context.PAINTS.Find(product.PRODUCT_ID);
                PaintName.Text = p.PRODUCT_NAME;
                PaintInStock.Text = p.PRODUCTS_IN_STOCK.ToString();
                discountPaint.Text = p.DISCOUNT.ToString();
                pricePaint.Text = p.PRICE.ToString();
                producerPaint.Text = p.PRODUCER;
                typePaint.Text = paint.PANIT_TYPE;
                var c = context.COLOURS.Find(paint.COLOUR_ID);
                colourPaint.SelectedItem = c;
            }
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Entities())
            {
                var product = context.PRODUCTS.Find(p.PRODUCT_ID);
                var paint = context.PAINTS.Find(p.PRODUCT_ID);
                if (string.IsNullOrEmpty(PaintName.Text))
                {
                    MessageBox.Show("Invalid data", "Name");
                    return;
                }
                else
                {
                    try
                    {
                        product.PRODUCT_NAME = PaintName.Text;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Invalid data", "Name");
                        return;
                    }
                }
                try
                {
                    product.PRODUCTS_IN_STOCK = int.Parse(PaintInStock.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid data", "Products in stock");
                    return;
                }
                try
                {
                    product.DISCOUNT = int.Parse(discountPaint.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalide data", "Discount");
                    return;
                }
                try
                {
                    product.PRICE = decimal.Parse(pricePaint.Text);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Invalid data", "Price");
                    return;
                }
                if (string.IsNullOrEmpty(producerPaint.Text))
                {
                    MessageBox.Show("Invalid data", "Producer");
                    return;
                }
                else
                {
                    try
                    {
                        product.PRODUCER = producerPaint.Text;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Invalid data", "Producer");
                        return;
                    }
                }
                if (string.IsNullOrEmpty(typePaint.Text))
                {
                    MessageBox.Show("Invalid data", "Crayon Type");
                    return;
                }
                else
                {
                    try
                    {
                        paint.PANIT_TYPE = typePaint.Text;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Invalid data", "Crayon Type");
                        return;
                    }
                }

                paint.COLOUR_ID = ((COLOUR)colourPaint.SelectedItem).COLOUR_ID;
                context.SaveChanges();
                this.Close();
            }
        }
    }
}
