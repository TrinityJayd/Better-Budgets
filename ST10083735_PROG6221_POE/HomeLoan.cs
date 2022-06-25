using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10083735_PROG6221_POE
{
    class HomeLoan : Expense
    {
        private double purchasePrice;
        private double totalDeposit;
        private double interestRate;
        private int numMonths;


        public HomeLoan(double groceries, double utilities, double travel, double phone, double other, double purchasePrice, double totalDeposit, double interestRate, int numMonths) : base(groceries, utilities, travel, phone, other)
        {
            this.purchasePrice = purchasePrice;
            this.totalDeposit = totalDeposit;
            this.interestRate = interestRate;
            this.numMonths = numMonths;
        }

        public double PurchasePrice { get => purchasePrice; set => purchasePrice = value; }
        public double TotalDeposit { get => totalDeposit; set => totalDeposit = value; }
        public double InterestRate { get => interestRate; set => interestRate = value; }
        public int NumMonths { get => numMonths; set => numMonths = value; }



        public double calcMonthlyRepayment()
        {
            double monthlyRepayment = 0;
            //Calculate principal ammount
            double principalAmont = this.purchasePrice - this.totalDeposit;
            //convert interest to a decimal
            double interest = this.interestRate / 100;
            //calculate years to pay
            double years = this.numMonths / 12;

            double totalAmount = principalAmont * Math.Pow((1 + interest), years);

            monthlyRepayment = totalAmount / this.numMonths;
            //round off amount
            monthlyRepayment = Math.Round(monthlyRepayment, 2);

            return monthlyRepayment;

        }

        //Check if the recieved income is > one third of the monthly repayment
        public bool isMoreThanAThird(double income)
        {
            bool isMore = false;

            double aThirdOfIncome = income * 1 / 3;

            if (calcMonthlyRepayment() > aThirdOfIncome)
            {
                isMore = true;

            }


            return isMore;

        }

        public override double totalCost()
        {
            //Calculate total expenses if the user chose to buy
            double sum = Groceries + Utilities + Travel + Phone + Other + TotalDeposit + calcMonthlyRepayment();
            return sum;
        }
    }
}
