using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10083735_PROG6221_POE
{
    class IncomeTax
    {
        private double grossIncome;
        private double tax;

        public IncomeTax()
        {

        }

        public IncomeTax(double grossIncome, double tax)
        {
            this.GrossIncome = grossIncome;
            this.Tax = tax;
        }

        public double GrossIncome { get => grossIncome; set => grossIncome = value; }
        public double Tax { get => tax; set => tax = value; }
    }
}
