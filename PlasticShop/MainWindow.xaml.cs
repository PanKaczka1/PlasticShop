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
        public MainWindow()
        {
            InitializeComponent();
            pencils = new ObservableCollection<PRODUCT>();
            canvases = new ObservableCollection<PRODUCT>();
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
            }
            pencilsList.ItemsSource = pencils;
            canvasesList.ItemsSource = canvases;
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
                MessageBox.Show("Invalid data", "Graphite grade");
                return;
            }
            try
            {
                canvas.MATERIAL = canvasMaterial.Text;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Invalid data", "Graphite grade");
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
    }
}


