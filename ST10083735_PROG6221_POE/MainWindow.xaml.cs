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
                accomodationpnl.Visibility = Visibility.Visible;
            }
            else
            {
                //Display an error if the values are in the incorrect format.
                expenseErrorlb.Visibility = Visibility.Visible;

            }

            //Send values to the Income constructor
            addIncome = new IncomeTax(income, tax);

        }

        private void nextpbx2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            double groceries = 0;
            double water = 0;
            double travel = 0;
            double other = 0;
            double cell = 0;

            //Since the values are in the correct format they can be stored in variables
            groceries = Convert.ToDouble(groceriestxt.Text);
            water = Convert.ToDouble(watertxt.Text);
            travel = Convert.ToDouble(traveltxt.Text);
            other = Convert.ToDouble(othertxt.Text);
            cell = Convert.ToDouble(celltxt.Text);

            double monthlyRent = 0;
            double purchasePrice = 0;
            double deposit = 0;
            double interestRate = 0;
            int monthsToRepay = 0;

            bool rentValid = false;
            bool purchasePriceValid = false;
            bool depositValid = false;
            bool interestRateValid = false;


            amountslb.Content = "";
            summaryInfolb.Content = "";

            if (rentgrp.IsChecked == false && buygrp.IsChecked == false)
            {
                //If the user has not chosen an accomodation type then an error is displayed
                choiceError.Visibility = Visibility.Visible;
            }
            else
            {
                choiceError.Visibility = Visibility.Hidden;
                //Check the format depending on which type of accomodation is chosen
                if (rentgrp.IsChecked == true)
                {
                    //Check if format is valid
                    rentValid = newData.isDecimalValid(renttxt.Text);

                }
                else
                {
                    //Check if format is valid
                    purchasePriceValid = newData.isDecimalValid(purchasetxt.Text);


                    depositValid = newData.isDecimalValid(deposittxt.Text);


                    interestRateValid = newData.isDecimalValid(interesttxt.Text);

                }

                //If it is valid convert the values to doubles
                if (rentValid || (purchasePriceValid && depositValid && interestRateValid))
                {
                    if (rentgrp.IsChecked == true)
                    {
                        //Since rent is valid it can be stored
                        monthlyRent = Convert.ToDouble(renttxt.Text);

                        //Send values to the rent constructor
                        addRentExpense = new Rent(groceries, water, travel, cell, other, monthlyRent);
                    }
                    else if (buygrp.IsChecked == true)
                    {
                        //All buying values are valid, so they can be stored in variables
                        purchasePrice = Convert.ToDouble(purchasetxt.Text);
                        interestRate = Convert.ToDouble(interesttxt.Text);
                        deposit = Convert.ToDouble(deposittxt.Text);
                        monthsToRepay = Convert.ToInt32(monthspn.Value);

                        //Send values to the home loan constructor
                        addBuyingExpense = new HomeLoan(groceries, water, travel, cell, other, purchasePrice, deposit, interestRate, monthsToRepay);

                        //Call method from HomeLoan class to check if the monthly repayment is greater
                        //than one 3rd of the users income

                        bool isGreater = addBuyingExpense.isMoreThanAThird(addIncome.GrossIncome);

                        //If it is the the user will get a notification
                        if (isGreater == true)
                        {
                            //Add date to the notification
                            DateTime now = DateTime.Now;
                            notificationInfolb.Content = "Your monthly repayment is greater than " +
                                "\na third of your income. Unfortunately, it is unlikey the loan will be approved.\n" + now.ToString("F");
                            MessageBox.Show("You have a new notification", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }


                    //Move to vehicle panel 
                    accomodationpnl.Visibility = Visibility.Hidden;
                    //vehiclepnl.Visible = true;

                }
                else
                {
                    //Display error on label
                    accomErrorlb.Visibility = Visibility.Visible;
                    return;

                }



            }


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

            //add date to the notification
            DateTime now = DateTime.Now;

            switch (notificationInfolb.Content)
            {
                //Update the times on the labels
                case string info when (info.Contains("Nothing here yet.")):
                    notificationInfolb.Content = "Nothing here yet.\n" + now.ToString("F");
                    break;
                case string info when (info.Contains("Your total expenses exceed 75% of your income.")):
                    notificationInfolb.Content = "Your total expenses exceed 75% of your income.\n" + now.ToString("F");
                    break;
                case string info when (info.Contains("Your monthly repayment is greater than ")):
                    notificationInfolb.Content = "Your monthly repayment is greater than " +
                        "\na third of your income. Unfortunately, it is unlikey the loan will be approved.\n" + now.ToString("F");
                    break;
            }

            switch (notiflb2.Content)
            {
                case string info when (info.Contains("Your total expenses exceed 75% of your income.")):
                    notiflb2.Content = "Your total expenses exceed 75% of your income.\n" + now.ToString("F");
                    break;

            }
        }

        private void exitNotifpb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            homepnl.Visibility = Visibility.Visible;
            notificationpnl.Visibility = Visibility.Hidden;
        }

        private void exitSearchpbx_MouseDown(object sender, MouseButtonEventArgs e)
        {          
            resultNamelb.Visibility = Visibility.Hidden;
            resultCostlb.Visibility = Visibility.Hidden;
            searchResultspbx.Visibility = Visibility.Hidden;
            searchpnl.Visibility = Visibility.Hidden ;
            homepnl.Visibility = Visibility.Visible;
            searchtxt.Text = "";
            aboutrtb.Visibility = Visibility.Hidden;
        }
        private Expense ReturnChosenObj()
        {
            Rent ifObjectWasNotCreated = new Rent(0, 0, 0, 0, 0, 0);
            if (rentgrp.IsChecked == true)
            {
                return addRentExpense;
            }
            else if (buygrp.IsChecked == true)
            {
                return addBuyingExpense;
            }
            else
            {
                return ifObjectWasNotCreated;
            }


        }


        private void searchFunction()
        {
            noResultslb.Visibility = Visibility.Hidden;

            resultslb.Content = "Results for";
            string search;
            //check if the search is valid
            bool isSearchValid = newData.onlyLetters(searchtxt.Text);
            if (isSearchValid)
            {
                search = searchtxt.Text.ToLower();

                resultslb.Content += " '" + searchtxt.Text + "'";
                //case statements for the various things a user can search for
                switch (search)
                {
                    case string search1 when search1.Contains("water") || search1.Contains("lights") || search1.Contains("util"):
                        //change the heading of the expense depending on what the user chose
                        resultNamelb.Content = "Water and lights";
                        resultCostlb.Content = "R" + ReturnChosenObj().Utilities.ToString();
                        //make search components visible
                        showSearchResults();
                        //cala=culate the percentage of income spent on the expense
                        expensePercentageOfIncome(ReturnChosenObj().Utilities);
                        break;
                    case string search1 when search1.Contains("income") || search1.Contains("salary"):
                        resultNamelb.Content = "Income";
                        resultCostlb.Content = "R" + addIncome.GrossIncome.ToString();
                        showSearchResults();
                        aboutrtb.Visibility = Visibility.Hidden;
                        break;
                    case string search1 when search1.Contains("groceries"):
                        resultNamelb.Content = "Groceries";
                        resultCostlb.Content = "R" + ReturnChosenObj().Groceries.ToString();
                        showSearchResults();
                        expensePercentageOfIncome(ReturnChosenObj().Groceries);
                        break;
                    case string search1 when search1.Contains("phone") || search1.Contains("cel") || search1.Contains("tel"):
                        resultNamelb.Content = "Cellphone/telephone";
                        resultCostlb.Content = "R" + ReturnChosenObj().Phone.ToString();
                        showSearchResults();
                        expensePercentageOfIncome(ReturnChosenObj().Phone);
                        break;
                    case string search1 when search1.Contains("travel") || search1.Contains("petrol"):
                        resultNamelb.Content = "Travel(incl petrol)";
                        resultCostlb.Content = "R" + ReturnChosenObj().Travel.ToString();
                        showSearchResults();
                        expensePercentageOfIncome(ReturnChosenObj().Travel);
                        break;
                    case string search1 when search1.Contains("other"):
                        resultNamelb.Content = "Other";
                        resultCostlb.Content = "R" + ReturnChosenObj().Other.ToString();
                        showSearchResults();
                        expensePercentageOfIncome(ReturnChosenObj().Other);
                        break;
                    case string search1 when search1.Contains("tax"):
                        resultNamelb.Content = "Tax";
                        resultCostlb.Content = "R" + addIncome.Tax.ToString();
                        showSearchResults();
                        expensePercentageOfIncome(addIncome.Tax);
                        break;
                    case string search1 when search1.Contains("rent") && rentgrp.IsChecked == true:
                        resultNamelb.Content = "Monthly Rent";
                        resultCostlb.Content = "R" + addRentExpense.MonthlyRent.ToString();
                        showSearchResults();
                        expensePercentageOfIncome(addRentExpense.MonthlyRent);

                        break;
                    case string search1 when (search1.Contains("house deposit") || search1.Contains("home deposit")) && buygrp.IsChecked == true:
                        resultNamelb.Content = "Home Deposit";
                        resultCostlb.Content = "R" + addBuyingExpense.TotalDeposit.ToString();
                        showSearchResults();
                        expensePercentageOfIncome(addBuyingExpense.TotalDeposit);
                        break;
                    case string search1 when (search1.Contains("home repay") || search1.Contains("house repay")) && buygrp.IsChecked == true:
                        resultNamelb.Content = "Home Repayment";
                        resultCostlb.Content = "R" + addBuyingExpense.calcMonthlyRepayment().ToString();
                        showSearchResults();
                        expensePercentageOfIncome(addBuyingExpense.calcMonthlyRepayment());
                        break;
                    //case string search1 when (search1.Contains("car deposit") || search1.Contains("vehicle deposit")) && yesrbtn.Checked == true:
                    //    resultNamelb.Content = "Vehicle Deposit";
                    //    resultCostlb.Content = "R" + addVehicle.Deposit.ToString();
                    //    showSearchResults();
                    //    expensePercentageOfIncome(addVehicle.Deposit);
                    //    break;
                    //case string search1 when (search1.Contains("car payment") || search1.Contains("vehicle pay") || search1.Contains("car repay") || search1.Contains("vehicle repayment")) && yesrbtn.Checked == true:
                    //    resultNamelb.Content = "Vehicle Repayment";
                    //    resultCostlb.Content = "R" + addVehicle.totalMonthlyCost();
                    //    showSearchResults();
                    //    expensePercentageOfIncome(addVehicle.totalMonthlyCost());
                    //    break;
                    //case string search1 when (search1.Contains("insurance")) && yesrbtn.Checked == true:
                    //    resultNamelb.Content = "Vehicle Insurance";
                    //    resultCostlb.Content = "R" + addVehicle.EstInsurancePremium.ToString();
                    //    showSearchResults();
                    //    expensePercentageOfIncome(addVehicle.EstInsurancePremium);
                    //    break;
                    default:
                        //if the search does not match anything show that there are no results
                        noResultslb.Visibility = Visibility.Visible;

                        break;
                }

                //display the search panel
                homepnl.Visibility = Visibility.Hidden;
                searchpnl.Visibility = Visibility.Visible;

            }
            else
            {
                //if the user's search contains numbers/special characters except a space it is invalid and an error will be shown
                MessageBox.Show("The term you entered is invalid. Please try again.", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                searchtxt.Text = "";
            }
        }

        private void searchpbx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            searchFunction();
        }

        //Calculate the percentage of income that is spent on an expense
        private void expensePercentageOfIncome(double expense)
        {
            double percentage = 0;
            percentage = (expense / (addIncome.GrossIncome)) * 100;
            percentage = Math.Round(percentage, 2);
            if (percentage.Equals(double.NaN))
            {
                percentage = 0;
            }
            aboutrtb.Document.Blocks.Clear();
            //aboutrtb.AppendText("You have spent ");
            ////display the percentage in a different color and font
            //aboutrtb.Selection = Color.FromRgb(96, 182, 79);
            //aboutrtb.SelectionFont = new Font("Verdana", 12, FontStyle.Bold);
            //aboutrtb.AppendText(percentage + "% ");
            //aboutrtb.SelectionColor = Color.Black;
            //aboutrtb.SelectionFont = new Font("Calibri Light", 13);
            //aboutrtb.AppendText("of your income on this expense.");

            TextRange rangeOfText1 = new TextRange(aboutrtb.Document.ContentEnd, aboutrtb.Document.ContentEnd);
            rangeOfText1.Text = "You have spent ";
            rangeOfText1.ApplyPropertyValue(TextElement.ForegroundProperty,Brushes.Black );
           

            TextRange rangeOfWord = new TextRange(aboutrtb.Document.ContentEnd, aboutrtb.Document.ContentEnd);
            rangeOfWord.Text = percentage + "% ";
            rangeOfWord.ApplyPropertyValue(TextElement.ForegroundProperty, Color.FromRgb(96, 182, 79));
            rangeOfWord.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);

            TextRange rangeOfText2 = new TextRange(aboutrtb.Document.ContentEnd, aboutrtb.Document.ContentEnd);
            rangeOfText2.Text = "of your income on this expense.";
            rangeOfText2.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
           
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

        private void editPurchasePricepbx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            editFigure(purchaseCostlb, purchasetxt);
            purchasetxt.Focus();
        }

        private void editDepositpbx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            editFigure(depositCostlb, deposittxt);
        }

        private void editInterestpbx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            editFigure(interestlb, interesttxt);
            interestlb.Content = interesttxt.Text + "%";
        }

        private void editMonthspbx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (monthspn.Visibility == Visibility.Visible)
            {
                monthspn.Visibility = Visibility.Hidden;
                monthlb.Content = monthspn.Value + " months";
                monthlb.Visibility = Visibility.Visible;
            }
            else
            {
                monthlb.Visibility = Visibility.Hidden;
                monthspn.Visibility = Visibility.Visible;
            }
        }

        private void purchasetxt_KeyDown(object sender, KeyEventArgs e)
        {
            enterAmount(e, purchaseCostlb, purchasetxt);
        }

        private void deposittxt_KeyDown(object sender, KeyEventArgs e)
        {
            enterAmount(e, depositCostlb, deposittxt);
        }

        private void interesttxt_KeyDown(object sender, KeyEventArgs e)
        {
            enterAmount(e, interestlb, interesttxt);
            interestlb.Content = interesttxt.Text + "%";
        }

        private void monthspn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                monthspn.Visibility = Visibility.Hidden;
                monthlb.Visibility = Visibility.Visible;
            }
        }

        private void rentgrp_Checked(object sender, RoutedEventArgs e)
        {
            if (rentgrp.IsChecked == true)
            {
                buypnl.Visibility = Visibility.Hidden;
                rentpnl.Visibility = Visibility.Visible;
            }
            else
            {
                rentpnl.Visibility = Visibility.Hidden;
                renttxt.Text = "0";
                rentCostlb.Content = "R0";
            }
        }

        private void buygrp_Checked(object sender, RoutedEventArgs e)
        {
            if (buygrp.IsChecked == true)
            {
                rentpnl.Visibility = Visibility.Hidden;
                buypnl.Visibility = Visibility.Visible;
            }
            else
            {
                //set all the values to R0 if they switch choices
                buypnl.Visibility = Visibility.Hidden;
                purchaseCostlb.Content = "R0";
                purchasetxt.Text = "0";

                depositCostlb.Content = "R0";
                deposittxt.Text = "0";

                interestlb.Content = "0%";
                interesttxt.Text = "0";

                monthlb.Content = "240 months";
                monthspn.Value = 240;

            }
        }

        private void previouspbx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            accomodationpnl.Visibility = Visibility.Hidden;
            expensepnl.Visibility = Visibility.Visible ;
        }

        private void editRentpbx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            editFigure(rentCostlb, renttxt);
        }

        private void renttxt_KeyDown(object sender, KeyEventArgs e)
        {
            enterAmount(e, rentCostlb, renttxt);
        }

        private void searchtxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                searchFunction();
            }
        }

        private void showSearchResults()
        {
            resultNamelb.Visibility = Visibility.Visible;
            resultCostlb.Visibility = Visibility.Visible;
            searchResultspbx.Visibility = Visibility.Visible;
            aboutrtb.Visibility = Visibility.Visible;
        }

        private void yesrbtn_Checked(object sender, RoutedEventArgs e)
        {
            if (yesrbtn.IsChecked == true)
            {
                vehiclePurchasepnl.Visibility = Visibility.Visible;
            }
            else
            {
                vehiclePurchasepnl.Visibility = Visibility.Hidden;
            }
        }
    }
}
