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
    /// Interaction logic for NotebookDetails.xaml
    /// </summary>
    public partial class NotebookDetails : Window
    {
        private PRODUCT p;
        public NotebookDetails(PRODUCT product)
        {
            InitializeComponent();
            using (var context = new Entities())
            {
                p = context.PRODUCTS.Find(product.PRODUCT_ID);
                var notebook = new NOTEBOOK();
                notebook = context.NOTEBOOKS.Find(product.PRODUCT_ID);
                NotebookName.Text = p.PRODUCT_NAME;
                NotebookInStock.Text = p.PRODUCTS_IN_STOCK.ToString();
                discountNotebook.Text = p.DISCOUNT.ToString();
                priceNotebook.Text = p.PRICE.ToString();
                producerNotebook.Text = p.PRODUCER;
                paperType.Text = notebook.PAPER_TYPE;
                notebookSize.Text = notebook.NOTEBOOK_SIZE;
                notebookPages.Text = notebook.NOTEBOOK_PAGES.ToString();
                grammage.Text = notebook.GRAMMAGE.ToString();
                hardcover.Text = notebook.HARDCOVER;
            }

        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Entities())
            {
                var product = context.PRODUCTS.Find(p.PRODUCT_ID);
                var notebook = context.NOTEBOOKS.Find(p.PRODUCT_ID);
                if (string.IsNullOrEmpty(NotebookName.Text))
                {
                    MessageBox.Show("Invalid data", "Name");
                    return;
                }
                else
                {
                    try
                    {
                        product.PRODUCT_NAME = NotebookName.Text;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Invalid data", "Name");
                        return;
                    }
                }
                try
                {
                    product.PRODUCTS_IN_STOCK = int.Parse(NotebookInStock.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid data", "Products in stock");
                    return;
                }
                try
                {
                    product.DISCOUNT = int.Parse(discountNotebook.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalide data", "Discount");
                    return;
                }
                try
                {
                    product.PRICE = decimal.Parse(priceNotebook.Text);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Invalid data", "Price");
                    return;
                }
                if (string.IsNullOrEmpty(producerNotebook.Text))
                {
                    MessageBox.Show("Invalid data", "Producer");
                    return;
                }
                else
                {
                    try
                    {
                        product.PRODUCER = producerNotebook.Text;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Invalid data", "Producer");
                        return;
                    }
                }
                if (string.IsNullOrEmpty(paperType.Text))
                {
                    MessageBox.Show("Invalid data", "Crayon Type");
                    return;
                }
                else
                {
                    try
                    {
                        notebook.PAPER_TYPE = paperType.Text;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Invalid data", "Crayon Type");
                        return;
                    }
                }
                if (string.IsNullOrEmpty(notebookSize.Text))
                {
                    MessageBox.Show("Invalid data", "Crayon Type");
                    return;
                }
                else
                {
                    try
                    {
                        notebook.NOTEBOOK_SIZE = notebookSize.Text;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Invalid data", "Crayon Type");
                        return;
                    }
                }
                if (string.IsNullOrEmpty(hardcover.Text))
                {
                    MessageBox.Show("Invalid data", "Crayon Type");
                    return;
                }
                else
                {
                    try
                    {
                        notebook.HARDCOVER = hardcover.Text;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Invalid data", "Crayon Type");
                        return;
                    }
                }
                try
                {
                    notebook.NOTEBOOK_PAGES = decimal.Parse(notebookPages.Text);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Invalid data", "Price");
                    return;
                }
                try
                {
                    notebook.GRAMMAGE = decimal.Parse(grammage.Text);
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
