using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
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
        ObservableCollection<PRODUCT> crayons;
        ObservableCollection<COLOUR> colours;
        ObservableCollection<CUSTOMER> customers;
        ObservableCollection<DELIVERER> deliverers;
        ObservableCollection<INFOORDERCUSTOMER> customerOrders;
        ObservableCollection<INFOSTOREORDER> storeOrders;
        ObservableCollection<PRODUCT> products;
        ObservableCollection<CUSTOMERORDER> customerOrderProducts;
        ObservableCollection<COLOUR> productColours;
        ObservableCollection<PRODUCT> productsInOrder;
        ObservableCollection<DISCOUNTPOINT> discountPointsCollection;

        public MainWindow()
        {
            InitializeComponent();
            pencils = new ObservableCollection<PRODUCT>();
            canvases = new ObservableCollection<PRODUCT>();
            crayons = new ObservableCollection<PRODUCT>();
            colours = new ObservableCollection<COLOUR>();
            customers = new ObservableCollection<CUSTOMER>();
            deliverers = new ObservableCollection<DELIVERER>();
            customerOrders = new ObservableCollection<INFOORDERCUSTOMER>();
            storeOrders = new ObservableCollection<INFOSTOREORDER>();
            products = new ObservableCollection<PRODUCT>();
            customerOrderProducts = new ObservableCollection<CUSTOMERORDER>();
            productColours = new ObservableCollection<COLOUR>();
            productsInOrder = new ObservableCollection<PRODUCT>();
            discountPointsCollection = new ObservableCollection<DISCOUNTPOINT>();
            using (var context = new Entities())
            {
                foreach (var pencil in context.PENCILS)
                {
                    var item = context.PRODUCTS.Find(pencil.PRODUCT_ID);
                    pencils.Add(new PRODUCT() { PRODUCT_NAME = item.PRODUCT_NAME, PRODUCT_ID = item.PRODUCT_ID });
                }
                foreach (var canvas in context.CANVASES)
                {
                    var canvasItem = context.PRODUCTS.Find(canvas.PRODUCT_ID);
                    canvases.Add(new PRODUCT() { PRODUCT_NAME = canvasItem.PRODUCT_NAME, PRODUCT_ID = canvasItem.PRODUCT_ID });
                }
                foreach(var crayon in context.CRAYONS)
                {
                    var crayonItem = context.PRODUCTS.Find(crayon.PRODUCT_ID);
                    crayons.Add(new PRODUCT() { PRODUCT_NAME = crayonItem.PRODUCT_NAME, PRODUCT_ID = crayonItem.PRODUCT_ID });
                }
                foreach (var colour in context.COLOURS)
                {
                    var colourItem = context.COLOURS.Find(colour.COLOUR_ID);
                    colours.Add(new COLOUR() { COLOUR_NAME = colourItem.COLOUR_NAME, COLOUR_ID = colourItem.COLOUR_ID });
                }
                foreach (var customer in context.CUSTOMERS)
                {
                    var customerItem = context.CUSTOMERS.Find(customer.CUSTOMER_ID);
                    customers.Add(new CUSTOMER() { NAME = customerItem.NAME, CUSTOMER_ID = customerItem.CUSTOMER_ID });
                }
                foreach (var deliverer in context.DELIVERERS)
                {
                    var delivererItem = context.DELIVERERS.Find(deliverer.DELIVERER_ID);
                    deliverers.Add(new DELIVERER() { DELIVERER_NAME = delivererItem.DELIVERER_NAME, DELIVERER_ID = delivererItem.DELIVERER_ID });
                }
                foreach (var customerOrder in context.INFOORDERCUSTOMERs)
                {
                    var customerOrderItem = context.INFOORDERCUSTOMERs.Find(customerOrder.ORDER_ID);
                    customerOrders.Add(new INFOORDERCUSTOMER() { ORDER_DATE = customerOrderItem.ORDER_DATE, ORDER_ID = customerOrderItem.ORDER_ID });
                }
                foreach (var discountPoint in context.DISCOUNTPOINTS)
                {
                    var discountPointItem = context.DISCOUNTPOINTS.Find(discountPoint.DISCOUNT);
                    discountPointsCollection.Add(new DISCOUNTPOINT() { DISCOUNT = discountPointItem.DISCOUNT });
                }
            }
            discountPointsList.ItemsSource = discountPointsCollection;
            pencilsList.ItemsSource = pencils;
            canvasesList.ItemsSource = canvases;
            crayonsList.ItemsSource = crayons;
            coloursList.ItemsSource = colours;
            customersList.ItemsSource = customers;
            deliverersList.ItemsSource = deliverers;
            ordersList.ItemsSource = customerOrders;
        }

        private void DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount == 2)
            {
                PencilDetails pencilDetails = new PencilDetails((PRODUCT)pencilsList.SelectedItem);
                pencilDetails.Show();
            }
        }
        private void DoubleClickOrder(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount == 2)
            {
                OrdersDetails orderDetails = new OrdersDetails((INFOORDERCUSTOMER)ordersList.SelectedItem);
                orderDetails.Show();
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

        private void DoubleClickDeliverer(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount == 2)
            {
                DelivererDetails delivererDetails = new DelivererDetails((DELIVERER)deliverersList.SelectedItem);
                delivererDetails.Show();
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

        private void DeleteDelivererClick(object sender, RoutedEventArgs e)
        {
            using (var context = new Entities())
            {
                foreach (DELIVERER deliverer in deliverers)
                {
                    if (deliverer == (DELIVERER)deliverersList.SelectedItem)
                    {
                        context.DELIVERERS.Attach(deliverer);
                        context.DELIVERERS.Remove(deliverer);
                        context.SaveChanges();
                    }
                }
            }
            deliverers.Remove((DELIVERER)deliverersList.SelectedItem);
        }

        private void DeleteOrderClick(object sender, RoutedEventArgs e)
        {
            using (var context = new Entities())
            {
                foreach (INFOORDERCUSTOMER order in customerOrders)
                {
                    if (order == (INFOORDERCUSTOMER)ordersList.SelectedItem)
                    {
                        context.INFOORDERCUSTOMERs.Attach(order);
                        context.INFOORDERCUSTOMERs.Remove(order);
                    }
                }
                context.SaveChanges();
            }
            customerOrders.Remove((INFOORDERCUSTOMER)ordersList.SelectedItem);
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
            if(string.IsNullOrEmpty(canvasName.Text))
            {
                MessageBox.Show("Invalid data", "Name");
                return;
            }
            else
            {
                try
                {
                    product.PRODUCT_NAME = canvasName.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid data", "Name");
                    return;
                }
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
            if(string.IsNullOrEmpty(producerCanvas.Text))
            {
                MessageBox.Show("Invalid data", "Producer");
                return;
            }
            else
            {
                try
                {
                    product.PRODUCER = producerCanvas.Text;
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Invalid data", "Producer");
                    return;
                }
            }
            if(string.IsNullOrEmpty(canvaseSize.Text))
            {
                MessageBox.Show("Invalid data", "Canvas Size");
                return;
            }
            else
            {
                try
                {
                    canvas.CANVASE_SIZE = canvaseSize.Text;
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Invalid data", "Canvas Size");
                    return;
                }
            }
            if(string.IsNullOrEmpty(canvasMaterial.Text))
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
            if (string.IsNullOrEmpty(colourName.Text))
            {
                MessageBox.Show("Invalid data", "Colour Name");
            }
            else
            {
                try
                {
                    colour.COLOUR_NAME = colourName.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid data", "Colour Name");
                    return;
                }
            }
            try
            {
                colour.RED_VALUE = decimal.Parse(redValue.Value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid data", "Red Value");
                return;
            }
            try
            {
                colour.GREEN_VALUE = decimal.Parse(greenValue.Value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalide data", "Green Value");
                return;
            }
            try
            {
                colour.BLUE_VALUE = decimal.Parse(blueValue.Value.ToString());
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
            if (graphiteGrade.Text.Length > 4 || string.IsNullOrEmpty(graphiteGrade.Text))
            {
                MessageBox.Show("Invalid data", "Graphite grade");
                return;
            }
            else
            {
                try
                {
                    pencil.GRAPHITE_GRADE = graphiteGrade.Text;
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Invalid data", "Graphite grade");
                    return;
                }
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
                if (context.CUSTOMERS.Any())
                {
                    decimal id = context.CUSTOMERS.Max(p => p.CUSTOMER_ID);
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

        private void AddDelivererClick(object sender, RoutedEventArgs e)
        {

            var deliverer = new DELIVERER();
            using (var context = new Entities())
            {
                if (context.DELIVERERS.Any())
                {
                    decimal id = context.DELIVERERS.Max(p => p.DELIVERER_ID);
                    deliverer.DELIVERER_ID = id + 1;
                }

            }
            try
            {
                deliverer.DELIVERER_NAME = delivererName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid data", "Name");
                return;
            }
            try
            {
                deliverer.BANK_ACCOUNT = delivererBankAccount.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid data", "Bank Account");
                return;
            }
            try
            {
                deliverer.EMAIL = delivererEmail.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid data", "Email");
                return;
            }
            try
            {
                deliverer.PHONE_NUMBER = delivererPhoneNumber.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid data", "Phone Number");
                return;
            }
            try
            {
                deliverer.POST_CODE = delivererPostCode.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid data", "Post Code");
                return;
            }
            try
            {
                deliverer.CITY = delivererCity.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid data", "City");
                return;
            }
            try
            {
                deliverer.STREET_NAME = delivererStreetName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid data", "Street Name");
                return;
            }
            try
            {
                deliverer.HOUSE_NUMBER = delivererHouseNumber.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid data", "House Number");
                return;
            }

            using (var context = new Entities())
            {
                context.DELIVERERS.Add(deliverer);
                var item = context.DELIVERERS.Find(deliverer.DELIVERER_ID);
                deliverers.Add(new DELIVERER() { DELIVERER_NAME = item.DELIVERER_NAME, DELIVERER_ID = item.DELIVERER_ID });
                context.SaveChanges();
            }
        }
        private void AddOrderClick(object sender, RoutedEventArgs e)
        {
            var customerOrder = new INFOORDERCUSTOMER();
            var cOrder = new List<CUSTOMERORDER>();
            decimal id;
            decimal counter;
            using (var context = new Entities())
            {
                if (context.INFOORDERCUSTOMERs.Any())
                {
                    id = context.INFOORDERCUSTOMERs.Max(p => p.ORDER_ID);
                    customerOrder.ORDER_ID = id + 1;
                }
                counter = 1;
                foreach (var itemProduct in customerOrderProducts)
                {
                    if (context.CUSTOMERORDERS.Any())
                    {
                        id = context.CUSTOMERORDERS.Max(p => p.CUSTOMERORDERS_ID);
                        id = id + counter;
                        counter++;
                        cOrder.Add(new CUSTOMERORDER() { PRODUCT_ID = itemProduct.PRODUCT_ID, NUMBER_OF_PRODUCTS = itemProduct.NUMBER_OF_PRODUCTS, ORDER_ID = customerOrder.ORDER_ID, CUSTOMERORDERS_ID = id });
                    }
                    else
                    {
                        cOrder.Add(new CUSTOMERORDER() { PRODUCT_ID = itemProduct.PRODUCT_ID, NUMBER_OF_PRODUCTS = itemProduct.NUMBER_OF_PRODUCTS, ORDER_ID = customerOrder.ORDER_ID });
                    }
                }
            }
            customerOrder.CUSTOMER_ID = ((CUSTOMER)customersOrder.SelectedItem).CUSTOMER_ID;
            try
            {
                customerOrder.SUMMARY_DISCOUNT = decimal.Parse(summaryDiscount.Text);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Invalid data", "Summary Discount");
            }
            customerOrder.ORDER_DATE = System.DateTime.Now;
            using (var context = new Entities())
            {
                foreach (var itemProduct in cOrder)
                {
                    context.CUSTOMERORDERS.Add(itemProduct);
                }
                context.INFOORDERCUSTOMERs.Add(customerOrder);
                var item = context.INFOORDERCUSTOMERs.Find(customerOrder.ORDER_ID);
                customerOrders.Add(new INFOORDERCUSTOMER() { ORDER_DATE = item.ORDER_DATE, ORDER_ID = item.ORDER_ID });
                productsInOrder.Clear();
                context.SaveChanges();
            }
        }

        private void addProductOrder_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Entities())
            {
                var item = context.PRODUCTS.Find(((PRODUCT)productsOrder.SelectedItem).PRODUCT_ID);
                if(item.PRODUCTS_IN_STOCK < decimal.Parse(productsAmount.Text))
                {
                    MessageBox.Show("Not enough products in stock");
                    return;
                }
                else
                {
                    item.PRODUCTS_IN_STOCK = item.PRODUCTS_IN_STOCK - decimal.Parse(productsAmount.Text);
                    context.SaveChanges();
                }
                customerOrderProducts.Add(new CUSTOMERORDER() { NUMBER_OF_PRODUCTS = decimal.Parse(productsAmount.Text), PRODUCT_ID = ((PRODUCT)productsOrder.SelectedItem).PRODUCT_ID });
                MessageBox.Show("Product " + ((PRODUCT)productsOrder.SelectedItem).PRODUCT_NAME + " was added to order");
                productsInOrder.Add(new PRODUCT() { PRODUCT_NAME = ((PRODUCT)productsOrder.SelectedItem).PRODUCT_NAME });
                productsOrderList.ItemsSource = productsInOrder;
            }
        }

        private void deleteCrayon_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Entities())
            {
                foreach (PRODUCT crayon in crayons)
                {
                    if (crayon == (PRODUCT)crayonsList.SelectedItem)
                    {
                        var item = context.CRAYONS.Find(crayon.PRODUCT_ID);
                        context.CRAYONS.Attach(item);
                        context.CRAYONS.Remove(item);
                        context.PRODUCTS.Attach(crayon);
                        context.PRODUCTS.Remove(crayon);
                        context.SaveChanges();
                    }
                }
            }
            crayons.Remove((PRODUCT)crayonsList.SelectedItem);
        }

        private void addCrayon_Click(object sender, RoutedEventArgs e)
        {
            var product = new PRODUCT();
            var crayon = new CRAYON();
            using (var context = new Entities())
            {
                if (context.PRODUCTS.Any())
                {
                    decimal id = context.PRODUCTS.Max(p => p.PRODUCT_ID);
                    product.PRODUCT_ID = id + 1;
                    crayon.PRODUCT_ID = id + 1;
                }

            }
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



            using (var context = new Entities())
            {
                context.PRODUCTS.Add(product);
                context.CRAYONS.Add(crayon);
                var item = context.PRODUCTS.Find(crayon.PRODUCT_ID);
                crayons.Add(new PRODUCT() { PRODUCT_NAME = item.PRODUCT_NAME, PRODUCT_ID = item.PRODUCT_ID });
                context.SaveChanges();
            }
        }

        private void deleteDeliver_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addDeliver_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addProductDeliver_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteDiscountPoint_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addDiscountPoint_Click(object sender, RoutedEventArgs e)
        {
            var discountPoint = new DISCOUNTPOINT();

            try
            {
                discountPoint.DISCOUNT = int.Parse(discountPoints.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalide data", "Discount");
                return;
            }
            try
            {
                discountPoint.POINTS_MINIMUM = int.Parse(pointsMin.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalide data", "Discount");
                return;
            }
            try
            {
                discountPoint.POINTS_MAXIMUM = int.Parse(pointsMax.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalide data", "Discount");
                return;
            }



            using (var context = new Entities())
            {
                context.DISCOUNTPOINTS.Add(discountPoint);
                var item = context.DISCOUNTPOINTS.Find(discountPoint.DISCOUNT);
                discountPointsCollection.Add(new DISCOUNTPOINT() { DISCOUNT = item.DISCOUNT});
                context.SaveChanges();
            }
        }

        private void orderBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount == 2)
            {
                OrdersDetails orderDetails = new OrdersDetails((INFOORDERCUSTOMER)ordersList.SelectedItem);
                orderDetails.Show();
            }
        }

        private void ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            byte rr = (byte)redValue.Value;
            byte gg = (byte)greenValue.Value;
            byte bb = (byte)blueValue.Value;
            Color cc = Color.FromRgb(rr, gg, bb);
            SolidColorBrush colorBrush = new SolidColorBrush(cc);
            borderColour.Background = colorBrush;
        }

        private void productsOrder_DropDownOpened(object sender, EventArgs e)
        {
            products.Clear();
            customersOrder.ItemsSource = customers;
            using (var context = new Entities())
            {
                foreach (var item in context.PRODUCTS)
                {
                    products.Add(new PRODUCT() { PRODUCT_ID = item.PRODUCT_ID, PRODUCT_NAME = item.PRODUCT_NAME });
                }
            }
            productsOrder.ItemsSource = products;
        }

        private void colour_DropDownOpened(object sender, EventArgs e)
        {
            colourCrayon.ItemsSource = colours;
        }
    }
}


