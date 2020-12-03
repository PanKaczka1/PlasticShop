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
    /// Interaction logic for ColourDetails.xaml
    /// </summary>
    public partial class ColourDetails : Window
    {
        COLOUR c;
        public ColourDetails(COLOUR colour)
        {
            InitializeComponent();
            using (var context = new Entities())
            {
                c = context.COLOURS.Find(colour.COLOUR_ID);
                colourName.Text = c.COLOUR_NAME;
                redValue.Text = c.RED_VALUE.ToString();
                greenValue.Text = c.GREEN_VALUE.ToString();
                blueValue.Text = c.BLUE_VALUE.ToString();
            }
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            using (var context = new Entities())
            {
                var colour = context.COLOURS.Find(c.COLOUR_ID);
                colour.COLOUR_NAME = colourName.Text;
                colour.RED_VALUE = int.Parse(redValue.Text);
                colour.GREEN_VALUE = int.Parse(greenValue.Text);
                colour.BLUE_VALUE = int.Parse(blueValue.Text);
                context.SaveChanges();
                this.Close();
            }
        }
    }
}
