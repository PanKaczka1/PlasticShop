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
    /// Interaction logic for SculptingMaterialDetails.xaml
    /// </summary>
    public partial class SculptingMaterialDetails : Window
    {
        private PRODUCT p;
        private List<COLOUR> colours;
        public SculptingMaterialDetails(PRODUCT product)
        {
            InitializeComponent();
            using (var context = new Entities())
            {
                colours = new List<COLOUR>();
                foreach (COLOUR colour in context.COLOURS)
                {
                    colours.Add(new COLOUR() { COLOUR_ID = colour.COLOUR_ID, COLOUR_NAME = colour.COLOUR_NAME });
                }
                colourSculptingMaterial.ItemsSource = colours;
                p = context.PRODUCTS.Find(product.PRODUCT_ID);
                var sMaterial = new SCULPTINGMATERIAL();
                sMaterial = context.SCULPTINGMATERIALS.Find(product.PRODUCT_ID);
                SculptingMaterialName.Text = p.PRODUCT_NAME;
                SculptingMaterialInStock.Text = p.PRODUCTS_IN_STOCK.ToString();
                discountSculptingMaterial.Text = p.DISCOUNT.ToString();
                priceSculptingMaterial.Text = p.PRICE.ToString();
                producerSculptingMaterial.Text = p.PRODUCER;
                typeSculptingMaterial.Text = sMaterial.SM_TYPE;
                var c = context.COLOURS.Find(sMaterial.COLOUR_ID);
                colourSculptingMaterial.SelectedItem = c;
            }
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Entities())
            {
                var product = context.PRODUCTS.Find(p.PRODUCT_ID);
                var sMaterial = context.SCULPTINGMATERIALS.Find(p.PRODUCT_ID);
                if (string.IsNullOrEmpty(SculptingMaterialName.Text))
                {
                    MessageBox.Show("Invalid data", "Name");
                    return;
                }
                else
                {
                    try
                    {
                        product.PRODUCT_NAME = SculptingMaterialName.Text;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Invalid data", "Name");
                        return;
                    }
                }
                try
                {
                    product.PRODUCTS_IN_STOCK = int.Parse(SculptingMaterialInStock.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid data", "Products in stock");
                    return;
                }
                try
                {
                    product.DISCOUNT = int.Parse(discountSculptingMaterial.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalide data", "Discount");
                    return;
                }
                try
                {
                    product.PRICE = decimal.Parse(priceSculptingMaterial.Text);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Invalid data", "Price");
                    return;
                }
                if (string.IsNullOrEmpty(producerSculptingMaterial.Text))
                {
                    MessageBox.Show("Invalid data", "Producer");
                    return;
                }
                else
                {
                    try
                    {
                        product.PRODUCER = producerSculptingMaterial.Text;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Invalid data", "Producer");
                        return;
                    }
                }
                if (string.IsNullOrEmpty(typeSculptingMaterial.Text))
                {
                    MessageBox.Show("Invalid data", "Crayon Type");
                    return;
                }
                else
                {
                    try
                    {
                        sMaterial.SM_TYPE = typeSculptingMaterial.Text;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Invalid data", "Crayon Type");
                        return;
                    }
                }

                sMaterial.COLOUR_ID = ((COLOUR)colourSculptingMaterial.SelectedItem).COLOUR_ID;
                context.SaveChanges();
                this.Close();
            }
        }
    }
}
