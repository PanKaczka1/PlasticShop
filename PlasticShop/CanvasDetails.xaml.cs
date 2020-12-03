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
                product.PRODUCT_NAME = productName.Text;
                product.PRODUCTS_IN_STOCK = int.Parse(productsInStock.Text);
                product.DISCOUNT = int.Parse(discount.Text);
                product.PRICE = decimal.Parse(price.Text);
                product.PRODUCER = producer.Text;
                canvas.CANVASE_SIZE = canvasSize.Text;
                canvas.MATERIAL = canvasMaterial.Text;
                context.SaveChanges();
                this.Close();
            }
        }
    }
}
