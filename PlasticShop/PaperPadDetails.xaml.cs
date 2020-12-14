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
    /// Interaction logic for PaperPadDetails.xaml
    /// </summary>
    public partial class PaperPadDetails : Window
    {
        private PRODUCT p;
        public PaperPadDetails(PRODUCT product)
        {
            InitializeComponent();
            using (var context = new Entities())
            {
                p = context.PRODUCTS.Find(product.PRODUCT_ID);
                var paperPad = new PAPERPAD();
                paperPad = context.PAPERPADS.Find(product.PRODUCT_ID);
                PaperPadName.Text = p.PRODUCT_NAME;
                PaperPadInStock.Text = p.PRODUCTS_IN_STOCK.ToString();
                discountPaperPad.Text = p.DISCOUNT.ToString();
                pricePaperPad.Text = p.PRICE.ToString();
                producerPaperPad.Text = p.PRODUCER;
                paperPadSize.Text = paperPad.PAPER_PAD_SIZE;
                paperPadPages.Text = paperPad.PAGES_NUMBER.ToString();
                grammagePaperPad.Text = paperPad.GRAMMAGE.ToString();
            }

        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Entities())
            {
                var product = context.PRODUCTS.Find(p.PRODUCT_ID);
                var paperPad = context.PAPERPADS.Find(p.PRODUCT_ID);
                if (string.IsNullOrEmpty(PaperPadName.Text))
                {
                    MessageBox.Show("Invalid data", "Name");
                    return;
                }
                else
                {
                    try
                    {
                        product.PRODUCT_NAME = PaperPadName.Text;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Invalid data", "Name");
                        return;
                    }
                }
                try
                {
                    product.PRODUCTS_IN_STOCK = int.Parse(PaperPadInStock.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid data", "Products in stock");
                    return;
                }
                try
                {
                    product.DISCOUNT = int.Parse(discountPaperPad.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalide data", "Discount");
                    return;
                }
                try
                {
                    product.PRICE = decimal.Parse(pricePaperPad.Text);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Invalid data", "Price");
                    return;
                }
                if (string.IsNullOrEmpty(producerPaperPad.Text))
                {
                    MessageBox.Show("Invalid data", "Producer");
                    return;
                }
                else
                {
                    try
                    {
                        product.PRODUCER = producerPaperPad.Text;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Invalid data", "Producer");
                        return;
                    }
                }
                if (string.IsNullOrEmpty(paperPadSize.Text))
                {
                    MessageBox.Show("Invalid data", "Crayon Type");
                    return;
                }
                else
                {
                    try
                    {
                        paperPad.PAPER_PAD_SIZE = paperPadSize.Text;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Invalid data", "Crayon Type");
                        return;
                    }
                }
                try
                {
                    paperPad.PAGES_NUMBER = decimal.Parse(paperPadPages.Text);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Invalid data", "Price");
                    return;
                }
                try
                {
                    paperPad.GRAMMAGE = decimal.Parse(grammagePaperPad.Text);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Invalid data", "Price");
                    return;
                }
                context.SaveChanges();
                this.Close();
            }
        }
    }
}
