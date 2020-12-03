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
    /// Interaction logic for PencilDetails.xaml
    /// </summary>
    public partial class PencilDetails : Window
    {
        private PRODUCT p;
        public PencilDetails(PRODUCT product)
        {
            InitializeComponent();
            using(var context = new Entities())
            {
                p = context.PRODUCTS.Find(product.PRODUCT_ID);
                var pencil = new PENCIL();
                pencil = context.PENCILS.Find(product.PRODUCT_ID);
                productName.Text = p.PRODUCT_NAME;
                productsInStock.Text = p.PRODUCTS_IN_STOCK.ToString();
                discount.Text = p.DISCOUNT.ToString();
                price.Text = p.PRICE.ToString();
                producer.Text = p.PRODUCER;
                graphiteGrade.Text = pencil.GRAPHITE_GRADE;
            }

        }



        private void EditClick(object sender, RoutedEventArgs e)
        {
            using(var context = new Entities())
            {
                var product = context.PRODUCTS.Find(p.PRODUCT_ID);
                var pencil = context.PENCILS.Find(p.PRODUCT_ID);
                product.PRODUCT_NAME = productName.Text;
                product.PRODUCTS_IN_STOCK = int.Parse(productsInStock.Text);
                product.DISCOUNT = int.Parse(discount.Text);
                product.PRICE = decimal.Parse(price.Text);
                product.PRODUCER = producer.Text;
                pencil.GRAPHITE_GRADE = graphiteGrade.Text;
                context.SaveChanges();
                this.Close();
            }
        }
    }
}
