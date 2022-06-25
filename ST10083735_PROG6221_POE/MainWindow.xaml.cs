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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ST10083735_PROG6221_POE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Create an object of the validation class
        ValidationMethods newData = new ValidationMethods();

        public MainWindow()
        {
            InitializeComponent();
            mainpnl.Visibility = Visibility.Visible;
            homepnl.Visibility = Visibility.Hidden;
        }

        private void startbtn_Click(object sender, RoutedEventArgs e)
        {
            mainpnl.Visibility = Visibility.Hidden;
            homepnl.Visibility = Visibility.Visible;

        }

        private void searchBarbtn_Click(object sender, RoutedEventArgs e)
        {
            searchtxt.Visibility = Visibility.Visible;
            searchtxt.Focus();
            searchBarbtn.IsEnabled = false;
        }

        private void exitimg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void bellpbx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            homepnl.Visibility = Visibility.Hidden;
            notificationpnl.Visibility = Visibility.Visible;
        }

        private void exitNotifpb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            homepnl.Visibility = Visibility.Visible;
            notificationpnl.Visibility = Visibility.Hidden;
        }

        private void exitSearchpbx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            homepnl.Visibility = Visibility.Visible;
            searchpnl.Visibility = Visibility.Hidden;
        }

        private void searchpbx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            homepnl.Visibility = Visibility.Hidden;
            searchpnl.Visibility = Visibility.Hidden;
        }

        //If the user hits the enter key the amount is displayed on the corresponding label
        private void enterAmount(KeyEventArgs e, Label costLabel, TextBox input)
        {
            if (e.Key == Key.Enter)
            {
                editFigure(costLabel, input);
            }
        }

        //Enables user to edit figures if they press the edit icon
        private void editFigure(Label costLabel, TextBox input)
        {
            if (input.Visibility == Visibility.Visible)
            {
                input.Visibility = Visibility.Hidden;
                input.Text = newData.isNull(input.Text);
                costLabel.Content = "R" + newData.isNull(input.Text);
                costLabel.Visibility = Visibility.Visible;

            }
            else
            {
                costLabel.Visibility = Visibility.Hidden;
                input.Visibility = Visibility.Visible;
                input.Focus();

            }
        }

        private void editincomepbx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            editFigure(incomeCostlb, incometxt);
        }

        private void incometxt_KeyDown(object sender, KeyEventArgs e)
        {
            enterAmount(e, incomeCostlb, incometxt);
        }

        private void editTaxpbx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            editFigure(taxCostlb, taxtxt);
        }

        private void editGroceriespbx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            editFigure(groceriesCostlb, groceriestxt);
        }

        private void editWaterpbx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            editFigure(waterCostlb, watertxt);
        }

        private void editTravelpbx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            editFigure(travelCostlb, traveltxt);
        }

        private void editCellpbx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            editFigure(cellCostlb, celltxt);
        }

        private void editOtherpbx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            editFigure(otherCostlb, othertxt);
        }

        private void taxtxt_KeyDown(object sender, KeyEventArgs e)
        {
            enterAmount(e, taxCostlb, taxtxt);
        }

        private void groceriestxt_KeyDown(object sender, KeyEventArgs e)
        {
            enterAmount(e, groceriesCostlb, groceriestxt);
        }

        private void watertxt_KeyDown(object sender, KeyEventArgs e)
        {
            enterAmount(e, waterCostlb, watertxt);
        }

        private void traveltxt_KeyDown(object sender, KeyEventArgs e)
        {
            enterAmount(e, travelCostlb, traveltxt);
        }

        private void celltxt_KeyDown(object sender, KeyEventArgs e)
        {
            enterAmount(e, cellCostlb, celltxt);
        }

        private void othertxt_KeyDown(object sender, KeyEventArgs e)
        {
            enterAmount(e, otherCostlb, othertxt);
        }
    }
}
