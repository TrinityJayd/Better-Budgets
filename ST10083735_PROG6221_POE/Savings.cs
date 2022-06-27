using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10083735_PROG6221_POE
{
    class Savings
    {
        private String reason;
        private double amountToSave;
        private int numberOfYears;
        private double interestRate;

        public Savings(string reason, double amountToSave, int numberOfYears, double interestRate)
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

            int months = numberOfYears * 12;




            return amountPerMonth;
        }
    }
}
