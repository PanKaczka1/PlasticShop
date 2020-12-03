﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PlasticShop;

namespace PlasticShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<PRODUCT> pencils;
        ObservableCollection<PRODUCT> canvases;
        ObservableCollection<COLOUR> colours;
        ObservableCollection<CUSTOMER> customers;
        public MainWindow()
        {
            InitializeComponent();
            pencils = new ObservableCollection<PRODUCT>();
            canvases = new ObservableCollection<PRODUCT>();
            colours = new ObservableCollection<COLOUR>();
            customers = new ObservableCollection<CUSTOMER>();
            using (var context = new Entities())
            {
                foreach (var pencil in context.PENCILS)
                {
                    var item = context.PRODUCTS.Find(pencil.PRODUCT_ID);
                    pencils.Add(new PRODUCT() { PRODUCT_NAME = item.PRODUCT_NAME, PRODUCT_ID = item.PRODUCT_ID });
                }
                foreach(var canvas in context.CANVASES)
                {
                    var canvasItem = context.PRODUCTS.Find(canvas.PRODUCT_ID);
                    canvases.Add(new PRODUCT() { PRODUCT_NAME = canvasItem.PRODUCT_NAME, PRODUCT_ID = canvasItem.PRODUCT_ID });
                }
                foreach(var colour in context.COLOURS)
                {
                    var colourItem = context.COLOURS.Find(colour.COLOUR_ID);
                    colours.Add(new COLOUR() { COLOUR_NAME = colourItem.COLOUR_NAME, COLOUR_ID = colourItem.COLOUR_ID });
                }
                foreach(var customer in context.CUSTOMERS)
                {
                    var customerItem = context.CUSTOMERS.Find(customer.CUSTOMER_ID);
                    customers.Add(new CUSTOMER() { NAME = customerItem.NAME, CUSTOMER_ID = customerItem.CUSTOMER_ID });
                }
            }
            pencilsList.ItemsSource = pencils;
            canvasesList.ItemsSource = canvases;
            coloursList.ItemsSource = colours;
            customersList.ItemsSource = customers;
        }

        private void DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount == 2)
            {
                PencilDetails pencilDetails = new PencilDetails((PRODUCT)pencilsList.SelectedItem);
                pencilDetails.Show();
            }
        }

        private void DoubleClickCanvas(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount == 2)
            {
                CanvasDetails canvasDetails = new CanvasDetails((PRODUCT)canvasesList.SelectedItem);
                canvasDetails.Show();
            }
        }
        private void DoubleClickColour(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount == 2)
            {
                ColourDetails colourDetails = new ColourDetails((COLOUR)coloursList.SelectedItem);
                colourDetails.Show();
            }
        }
        private void DoubleClickCustomer(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount == 2)
            {
                CustomerDetails customerDetails = new CustomerDetails((CUSTOMER)customersList.SelectedItem);
                customerDetails.Show();
            }
        }

        private void DeletePencilClick(object sender, RoutedEventArgs e)
        {
            using (var context = new Entities())
            {
                foreach (PRODUCT pencil in pencils)
                {
                    if (pencil == (PRODUCT)pencilsList.SelectedItem)
                    {
                        var item = context.PENCILS.Find(pencil.PRODUCT_ID);
                        context.PENCILS.Attach(item);
                        context.PENCILS.Remove(item);
                        context.PRODUCTS.Attach(pencil);
                        context.PRODUCTS.Remove(pencil);
                        context.SaveChanges();
                    }
                }
            }
            pencils.Remove((PRODUCT)pencilsList.SelectedItem);
        }

        private void DeleteCanvasClick(object sender, RoutedEventArgs e)
        {
            using (var context = new Entities())
            {
                foreach (PRODUCT canvas in canvases)
                {
                    if (canvas == (PRODUCT)canvasesList.SelectedItem)
                    {
                        var item = context.CANVASES.Find(canvas.PRODUCT_ID);
                        context.CANVASES.Attach(item);
                        context.CANVASES.Remove(item);
                        context.PRODUCTS.Attach(canvas);
                        context.PRODUCTS.Remove(canvas);
                        context.SaveChanges();
                    }
                }
            }
            canvases.Remove((PRODUCT)canvasesList.SelectedItem);
        }

        private void DeleteColourClick(object sender, RoutedEventArgs e)
        {
            using (var context = new Entities())
            {
                foreach (COLOUR colour in colours)
                {
                    if (colour == (COLOUR)coloursList.SelectedItem)
                    {
                        var item = context.COLOURS.Find(colour.COLOUR_ID);
                        context.COLOURS.Attach(item);
                        context.COLOURS.Remove(item);
                        context.SaveChanges();
                    }
                }
            }
            colours.Remove((COLOUR)coloursList.SelectedItem);
        }

        private void DeleteCustomerClick(object sender, RoutedEventArgs e)
        {
            using (var context = new Entities())
            {
                foreach (CUSTOMER customer in customers)
                {
                    if (customer == (CUSTOMER)customersList.SelectedItem)
                    {
                        context.CUSTOMERS.Attach(customer);
                        context.CUSTOMERS.Remove(customer);
                        context.SaveChanges();
                    }
                }
            }
            customers.Remove((CUSTOMER)customersList.SelectedItem);
        }

        private void AddCanvasClick(object sender, RoutedEventArgs e)
        {

            var product = new PRODUCT();
            var canvas = new CANVAS();
            using (var context = new Entities())
            {
                if (context.PRODUCTS.Any())
                {
                    decimal id = context.PRODUCTS.Max(p => p.PRODUCT_ID);
                    product.PRODUCT_ID = id + 1;
                    canvas.PRODUCT_ID = id + 1;
                }

            }
            try
            {
                product.PRODUCT_NAME = canvasName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid data", "Name");
                return;
            }
            try
            {
                product.PRODUCTS_IN_STOCK = int.Parse(canvasesInStock.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid data", "Products in stock");
                return;
            }
            try
            {
                product.DISCOUNT = int.Parse(discountCanvas.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalide data", "Discount");
                return;
            }
            try
            {
                product.PRICE = decimal.Parse(priceCanvas.Text);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Invalid data", "Price");
                return;
            }
            try
            {
                product.PRODUCER = producerCanvas.Text;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Invalid data", "Producer");
                return;
            }
            try
            {
                canvas.CANVASE_SIZE = canvaseSize.Text;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Invalid data", "Canvas Size");
                return;
            }
            try
            {
                canvas.MATERIAL = canvasMaterial.Text;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Invalid data", "Material");
                return;
            }

            using (var context = new Entities())
            {
                context.PRODUCTS.Add(product);
                context.CANVASES.Add(canvas);
                var item = context.PRODUCTS.Find(canvas.PRODUCT_ID);
                canvases.Add(new PRODUCT() { PRODUCT_NAME = item.PRODUCT_NAME, PRODUCT_ID = item.PRODUCT_ID });
                context.SaveChanges();
            }
        }

        private void AddColourClick(object sender, RoutedEventArgs e)
        {
            var colour = new COLOUR();
            using (var context = new Entities())
            {
                if (context.COLOURS.Any())
                {
                    decimal id = context.COLOURS.Max(p => p.COLOUR_ID);
                    colour.COLOUR_ID = id + 1;
                }

            }
            try
            {
                colour.COLOUR_NAME = colourName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid data", "Name");
                return;
            }
            try
            {
                colour.RED_VALUE = int.Parse(redValue.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid data", "Red Value");
                return;
            }
            try
            {
                colour.GREEN_VALUE = int.Parse(greenValue.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalide data", "Green Value");
                return;
            }
            try
            {
                colour.BLUE_VALUE = int.Parse(blueValue.Text);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Invalid data", "Blue Value");
                return;
            }

            using (var context = new Entities())
            {
                context.COLOURS.Add(colour);
                var item = context.COLOURS.Find(colour.COLOUR_ID);
                colours.Add(new COLOUR() { COLOUR_NAME = item.COLOUR_NAME, COLOUR_ID = item.COLOUR_ID });
                context.SaveChanges();
            }
        }

        private void AddPencilClick(object sender, RoutedEventArgs e)
        {

            var product = new PRODUCT();
            var pencil = new PENCIL();
            using (var context = new Entities())
            {
                if (context.PRODUCTS.Any())
                {
                    decimal id = context.PRODUCTS.Max(p => p.PRODUCT_ID);
                    product.PRODUCT_ID = id + 1;
                    pencil.PRODUCT_ID = id + 1;
                }

            }
            try
            {
                product.PRODUCT_NAME = productName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid data", "Name");
                return;
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
            try
            {
                product.PRODUCER = producer.Text;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Invalid data", "Producer");
                return;
            }
            try
            {
                pencil.GRAPHITE_GRADE = graphiteGrade.Text;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Invalid data", "Graphite grade");
                return;
            }



            using (var context = new Entities())
            {
                context.PRODUCTS.Add(product);
                context.PENCILS.Add(pencil);
                var item = context.PRODUCTS.Find(pencil.PRODUCT_ID);
                pencils.Add(new PRODUCT() { PRODUCT_NAME = item.PRODUCT_NAME, PRODUCT_ID = item.PRODUCT_ID });
                context.SaveChanges();
            }
         }
        private void AddCustomerClick(object sender, RoutedEventArgs e)
        {

            var customer = new CUSTOMER();
            using (var context = new Entities())
            {
                if (context.PRODUCTS.Any())
                {
                    decimal id = context.PRODUCTS.Max(p => p.PRODUCT_ID);
                    customer.CUSTOMER_ID = id + 1;
                }

            }
            try
            {
                customer.NAME = customerName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid data", "Name");
                return;
            }
            try
            {
                customer.BANK_ACCOUNT = customerBankAccount.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid data", "Bank Account");
                return;
            }
            try
            {
                customer.EMAIL = customerEmail.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid data", "Email");
                return;
            }
            try
            {
                customer.PHONE_NUMBER = customerPhoneNumber.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid data", "Phone Number");
                return;
            }
            try
            {
                customer.POST_CODE = customerPostCode.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid data", "Post Code");
                return;
            }
            try
            {
                customer.CITY = customerCity.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid data", "City");
                return;
            }
            try
            {
                customer.STREET_NAME = customerStreetName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid data", "Street Name");
                return;
            }
            try
            {
                customer.HOUSE_NUMBER = customerHouseNumber.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid data", "House Number");
                return;
            }
            try
            {
                customer.LOCAL_NUMBER = customerLocalNumber.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid data", "Local Number");
                return;
            }
            try
            {
                customer.DISCOUNT_POINTS = decimal.Parse(customerDiscountPoints.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid data", "Discount Points");
                return;
            }

            using (var context = new Entities())
            {
                context.CUSTOMERS.Add(customer);
                var item = context.CUSTOMERS.Find(customer.CUSTOMER_ID);
                customers.Add(new CUSTOMER() { NAME = item.NAME, CUSTOMER_ID = item.CUSTOMER_ID });
                context.SaveChanges();
            }
        }
    }
}

