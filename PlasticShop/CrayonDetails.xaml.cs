﻿using System;
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
    /// Interaction logic for CrayonDetails.xaml
    /// </summary>
    public partial class CrayonDetails : Window
    {
        private PRODUCT p;
        private List<COLOUR> colours;
        public CrayonDetails(PRODUCT product)
        {
            InitializeComponent();
            using (var context = new Entities())
            {
                colours = new List<COLOUR>();
                foreach(COLOUR colour in context.COLOURS)
                {
                    colours.Add(new COLOUR() { COLOUR_ID = colour.COLOUR_ID, COLOUR_NAME = colour.COLOUR_NAME });
                }
                colourCrayon.ItemsSource = colours;
                p = context.PRODUCTS.Find(product.PRODUCT_ID);
                var crayon = new CRAYON();
                crayon = context.CRAYONS.Find(product.PRODUCT_ID);
                CrayonName.Text = p.PRODUCT_NAME;
                CrayonsInStock.Text = p.PRODUCTS_IN_STOCK.ToString();
                discountCrayon.Text = p.DISCOUNT.ToString();
                priceCrayon.Text = p.PRICE.ToString();
                producerCrayon.Text = p.PRODUCER;
                typeCrayon.Text = crayon.CRAYON_TYPE;
                shapeCrayon.Text = crayon.SHAPE;
                var c = context.COLOURS.Find(crayon.COLOUR_ID);
                colourCrayon.SelectedItem = c;
            }
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Entities())
            {
                var product = context.PRODUCTS.Find(p.PRODUCT_ID);
                var crayon = context.CRAYONS.Find(p.PRODUCT_ID);
                if (string.IsNullOrEmpty(CrayonName.Text))
                {
                    MessageBox.Show("Invalid data", "Name");
                    return;
                }
                else
                {
                    try
                    {
                        product.PRODUCT_NAME = CrayonName.Text;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Invalid data", "Name");
                        return;
                    }
                }
                try
                {
                    product.PRODUCTS_IN_STOCK = int.Parse(CrayonsInStock.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid data", "Products in stock");
                    return;
                }
                try
                {
                    product.DISCOUNT = int.Parse(discountCrayon.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalide data", "Discount");
                    return;
                }
                try
                {
                    product.PRICE = decimal.Parse(priceCrayon.Text);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Invalid data", "Price");
                    return;
                }
                if (string.IsNullOrEmpty(producerCrayon.Text))
                {
                    MessageBox.Show("Invalid data", "Producer");
                    return;
                }
                else
                {
                    try
                    {
                        product.PRODUCER = producerCrayon.Text;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Invalid data", "Producer");
                        return;
                    }
                }
                if (string.IsNullOrEmpty(typeCrayon.Text))
                {
                    MessageBox.Show("Invalid data", "Crayon Type");
                    return;
                }
                else
                {
                    try
                    {
                        crayon.CRAYON_TYPE = typeCrayon.Text;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Invalid data", "Crayon Type");
                        return;
                    }
                }
                if (string.IsNullOrEmpty(shapeCrayon.Text))
                {
                    MessageBox.Show("Invalid data", "Crayon Shape");
                    return;
                }
                else
                {
                    try
                    {
                        crayon.SHAPE = shapeCrayon.Text;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Invalid data", "Crayon Shape");
                        return;
                    }
                }

                crayon.COLOUR_ID = ((COLOUR)colourCrayon.SelectedItem).COLOUR_ID;
                context.SaveChanges();
                this.Close();
            }
        }
    }
}
