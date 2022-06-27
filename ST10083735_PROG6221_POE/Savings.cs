using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ST10083735_PROG6221_POE
{
    class Savings : Expense
    {
        private String reason;
        private double amountToSave;
        private int numberOfYears;
        private double interestRate;

        public Savings(string reason, double amountToSave, int numberOfYears, double interestRate, double groceries = 0, double utilities = 0, double travel = 0, double phone = 0, double other = 0) : base(groceries, utilities, travel, phone, other)
        {
            this.reason = reason;
            this.amountToSave = amountToSave;
            this.numberOfYears = numberOfYears;
            this.interestRate = interestRate;
        }

        public string Reason { get => reason; set => reason = value; }
        public double AmountToSave { get => amountToSave; set => amountToSave = value; }
        public int NumberOfYears { get => numberOfYears; set => numberOfYears = value; }
        public double InterestRate { get => interestRate; set => interestRate = value; }

        public double calculateAmountToSavePerMonth()
        {
            double amountPerMonth = 0;

            int compoundingPeriod = 12;
            int months = numberOfYears * compoundingPeriod;
            double interest = interestRate / 100;

            double step1 = Math.Pow((1 + interest / compoundingPeriod), months) - 1;
            double step2 = step1 / (interest / compoundingPeriod);
            double step3 = amountToSave / step2;
            amountPerMonth = Math.Round(step3, 2); 

            MessageBox.Show(amountPerMonth.ToString());
            return amountPerMonth;
        }

        public override double totalCost()
        {
            double total = calculateAmountToSavePerMonth();
            MessageBox.Show(total.ToString());
            return total;
        }
    }
}
