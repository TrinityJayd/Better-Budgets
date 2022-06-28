
using Syncfusion.UI.Xaml.SunburstChart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace ST10083735_PROG6221_POE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Create a model for the data to be displayed on the chart
        MainViewModel newModel = new MainViewModel();

        //This dictionary contains the expenses the user inputs as well as the names of the expenses
        Dictionary<string, double> expenses = new Dictionary<string, double>();

        //Create an object of the validation class
        ValidationMethods newData = new ValidationMethods();

        //Create objects tof the different expense classes
        IncomeTax addIncome = new IncomeTax(0, 0);
        HomeLoan addBuyingExpense = new HomeLoan(0, 0, 0, 0, 0, 0, 0, 0, 0);
        Rent addRentExpense = new Rent(0, 0, 0, 0, 0, 0);
        Vehicle addVehicle = new Vehicle("", 0, 0, 0, 0);
        Savings addSavings = new Savings("", 0, 0, 0);

        public MainWindow()
        {
            InitializeComponent();
            mainpnl.Visibility = Visibility.Visible;
            homepnl.Visibility = Visibility.Hidden;

        }


        //The first button the user will click on which will save their input and move on to the accomodation panel
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

        //This button will save the users input then take them to the vehicles panel
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

            //initialize values to 0
            double monthlyRent = 0;
            double purchasePrice = 0;
            double deposit = 0;
            double interestRate = 0;
            int monthsToRepay = 0;

            bool rentValid = false;
            bool purchasePriceValid = false;
            bool depositValid = false;
            bool interestRateValid = false;


            

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
                    vehiclepnl.Visibility = Visibility.Visible;

                }
                else
                {
                    //Display error on label
                    accomErrorlb.Visibility = Visibility.Visible;
                    return;

                }



            }


        }


        //This button will save the users input then display the savings panel
        private void nextpbx3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            modelErrorlb.Visibility = Visibility.Hidden;
            vehicleFormatError.Visibility = Visibility.Hidden;
            //If the user hasnt chosen yes or no an error will be displayed
            if (yesrbtn.IsChecked == false && norbtn.IsChecked == false)
            {
                vehicleErrorlb.Visibility = Visibility.Visible;
            }
            else
            {
                vehicleErrorlb.Visibility = Visibility.Hidden;

                //If they have chosen yes then check if the values are in the correct format
                if (yesrbtn.IsChecked == true)
                {
                    string modelAndMake = modeltxt.Text;
                    //Check that the data is in valid format
                    bool vehiclePurchasePriceValid = newData.isDecimalValid(vehicleCosttxt.Text);
                    bool vehicleDepositValid = newData.isDecimalValid(vehicleDeposittxt.Text);
                    bool vehicleInterestValid = newData.isDecimalValid(vehicleInteresttxt.Text);
                    bool vehicleInsuranceValid = newData.isDecimalValid(insurancetxt.Text);


                    if (modelAndMake == null || modelAndMake == "")
                    {
                        modelErrorlb.Visibility = Visibility.Visible;
                        return;
                    }
                    //if the values are not valid, the error will be displayed
                    else if (vehiclePurchasePriceValid == false || vehicleDepositValid == false || vehicleInterestValid == false || vehicleInsuranceValid == false)
                    {
                        vehicleFormatError.Visibility = Visibility.Visible;
                        return;
                    }
                    else
                    {
                        modelErrorlb.Visibility = Visibility.Hidden;
                        vehicleFormatError.Visibility = Visibility.Hidden;

                        //if the values are valid then they are stored
                        double vehiclePrice = Convert.ToDouble(vehicleCosttxt.Text);
                        double vehicleDeposit = Convert.ToDouble(vehicleDeposittxt.Text);
                        double vehicleInterest = Convert.ToDouble(vehicleInteresttxt.Text);
                        double vehicleInsurance = Convert.ToDouble(insurancetxt.Text);

                        //send values to the vehicle constructor
                        addVehicle = new Vehicle(modelAndMake, vehiclePrice, vehicleDeposit, vehicleInterest, vehicleInsurance);


                        //display the savings panel
                        vehiclepnl.Visibility = Visibility.Hidden;
                        savingspnl.Visibility = Visibility.Visible;


                    }
                }
                else
                {



                    //display the home panel
                    vehiclepnl.Visibility = Visibility.Hidden;
                    savingspnl.Visibility = Visibility.Visible;


                }


            }
        }

        //Once the user has completed all inputs, they will then be redirected to the home panel, where they can
        //view a summary of their expenses
        private void completebtn_Click(object sender, RoutedEventArgs e)
        {
            reasonErrorlb.Visibility = Visibility.Hidden;
            savingsFormatErrorlb.Visibility = Visibility.Hidden;
            //If the user hasnt chosen yes or no an error will be displayed
            if (yesSavebtn.IsChecked == false && noSavebtn.IsChecked == false)
            {
                savingErrorlb.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                savingErrorlb.Visibility = Visibility.Hidden;

                //If they have chosen yes then check if the values are in the correct format
                if (yesSavebtn.IsChecked == true)
                {
                    string reason = reasontxt.Text;
                    //check if the inputs are in valid format
                    bool totalToSaveValid = newData.isDecimalValid(amounttxt.Text);
                    bool interestValid = newData.isDecimalValid(interestSavetxt.Text);

                    //check if they have added a reason
                    if (reason == null || reason == "")
                    {
                        reasonErrorlb.Visibility = Visibility.Visible;
                        return;
                    }
                    //if the values are not valid, the error will be displayed
                    else if (totalToSaveValid == false || interestValid == false)
                    {
                        savingsFormatErrorlb.Visibility = Visibility.Visible;
                        return;
                    }
                    else
                    {
                        reasonErrorlb.Visibility = Visibility.Hidden;
                        savingsFormatErrorlb.Visibility = Visibility.Hidden;

                        //if the values are valid then they are stored
                        double totalToSave = Convert.ToDouble(amounttxt.Text);
                        double interest = Convert.ToDouble(interestSavetxt.Text);
                        int years = Convert.ToInt32(yearsSpn.Value);


                        //send values to the savings constructor
                        addSavings = new Savings(reason, totalToSave, years, interest);

                        //the complete analyis method will display the expenses and calculate how much the user has spent
                        completeAnalysis();

                        //display the home panel                        
                        savingspnl.Visibility = Visibility.Hidden;
                        homepnl.Visibility = Visibility.Visible;

                        //make the button that allows the user to order the expenses visible
                        Orderbtn.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    //the complete analyis method will display the expenses and calculate how much the user has spent
                    completeAnalysis();

                    //display the home panel
                    savingspnl.Visibility = Visibility.Hidden;
                    homepnl.Visibility = Visibility.Visible;

                    Orderbtn.Visibility = Visibility.Visible;
                }

                //delegate for the exceeds 75% method
                exceedsIncomeDel newDel = expensesExceed75Perc;

                //if the delegate returns true, the total expenses
                //exceed 75% of the total income
                if (newDel() == true)
                {
                    //If there are no notifications,  the notification can be displayed on the first
                    //notification label
                    String notifText = notificationInfolb.Content.ToString();
                    if (notifText.Contains("Nothing here yet."))
                    {
                        //add date and time to the notification
                        DateTime now = DateTime.Now;
                        notificationInfolb.Content = "Your total expenses exceed 75% of your income.\n" + now.ToString("F");

                        //make the notification visible
                        notifpbx2.Visibility = Visibility.Hidden;
                        notiflb2.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        //If there is already a notification being displayed 
                        //display the notification on the second label
                        //make the notification visible
                        notifpbx2.Visibility = Visibility.Visible;
                        notiflb2.Visibility = Visibility.Visible;

                        //Add date to the notification
                        DateTime now = DateTime.Now;
                        notiflb2.Content = "Your total expenses exceed 75% of your income.\n" + now.ToString("F");
                    }

                    //alert the user that they have a notification
                    MessageBox.Show("You have a new notification", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                }
                //Make graph button visible
                viewGraphbtn.Visibility = Visibility.Visible;

            }



        }

        

        //Create delegate for the function that
        //checks if the user's total expenses exceeds 75% of their income
        public delegate bool exceedsIncomeDel();

        //Calculate if total expenses exceed 75% of income
        private bool expensesExceed75Perc()
        {
            //The program needs to check if the expenses exceed 75% of the total income
            //Therefore the threshold is 75
            const double THRESHOLD = 0.75;

            //To calculate the total expenses call the sum expenses function which will
            //Calculate and return the total expenses in the expense dictionary
            double totalExpenses = sumExpenseList();

            //Calculate 75% of the income the user has provided
            double percOfIncome75 = addIncome.GrossIncome * THRESHOLD;

            //If the expenses exceed 75% of the income return true
            if (totalExpenses > percOfIncome75)
            {
                return true;
            }
            else
            {
                //else return false
                return false;
            }

        }

        private void completeAnalysis()
        {
            
            //populate the dictionary
            fillDictionary();

            //sort and display the dictionary in descending order
            sortAndDisplayListDESC();

            //store the money left in a variable
            double moneyAfterExpenses = moneyLeft();

            //Display how much money is left over for the user 
            analysisCostsrtbx.Document.Blocks.Clear();
            TextRange rangeOfText1 = new TextRange(analysisCostsrtbx.Document.ContentEnd, analysisCostsrtbx.Document.ContentEnd);


            //Display a different message based on what the user has left over at the end of the month.
            if (moneyAfterExpenses == 0)
            {
                rangeOfText1.Text = "You have no money left for the month.";
                return;
            }
            else if (moneyAfterExpenses > 0)
            {
                rangeOfText1.Text = "You have ";
            }
            else
            {
                rangeOfText1.Text = "You have a negative balance of ";
            }

            //Customize the font/color of the text
            rangeOfText1.ApplyPropertyValue(TextElement.FontFamilyProperty, "Calibri Light");
            rangeOfText1.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);

            //Different colour and font for the amount
            SolidColorBrush mySolidColorBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#60b64f");

            TextRange rangeOfWord = new TextRange(analysisCostsrtbx.Document.ContentEnd, analysisCostsrtbx.Document.ContentEnd);

            //if they have a positive balance display the amount
            if (moneyAfterExpenses > 0)
            {
                rangeOfWord.Text = "R" + moneyAfterExpenses;
            }
            else
            {
                //if the value is negative find the absolute value
                rangeOfWord.Text = "- R" + Math.Abs(moneyAfterExpenses);
            }
            //Customize the font/color of the text
            rangeOfWord.ApplyPropertyValue(TextElement.ForegroundProperty, mySolidColorBrush);
            rangeOfWord.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.ExtraBold);
            rangeOfWord.ApplyPropertyValue(TextElement.FontFamilyProperty, "Verdana");

            //Chage the font colour
            TextRange rangeOfText2 = new TextRange(analysisCostsrtbx.Document.ContentEnd, analysisCostsrtbx.Document.ContentEnd);


            if (moneyAfterExpenses > 0)
            {
                rangeOfText2.Text = " left for the month.";
            }

            //store the total percentage the user spent in a variable
            double percentageSpent = percentageOfIncome();
            rangeOfText2.Text += "\nYou have spent ";
            //Customize the font/color of the text
            rangeOfText2.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
            rangeOfText2.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            rangeOfText2.ApplyPropertyValue(TextElement.FontFamilyProperty, "Calibri Light");

            //Code to make the percentage amount a different color and font
            TextRange textRange3 = new TextRange(analysisCostsrtbx.Document.ContentEnd, analysisCostsrtbx.Document.ContentEnd);
            textRange3.Text = percentageSpent + "% ";
            //Customize the font/color of the text
            textRange3.ApplyPropertyValue(TextElement.ForegroundProperty, mySolidColorBrush);
            textRange3.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.ExtraBold);
            textRange3.ApplyPropertyValue(TextElement.FontFamilyProperty, "Verdana");

            TextRange rangeOfText4 = new TextRange(analysisCostsrtbx.Document.ContentEnd, analysisCostsrtbx.Document.ContentEnd);
            rangeOfText4.Text = "of your income.";
            //Customize the font/color of the text
            rangeOfText4.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
            rangeOfText4.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            rangeOfText4.ApplyPropertyValue(TextElement.FontFamilyProperty, "Calibri Light");


        }

        //calculate in total how much of the income was spent on the expenses
        private double percentageOfIncome()
        {
            double percentage;

            //calculate the percentage of the users income that was spent on the expenses
            percentage = sumExpenseList() / (addIncome.GrossIncome / 100);

            //round of the value to 2 decimal places
            percentage = Math.Round(percentage, 2);


            return percentage;
        }

        private double sumExpenseList()
        {
            double totalExpenses = 0;
            //calculate the total expenses
            totalExpenses += expenses.Sum(x => x.Value);

            return totalExpenses;
        }



        //Add expenses to the array
        private void fillDictionary()
        {
            //Clear the dictionary to prevent the prgram from crashing when the same key is enetered
            expenses.Clear();

            // add expenses to the dictionary
            expenses.Add("Tax", addIncome.Tax);
            expenses.Add("Groceries", ReturnChosenObj().Groceries);
            expenses.Add("Utilities", ReturnChosenObj().Utilities);
            expenses.Add("Travel", ReturnChosenObj().Travel);
            expenses.Add("Phone", ReturnChosenObj().Phone);
            expenses.Add("Other", ReturnChosenObj().Other);


            //if the user chose rent add rent to the dictionary
            if (rentgrp.IsChecked == true)
            {
                expenses.Add("Monthly Rent", addRentExpense.MonthlyRent);

            }
            else
            {
                //if the user chose buy add home loan to the dictionary
                expenses.Add("Home Deposit", addBuyingExpense.TotalDeposit);
                double homeRepayment = addBuyingExpense.calcMonthlyRepayment();
                expenses.Add("Monthly Home Repayment", homeRepayment);

            }

            if (yesrbtn.IsChecked == true)
            {
                //if the user decided that they are buying a vehicle, add it to the dictionary
                double monthlyRepayment = addVehicle.totalCost();
                expenses.Add("Vehicle Deposit", addVehicle.Deposit);
                expenses.Add("Vehicle Repayment(incl. Insurance)", monthlyRepayment);

            }

            //if the user chose to save, add the values to the dictionary
            if (yesSavebtn.IsChecked == true)
            {
                double savingsPerMonth = addSavings.totalCost();
                expenses.Add("Savings Deposit Per Month", savingsPerMonth);
            }


        }

        //sort the dictionary in descending order
        private void sortAndDisplayListDESC()
        {

            summaryInfolb.Content = "";
            amountslb.Content = "";

            //Income will always be displayed at the top of the list
            summaryInfolb.Content = "Gross Income\n";
            amountslb.Content = "R" + addIncome.GrossIncome + "\n";
            foreach (KeyValuePair<string, double> number in expenses.OrderByDescending(key => key.Value))
            {
                summaryInfolb.Content += number.Key + "\n";
                amountslb.Content += "R" + number.Value + "\n";
            }



        }

        //sort the dictionary in ascending order
        private void sortListLowToHigh()
        {
            summaryInfolb.Content = "";
            amountslb.Content = "";

            //Income will always be displayed at the top of the list
            summaryInfolb.Content = "Gross Income\n";
            amountslb.Content = "R" + addIncome.GrossIncome + "\n";
            foreach (KeyValuePair<string, double> number in expenses.OrderBy(key => key.Value))
            {
                summaryInfolb.Content += number.Key + "\n";
                amountslb.Content += "R" + number.Value + "\n";
            }


        }

        private void Orderbtn_Click(object sender, RoutedEventArgs e)
        {
            //if the button caption is low to high and the user clicks it, display the dictionary in ascending order
            if (Orderbtn.Content.Equals("Low to High"))
            {
                sortListLowToHigh();
                //change the caption to high to low since it is already sorted in ascending order
                Orderbtn.Content = "High to Low";
            }
            else
            {
                //if the button caption is high to low and the user clicks it, display the dictionary in descending order
                sortAndDisplayListDESC();
                //change the caption to low to high since it is already sorted in descending order
                Orderbtn.Content = "Low to High";
            }
        }

        //Calculate how much money is left over for the user
        private double moneyLeft()
        {
            //store the total expenses in a variable
            double totalExpenses = sumExpenseList();
            double moneyLeftOver = 0;

            //calculate the money left over
            moneyLeftOver = addIncome.GrossIncome - totalExpenses;
            double roudedMoneyLeftOver = Math.Round(moneyLeftOver, 2);
            return roudedMoneyLeftOver;
        }

        //Add doughnut chart
        private void Canvas_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //Add data inputted by the user into the data model class
            populateData();

            //Code Attribution
            //Link: https://help.syncfusion.com/wpf/sunburst-chart/overview
            //Author: SyncFusion

            //Create the donut chart
            SfSunburstChart sunburst1 = new SfSunburstChart
            {
                ValueMemberPath = "Amount",
                Header = "Portion of Income Spent on Expenses",
                FontSize = 18,
                InnerRadius = 0.5,
                Height = 450,
                Width = 492,

            };


            //Add a custom color pallette
            SunburstColorModel colorModel = new SunburstColorModel();
            LinearGradientBrush brush1 = new LinearGradientBrush();
            brush1.GradientStops.Add(new GradientStop() { Color = Color.FromRgb(121, 173, 220), Offset = 0 });


            LinearGradientBrush brush2 = new LinearGradientBrush();
            brush2.GradientStops.Add(new GradientStop() { Color = Color.FromRgb(255, 192, 159), Offset = 0 });


            LinearGradientBrush brush3 = new LinearGradientBrush();
            brush3.GradientStops.Add(new GradientStop() { Color = Color.FromRgb(173, 247, 182), Offset = 0 });


            LinearGradientBrush brush4 = new LinearGradientBrush();
            brush4.GradientStops.Add(new GradientStop() { Color = Color.FromRgb(255, 136, 220), Offset = 0 });

            LinearGradientBrush brush5 = new LinearGradientBrush();
            brush5.GradientStops.Add(new GradientStop() { Color = Color.FromRgb(208, 0, 0), Offset = 0 });

            LinearGradientBrush brush6 = new LinearGradientBrush();
            brush6.GradientStops.Add(new GradientStop() { Color = Color.FromRgb(0, 56, 68), Offset = 0 });

            LinearGradientBrush brush7 = new LinearGradientBrush();
            brush7.GradientStops.Add(new GradientStop() { Color = Color.FromRgb(250, 188, 42), Offset = 0 });

            LinearGradientBrush brush8 = new LinearGradientBrush();
            brush8.GradientStops.Add(new GradientStop() { Color = Color.FromRgb(252, 170, 103), Offset = 0 });

            LinearGradientBrush brush9 = new LinearGradientBrush();
            brush9.GradientStops.Add(new GradientStop() { Color = Color.FromRgb(140, 83, 131), Offset = 0 });

            LinearGradientBrush brush10 = new LinearGradientBrush();
            brush10.GradientStops.Add(new GradientStop() { Color = Color.FromRgb(255, 231, 76), Offset = 0 });

            colorModel.CustomBrushes.Add(brush1);
            colorModel.CustomBrushes.Add(brush2);
            colorModel.CustomBrushes.Add(brush3);
            colorModel.CustomBrushes.Add(brush4);
            colorModel.CustomBrushes.Add(brush5);
            colorModel.CustomBrushes.Add(brush6);
            colorModel.CustomBrushes.Add(brush7);
            colorModel.CustomBrushes.Add(brush8);
            colorModel.CustomBrushes.Add(brush9);
            colorModel.CustomBrushes.Add(brush10);

            //Add the color model to the chart
            sunburst1.ColorModel = colorModel;
            sunburst1.Palette = SunburstColorPalette.Custom;

            //Bind the chart with the data
            sunburst1.SetBinding(SfSunburstChart.ItemsSourceProperty, "Data");
            sunburst1.Levels.Add(new SunburstHierarchicalLevel() { GroupMemberPath = "Expense" });

            //Chart Legend Customization
            SunburstLegend legend = new SunburstLegend
            {
                DockPosition = ChartDock.Left,
                FontSize = 16,
                ClickAction = LegendClickAction.ToggleSegmentVisibility
            };

            //Add legend to chart
            sunburst1.Legend = legend;

            //Add tool tips to chart
            SunburstToolTipBehavior tooltip = new SunburstToolTipBehavior();
            tooltip.ShowToolTip = true;
            sunburst1.Behaviors.Add(tooltip);

            sunburst1.DataContext = newModel;
            Canvas.SetLeft(sunburst1, 10);
            Canvas.SetTop(sunburst1, 91);
            analysispnl.Children.Add(sunburst1);


            //End Code attribution
        }

        public void populateData()
        {
            //Add user input data to the expense model to be displayed on the doughnut chart
            List<ExpensesModel> addData = new List<ExpensesModel>
                {
                    new ExpensesModel { Expense = "Tax",Amount = addIncome.Tax },
                    new ExpensesModel { Expense = "Groceries",Amount = ReturnChosenObj().Groceries },
                    new ExpensesModel { Expense = "Water and Lights", Amount = ReturnChosenObj().Utilities },
                    new ExpensesModel { Expense = "Travel", Amount = ReturnChosenObj().Travel },
                    new ExpensesModel { Expense = "Phone", Amount = ReturnChosenObj().Phone },
                    new ExpensesModel { Expense = "Other", Amount =  ReturnChosenObj().Other }
                };

            //Add rent data if the user chose to rent
            if (rentgrp.IsChecked == true)
            {

                addData.Add(new ExpensesModel { Expense = "Rent", Amount = addRentExpense.MonthlyRent });
            }
            else
            {
                //Add buy data if the user chose to buy
                addData.Add(new ExpensesModel { Expense = "Home Deposit", Amount = addBuyingExpense.TotalDeposit });
                addData.Add(new ExpensesModel { Expense = "Home Repayment", Amount = addBuyingExpense.calcMonthlyRepayment() });
            }

            //Add vehicle data if the user chose to buy a vehicle
            if (yesrbtn.IsChecked == true)
            {
                addData.Add(new ExpensesModel { Expense = "Vehicle Repayment", Amount = addVehicle.totalMonthlyCost() });
                addData.Add(new ExpensesModel { Expense = "Vehicle Deposit", Amount = addVehicle.Deposit });

            }

            //Add savings data if the user chose to save money
            if (yesSavebtn.IsChecked == true)
            {
                addData.Add(new ExpensesModel { Expense = "Savings Deposit(Per Month)", Amount = addSavings.totalCost() });
            }

            //add the list to the data model
            newModel.Data = addData;
        }

        

        private void bellpbx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //show  the notification panel
            homepnl.Visibility = Visibility.Hidden;
            notificationpnl.Visibility = Visibility.Visible;

            //add date to the notification
            DateTime now = DateTime.Now;

            //Update the times on the labels whenever the user clicks it
            switch (notificationInfolb.Content)
            {

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
                case string info when info.Contains("Your total expenses exceed 75% of your income."):
                    notiflb2.Content = "Your total expenses exceed 75% of your income.\n" + now.ToString("F");
                    break;

            }
        }

        //Return the object the user chose either rent or buying
        private Expense ReturnChosenObj()
        {
            Rent ifObjectWasNotCreated = new(0, 0, 0, 0, 0, 0);
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

        
        //Function called when the user either hits enter in the search text box or when they click on the search button
        private void searchFunction()
        {
            //Hide the label that says no results found
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
                        //calaculate the percentage of income spent on the expense
                        expensePercentageOfIncome(ReturnChosenObj().Utilities);
                        break;
                    case string search1 when search1.Contains("income") || search1.Contains("salary"):
                        //change the heading of the expense depending on what the user chose
                        resultNamelb.Content = "Income";
                        resultCostlb.Content = "R" + addIncome.GrossIncome.ToString();
                        //make search components visible
                        showSearchResults();
                        aboutrtb.Visibility = Visibility.Hidden;
                        break;
                    case string search1 when search1.Contains("groceries"):
                        //change the heading of the expense depending on what the user chose
                        resultNamelb.Content = "Groceries";
                        resultCostlb.Content = "R" + ReturnChosenObj().Groceries.ToString();
                        //make search components visible
                        showSearchResults();
                        //calaculate the percentage of income spent on the expense
                        expensePercentageOfIncome(ReturnChosenObj().Groceries);
                        break;
                    case string search1 when search1.Contains("phone") || search1.Contains("cel") || search1.Contains("tel"):
                        //change the heading of the expense depending on what the user chose
                        resultNamelb.Content = "Cellphone/telephone";
                        resultCostlb.Content = "R" + ReturnChosenObj().Phone.ToString();
                        //make search components visible
                        showSearchResults();
                        //calaculate the percentage of income spent on the expense(ReturnChosenObj().Phone);
                        break;
                    case string search1 when search1.Contains("travel") || search1.Contains("petrol"):
                        //change the heading of the expense depending on what the user chose
                        resultNamelb.Content = "Travel(incl petrol)";
                        resultCostlb.Content = "R" + ReturnChosenObj().Travel.ToString();
                        //make search components visible
                        showSearchResults();
                        //calaculate the percentage of income spent on the expense
                        expensePercentageOfIncome(ReturnChosenObj().Travel);
                        break;
                    case string search1 when search1.Contains("other"):
                        //change the heading of the expense depending on what the user chose
                        resultNamelb.Content = "Other";
                        resultCostlb.Content = "R" + ReturnChosenObj().Other.ToString();
                        //make search components visible
                        showSearchResults();
                        //calaculate the percentage of income spent on the expense
                        expensePercentageOfIncome(ReturnChosenObj().Other);
                        break;
                    case string search1 when search1.Contains("tax"):
                        //change the heading of the expense depending on what the user chose
                        resultNamelb.Content = "Tax";
                        resultCostlb.Content = "R" + addIncome.Tax.ToString();
                        //make search components visible
                        showSearchResults();
                        //calaculate the percentage of income spent on the expense
                        expensePercentageOfIncome(addIncome.Tax);
                        break;
                    case string search1 when search1.Contains("rent") && rentgrp.IsChecked == true:
                        //change the heading of the expense depending on what the user chose
                        resultNamelb.Content = "Monthly Rent";
                        resultCostlb.Content = "R" + addRentExpense.MonthlyRent.ToString();
                        //make search components visible
                        showSearchResults();
                        //calaculate the percentage of income spent on the expense
                        expensePercentageOfIncome(addRentExpense.MonthlyRent);

                        break;
                    case string search1 when (search1.Contains("house deposit") || search1.Contains("home deposit")) && buygrp.IsChecked == true:
                        //change the heading of the expense depending on what the user chose
                        resultNamelb.Content = "Home Deposit";
                        resultCostlb.Content = "R" + addBuyingExpense.TotalDeposit.ToString();
                        //make search components visible
                        showSearchResults();
                        //calaculate the percentage of income spent on the expense
                        expensePercentageOfIncome(addBuyingExpense.TotalDeposit);
                        break;
                    case string search1 when (search1.Contains("home repay") || search1.Contains("house repay")) && buygrp.IsChecked == true:
                        //change the heading of the expense depending on what the user chose
                        resultNamelb.Content = "Home Repayment";
                        resultCostlb.Content = "R" + addBuyingExpense.calcMonthlyRepayment().ToString();
                        //make search components visible
                        showSearchResults();
                        //calaculate the percentage of income spent on the expense
                        expensePercentageOfIncome(addBuyingExpense.calcMonthlyRepayment());
                        break;
                    case string search1 when (search1.Contains("car deposit") || search1.Contains("vehicle deposit")) && yesrbtn.IsChecked == true:
                        //change the heading of the expense depending on what the user chose
                        resultNamelb.Content = "Vehicle Deposit";
                        resultCostlb.Content = "R" + addVehicle.Deposit.ToString();
                        //make search components visible
                        showSearchResults();
                        //calaculate the percentage of income spent on the expense
                        expensePercentageOfIncome(addVehicle.Deposit);
                        break;
                    case string search1 when (search1.Contains("car payment") || search1.Contains("vehicle pay") || search1.Contains("car repay") || search1.Contains("vehicle repayment")) && yesrbtn.IsChecked == true:
                        //change the heading of the expense depending on what the user chose
                        resultNamelb.Content = "Vehicle Repayment";
                        resultCostlb.Content = "R" + addVehicle.totalMonthlyCost();
                        //make search components visible
                        showSearchResults();
                        //calaculate the percentage of income spent on the expense
                        expensePercentageOfIncome(addVehicle.totalMonthlyCost());
                        break;
                    case string search1 when (search1.Contains("insurance")) && yesrbtn.IsChecked == true:
                        //change the heading of the expense depending on what the user chose
                        resultNamelb.Content = "Vehicle Insurance";
                        resultCostlb.Content = "R" + addVehicle.EstInsurancePremium.ToString();
                        //make search components visible
                        showSearchResults();
                        //calaculate the percentage of income spent on the expense
                        expensePercentageOfIncome(addVehicle.EstInsurancePremium);
                        break;
                    case string search1 when (search1.Contains("save") || search1.Contains("saving")) && yesSavebtn.IsChecked == true:
                        //change the heading of the expense depending on what the user chose
                        resultNamelb.Content = "Savings Deposit Per Month";
                        resultCostlb.Content = "R" + addSavings.totalCost().ToString();
                        //make search components visible
                        showSearchResults();
                        //calaculate the percentage of income spent on the expense
                        expensePercentageOfIncome(addSavings.totalCost());
                        break;
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

            //if the percentage is Nan or positive or negative infity set it to 0
            if (percentage.Equals(double.NaN) || Double.IsPositiveInfinity(percentage) == true || Double.IsNegativeInfinity(percentage) == true)
            {
                percentage = 0;
            }
            aboutrtb.Document.Blocks.Clear();


            TextRange rangeOfText1 = new TextRange(aboutrtb.Document.ContentEnd, aboutrtb.Document.ContentEnd);
            rangeOfText1.Text = "You have spent ";
            //Customize font/color of text
            rangeOfText1.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
            rangeOfText1.ApplyPropertyValue(TextElement.FontFamilyProperty, "Calibri Light");

            SolidColorBrush mySolidColorBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#60b64f");

            TextRange rangeOfWord = new TextRange(aboutrtb.Document.ContentEnd, aboutrtb.Document.ContentEnd);
            rangeOfWord.Text = percentage + "% ";
            //Customize font/color of text
            rangeOfWord.ApplyPropertyValue(TextElement.ForegroundProperty, mySolidColorBrush);
            rangeOfWord.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            rangeOfWord.ApplyPropertyValue(TextElement.FontFamilyProperty, "Verdana");

            TextRange rangeOfText2 = new TextRange(aboutrtb.Document.ContentEnd, aboutrtb.Document.ContentEnd);
            rangeOfText2.Text = "of your income on this expense.";
            //Customize font/color of text
            rangeOfText2.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
            rangeOfText2.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            rangeOfText2.ApplyPropertyValue(TextElement.FontFamilyProperty, "Calibri Light");

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
                //set focus to the textbox the user wants to input
                input.Focusable = true;
                Keyboard.Focus(input);
                //move the cursor to the end of the textbox
                input.Select(input.Text.Length, 0);

            }
        }

        private void startbtn_Click(object sender, RoutedEventArgs e)
        {
            //show home panel
            mainpnl.Visibility = Visibility.Hidden;
            homepnl.Visibility = Visibility.Visible;

        }

        private void searchBarbtn_Click(object sender, RoutedEventArgs e)
        {
            //allow user to input a search term
            searchtxt.Visibility = Visibility.Visible;
            searchtxt.Focus();
            searchBarbtn.IsEnabled = false;
        }

        private void exitimg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //exit application
            System.Windows.Application.Current.Shutdown();
        }

        private void exitNotifpb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //show home panel
            homepnl.Visibility = Visibility.Visible;
            notificationpnl.Visibility = Visibility.Hidden;
        }

        private void exitSearchpbx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //make search components invisible
            resultNamelb.Visibility = Visibility.Hidden;
            resultCostlb.Visibility = Visibility.Hidden;
            searchResultspbx.Visibility = Visibility.Hidden;
            searchpnl.Visibility = Visibility.Hidden;
            homepnl.Visibility = Visibility.Visible;
            searchtxt.Text = "";
            aboutrtb.Visibility = Visibility.Hidden;
        }


        //Data input/naviagtion methods
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
            expensepnl.Visibility = Visibility.Visible;
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

            vehiclePurchasepnl.Visibility = Visibility.Visible;


        }

        private void norbtn_Checked(object sender, RoutedEventArgs e)
        {
            vehiclePurchasepnl.Visibility = Visibility.Hidden;
        }

        private void editModelpbx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (modeltxt.Visibility == Visibility.Hidden)
            {
                modelSavelb.Visibility = Visibility.Hidden;
                modeltxt.Visibility = Visibility.Visible;

            }
            else if (modeltxt.Visibility == Visibility.Visible)
            {
                modeltxt.Visibility = Visibility.Hidden;
                modelSavelb.Visibility = Visibility.Visible;
            }
            modelSavelb.Content = modeltxt.Text;
        }

        private void editVehicleCostpbx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            editFigure(vehicleCostlb, vehicleCosttxt);
        }

        private void editVehicleDepositpbx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            editFigure(vehicleDepositCostlb, vehicleDeposittxt);
        }

        private void editVehicleInterestpbx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            editFigure(vehicleInterestPerclb, vehicleInteresttxt);
            vehicleInterestPerclb.Content = vehicleInteresttxt.Text + "%";
        }

        private void editinsurancelb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            editFigure(insuranceCostlb, insurancetxt);
        }

        private void modeltxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                modeltxt.Visibility = Visibility.Hidden;
                modelSavelb.Visibility = Visibility.Visible;
                modelSavelb.Content = modeltxt.Text;
            }
        }

        private void vehicleCosttxt_KeyDown(object sender, KeyEventArgs e)
        {
            enterAmount(e, vehicleCostlb, vehicleCosttxt);
        }

        private void vehicleDeposittxt_KeyDown(object sender, KeyEventArgs e)
        {
            enterAmount(e, vehicleDepositCostlb, vehicleDeposittxt);
        }

        private void vehicleInteresttxt_KeyDown(object sender, KeyEventArgs e)
        {
            enterAmount(e, vehicleInterestPerclb, vehicleInteresttxt);
            vehicleInterestPerclb.Content = vehicleInteresttxt.Text + "%";
        }

        private void insurancetxt_KeyDown(object sender, KeyEventArgs e)
        {
            enterAmount(e, insuranceCostlb, insurancetxt);
        }

        private void reasontxt_KeyDown(object sender, KeyEventArgs e)
        {
            enterAmount(e, reasonSavelb, reasontxt);
            if (reasontxt.Text.Equals(""))
            {
                reasontxt.Text = "";
            }
            else
            {
                reasonSavelb.Content = reasontxt.Text;
            }
        }

        private void amounttxt_KeyDown(object sender, KeyEventArgs e)
        {
            enterAmount(e, amountSacelb, amounttxt);

        }

        private void yearsSpn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (yearsSpn.Visibility == Visibility.Visible)
                {
                    yearsSpn.Visibility = Visibility.Hidden;
                    yearsSavelb.Content = yearsSpn.Value + " years";
                    yearsSavelb.Visibility = Visibility.Visible;
                }
                else
                {
                    yearsSavelb.Visibility = Visibility.Hidden;
                    yearsSpn.Visibility = Visibility.Visible;
                }
            }

        }

        private void interestSavetxt_KeyDown(object sender, KeyEventArgs e)
        {
            enterAmount(e, interestSavingslb, interestSavetxt);
            interestSavingslb.Content = interestSavetxt.Text + "%";
        }

        private void editReasonpbx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            editFigure(reasonSavelb, reasontxt);
            if (reasontxt.Text.Equals(""))
            {
                reasontxt.Text = "";
            }
            else
            {
                reasonSavelb.Content = reasontxt.Text;
            }

        }

        private void edittAmountpbx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            editFigure(amountSacelb, amounttxt);
        }

        private void editYearspbx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (yearsSpn.Visibility == Visibility.Visible)
            {
                yearsSpn.Visibility = Visibility.Hidden;
                yearsSavelb.Content = yearsSpn.Value + " years";
                yearsSavelb.Visibility = Visibility.Visible;
            }
            else if (yearsSpn.Visibility == Visibility.Hidden)
            {
                yearsSavelb.Visibility = Visibility.Hidden;
                yearsSpn.Visibility = Visibility.Visible;
            }
        }

        private void editInterestRatepbx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            editFigure(interestSavingslb, interestSavetxt);
            interestSavingslb.Content = interestSavetxt.Text + "%";
        }

        private void previouspbx4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            savingspnl.Visibility = Visibility.Hidden;
            vehiclepnl.Visibility = Visibility.Visible;
        }

        private void yesSavebtn_Checked(object sender, RoutedEventArgs e)
        {
            savingsInputpnl.Visibility = Visibility.Visible;
        }

        private void noSavebtn_Checked(object sender, RoutedEventArgs e)
        {
            savingsInputpnl.Visibility = Visibility.Hidden;
        }

        private void previouspbx1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            vehiclepnl.Visibility = Visibility.Hidden;
            accomodationpnl.Visibility = Visibility.Visible;
        }


        private void viewGraphbtn_Click(object sender, RoutedEventArgs e)
        {
            homepnl.Visibility = Visibility.Hidden;
           
            analysispnl.Visibility = Visibility.Visible;
        }

        private void exitgraphpbx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            analysispnl.Visibility = Visibility.Hidden;
            homepnl.Visibility = Visibility.Visible;
        }

        
    }
}
