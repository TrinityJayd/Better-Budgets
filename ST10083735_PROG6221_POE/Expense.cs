using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10083735_PROG6221_POE
{
    public abstract class Expense
    {
        private double groceries;
        private double utilities;
        private double travel;
        private double phone;
        private double other;


        public abstract double totalCost();

        protected Expense(double groceries, double utilities, double travel, double phone, double other)
        {
            this.Groceries = groceries;
            this.Utilities = utilities;
            this.Travel = travel;
            this.Phone = phone;
            this.Other = other;
        }

        public double Groceries { get => groceries; set => groceries = value; }
        public double Utilities { get => utilities; set => utilities = value; }
        public double Travel { get => travel; set => travel = value; }
        public double Phone { get => phone; set => phone = value; }
        public double Other { get => other; set => other = value; }

    }
}
