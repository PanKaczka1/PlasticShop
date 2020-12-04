using System;
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
        ObservableCollection<DELIVERER> deliverers;
        ObservableCollection<INFOORDERCUSTOMER> customerOrders;
        ObservableCollection<INFOSTOREORDER> storeOrders;
        ObservableCollection<PRODUCT> products;
        ObservableCollection<CUSTOMERORDER> productsInOrder;

        public MainWindow()
        {
            InitializeComponent();
            pencils = new ObservableCollection<PRODUCT>();
            canvases = new ObservableCollection<PRODUCT>();
            colours = new ObservableCollection<COLOUR>();
            customers = new ObservableCollection<CUSTOMER>();
            deliverers = new ObservableCollection<DELIVERER>();
            customerOrders = new ObservableCollection<INFOORDERCUSTOMER>();
            storeOrders = new ObservableCollection<INFOSTOREORDER>();
            products = new ObservableCollection<PRODUCT>();
            productsInOrder = new ObservableCollection<CUSTOMERORDER>();
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
                foreach(var deliverer in context.DELIVERERS)
                {
                    var delivererItem = context.DELIVERERS.Find(deliverer.DELIVERER_ID);
                    deliverers.Add(new DELIVERER() { DELIVERER_NAME = delivererItem.DELIVERER_NAME, DELIVERER_ID = delivererItem.DELIVERER_ID });
                }
                foreach(var customerOrder in context.INFOORDERCUSTOMERs)
                {
                    var customerOrderItem = context.INFOORDERCUSTOMERs.Find(customerOrder.ORDER_ID);
                    customerOrders.Add(new INFOORDERCUSTOMER() { ORDER_DATE = customerOrderItem.ORDER_DATE, ORDER_ID = customerOrderItem.ORDER_ID });
                    storeOrders.Add(new INFOSTOREORDER() { ORDER_DATE = customerOrderItem.ORDER_DATE, ORDER_ID = customerOrderItem.ORDER_ID });
                }
            }
            pencilsList.ItemsSource = pencils;
            canvasesList.ItemsSource = canvases;
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
                        var o = context.INFOSTOREORDERs.Find(order.ORDER_ID);
                        context.INFOORDERCUSTOMERs.Attach(order);
                        context.INFOORDERCUSTOMERs.Remove(order);
                        context.INFOSTOREORDERs.Attach(o);
                        context.INFOSTOREORDERs.Remove(o);
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
            var storeOrder = new INFOSTOREORDER();
            var cOrderList = new List<CUSTOMERORDER>();
            var sOrderList = new List<STOREORDER>();
            using (var context = new Entities())
            {
                if (context.INFOORDERCUSTOMERs.Any())
                {
                    decimal id = context.INFOORDERCUSTOMERs.Max(p => p.ORDER_ID);
                    customerOrder.ORDER_ID = id + 1;
                    storeOrder.ORDER_ID = id + 1;
                }
                foreach(var item in productsInOrder)
                {
                    cOrderList.Add(new CUSTOMERORDER() { NUMBER_OF_PRODUCTS = item.NUMBER_OF_PRODUCTS, PRODUCT_ID = item.PRODUCT_ID, ORDER_ID = customerOrder.ORDER_ID });
                    sOrderList.Add(new STOREORDER() { NUMBER_OF_PRODUCTS = item.NUMBER_OF_PRODUCTS, PRODUCT_ID = item.PRODUCT_ID, ORDER_ID = customerOrder.ORDER_ID });
                }
            }
            customerOrder.ORDER_DATE = (System.DateTime)orderDate.SelectedDate;
            storeOrder.ORDER_DATE = (System.DateTime)orderDate.SelectedDate;
            customerOrder.SHIPPING_DATE = (System.DateTime)orderShippingDate.SelectedDate;
            storeOrder.DELIVERY_DATE = (System.DateTime)deliverDate.SelectedDate;
            customerOrder.CUSTOMER_ID = ((CUSTOMER)customersOrder.SelectedItem).CUSTOMER_ID;
            storeOrder.DELIVERER_ID = ((DELIVERER)deliverersOrder.SelectedItem).DELIVERER_ID;
            try
            {
                customerOrder.SUMMARY_DISCOUNT = decimal.Parse(summaryDiscount.Text);
            }
            catch(Exception exc)
            {
                MessageBox.Show("Invalid data", "Summary Discount");
            }
            using (var context = new Entities())
            {
                context.INFOORDERCUSTOMERs.Add(customerOrder);
                context.INFOSTOREORDERs.Add(storeOrder);
                foreach(var itemC in cOrderList)
                {
                    context.CUSTOMERORDERS.Add(itemC);
                }
                foreach(var itemS in sOrderList)
                {
                    context.STOREORDERS.Add(itemS);
                }
                var item = context.INFOORDERCUSTOMERs.Find(customerOrder.ORDER_ID);
                customerOrders.Add(new INFOORDERCUSTOMER() { ORDER_DATE = item.ORDER_DATE, ORDER_ID = item.ORDER_ID });
                productsInOrder.Clear();
                context.SaveChanges();
            }
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(OrdersTab.IsSelected)
            {
                customersOrder.ItemsSource = customers;
                deliverersOrder.ItemsSource = deliverers;
                using(var context = new Entities())
                {
                    foreach(var item in context.PRODUCTS)
                    {
                        products.Add(new PRODUCT() { PRODUCT_ID = item.PRODUCT_ID, PRODUCT_NAME = item.PRODUCT_NAME });
                    }
                }
                productsOrder.ItemsSource = products;
            }
            if(!OrdersTab.IsSelected)
            {
                products.Clear();
            }
        }

        private void addNewProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                productsInOrder.Add(new CUSTOMERORDER() { PRODUCT_ID = ((PRODUCT)productsOrder.SelectedItem).PRODUCT_ID, NUMBER_OF_PRODUCTS = decimal.Parse(productsAmount.Text) });
            }
            catch(Exception exc)
            {
                MessageBox.Show("Invalid data");
            }
            using(var context = new Entities())
            {
                var item = context.PRODUCTS.Find(((PRODUCT)productsOrder.SelectedItem).PRODUCT_ID);
                MessageBox.Show("Product " + item.PRODUCT_NAME + " was added to order");
            }
            products.Clear();
        }
    }
}


