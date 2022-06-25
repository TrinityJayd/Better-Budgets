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
        //This dictionary contains the expenses the user inputs as well as the names of the expenses
        Dictionary<string, double> expenses = new Dictionary<string, double>();

        //Create an object of the validation class
        ValidationMethods newData = new ValidationMethods();

        //Create objects tof the different expense classes
        IncomeTax addIncome = new IncomeTax(0, 0);
        HomeLoan addBuyingExpense = new HomeLoan(0, 0, 0, 0, 0, 0, 0, 0, 0);
        Rent addRentExpense = new Rent(0, 0, 0, 0, 0, 0);
        Vehicle addVehicle = new Vehicle("", 0, 0, 0, 0); 

        public MainWindow()
        {
            InitializeComponent();
            mainpnl.Visibility = Visibility.Visible;
            homepnl.Visibility = Visibility.Hidden;
        }


        private void nextpbx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            double income = 0;
            double tax = 0;

            //Check if the decimal inputs are valid
            bool incomeValid = newData.isDecimalValid(incometxt.Text);

            bool taxValid = newData.isDecimalValid(taxtxt.Text);

            bool groceriesValid = newData.isDecimalValid(groceriestxt.Text);

            bool waterValid = newData.isDecimalValid(watertxt.Text);

            bool travelValid = newData.isDecimalValid(traveltxt.Text);

            bool otherValid = newData.isDecimalValid(othertxt.Text);

            bool cellValid = newData.isDecimalValid(celltxt.Text);



            //If all values are valid in their format then store them in variables
            if (incomeValid & taxValid & waterValid & travelValid & otherValid & cellValid & groceriesValid)
            {
                income = Convert.ToDouble(incometxt.Text);
                tax = Convert.ToDouble(taxtxt.Text);

                //Display the accomodation panelso the user can input values
                expenseErrorlb.Visibility = Visibility.Hidden;
                expensepnl.Visibility = Visibility.Hidden;
                //accomodationpnl.Visible = true;
            }
            else
            {
                //Display an error if the values are in the incorrect format.
                expenseErrorlb.Visibility = Visibility.Visible;

            }

            //Send values to the Income constructor
            addIncome = new IncomeTax(income, tax);

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

        private void expenseFormpb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            homepnl.Visibility = Visibility.Hidden;
            expensepnl.Visibility = Visibility.Visible;
        }
    }
}
