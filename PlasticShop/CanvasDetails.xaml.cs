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
    /// Interaction logic for CanvasDetails.xaml
    /// </summary>
    public partial class CanvasDetails : Window
    {
        private PRODUCT p;
        public CanvasDetails(PRODUCT product)
        {
            InitializeComponent();
            using (var context = new Entities())
            {
                p = context.PRODUCTS.Find(product.PRODUCT_ID);
                var canvas = new CANVAS();
                canvas = context.CANVASES.Find(product.PRODUCT_ID);
                productName.Text = p.PRODUCT_NAME;
                productsInStock.Text = p.PRODUCTS_IN_STOCK.ToString();
                discount.Text = p.DISCOUNT.ToString();
                price.Text = p.PRICE.ToString();
                producer.Text = p.PRODUCER;
                canvasSize.Text = canvas.CANVASE_SIZE;
                canvasMaterial.Text = canvas.MATERIAL;
            }
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            using (var context = new Entities())
            {
                var product = context.PRODUCTS.Find(p.PRODUCT_ID);
                var canvas = context.CANVASES.Find(p.PRODUCT_ID);
                if (string.IsNullOrEmpty(productName.Text))
                {
                    MessageBox.Show("Invalid data", "Name");
                    return;
                }
                else
                {
                    try
                    {
                        product.PRODUCT_NAME = productName.Text;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Invalid data", "Name");
                        return;
                    }
                }
                try
                {
                    product.PRODUCTS_IN_STOCK = int.Parse(productsInStock.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid data", "Products in stock");
                    return;
                }
                try
                {
                    product.DISCOUNT = int.Parse(discount.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalide data", "Discount");
                    return;
                }
                try
                {
                    product.PRICE = decimal.Parse(price.Text);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Invalid data", "Price");
                    return;
                }
                if (string.IsNullOrEmpty(producer.Text))
                {
                    MessageBox.Show("Invalid data", "Producer");
                    return;
                }
                else
                {
                    try
                    {
                        product.PRODUCER = producer.Text;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Invalid data", "Producer");
                        return;
                    }
                }
                if (string.IsNullOrEmpty(canvasSize.Text))
                {
                    MessageBox.Show("Invalid data", "Canvas Size");
                    return;
                }
                else
                {
                    try
                    {
                        canvas.CANVASE_SIZE = canvasSize.Text;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Invalid data", "Canvas Size");
                        return;
                    }
                }
                if (string.IsNullOrEmpty(canvasMaterial.Text))
                {
                    MessageBox.Show("Invalid data", "Material");
                    return;
                }
                else
                {
                    try
                    {
                        canvas.MATERIAL = canvasMaterial.Text;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Invalid data", "Material");
                        return;
                    }
                }
                context.SaveChanges();
                this.Close();
            }
        }
    }
}
