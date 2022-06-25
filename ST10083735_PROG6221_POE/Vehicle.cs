using System;

namespace ST10083735_PROG6221_POE
{
    class Vehicle : Expense
    {
        //Declare characteristics needed to purchase a vehicle
        private String modelAndMake;
        private double purchasePrice;
        private double deposit;
        private double interest;
        private double estInsurancePremium;

        //The vehicle repayment is always calculated over 5 years.
        private const int YEARS = 5;

        public Vehicle(string modelAndMake, double purchasePrice, double deposit, double interest, double estInsurancePremium, double groceries = 0, double utilities = 0, double travel = 0, double phone = 0, double other = 0) : base(groceries, utilities, travel, phone, other)
        {
            this.modelAndMake = modelAndMake;
            this.purchasePrice = purchasePrice;
            this.deposit = deposit;
            this.interest = interest;
            this.estInsurancePremium = estInsurancePremium;
        }

        public string ModelAndMake { get => modelAndMake; set => modelAndMake = value; }
        public double PurchasePrice { get => purchasePrice; set => purchasePrice = value; }
        public double Deposit { get => deposit; set => deposit = value; }
        public double Interest { get => interest; set => interest = value; }
        public double EstInsurancePremium { get => estInsurancePremium; set => estInsurancePremium = value; }


        public double totalMonthlyCost()
        {

            //Purchase price minus the total deposit
            double principalAmt = PurchasePrice - Deposit;
            double monthlyPayment = 0;
            //Divide interest by 100
            Interest = Interest / 100;

            //Amount excluding deposit multiplied by interest
            double interestTotal = principalAmt * Interest;

            //Divide the interest by the number of years to repay the car loan
            monthlyPayment = interestTotal / (YEARS * 12);

            //Round off monthly repayment
            double roundedMonthlyPayment = Math.Round(monthlyPayment, 2);

            roundedMonthlyPayment += EstInsurancePremium;


            return roundedMonthlyPayment;
        }

        public override double totalCost()
        {
            //Return the total monthly payment plus the insurance premium
            double total = totalMonthlyCost();

            return total;
        }
    }
}
