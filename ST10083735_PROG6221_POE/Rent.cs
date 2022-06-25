namespace ST10083735_PROG6221_POE
{
    class Rent : Expense
    {
        private double monthlyRent;



        public Rent(double groceries, double utilities, double travel, double phone, double other, double monthlyRent) : base(groceries, utilities, travel, phone, other)
        {
            this.monthlyRent = monthlyRent;
        }

        public double MonthlyRent { get => monthlyRent; set => monthlyRent = value; }


        public override double totalCost()
        {
            //Calculate total cost if user chose to rent
            double sum = Groceries + Utilities + Travel + Phone + Other + MonthlyRent;
            return sum;
        }
    }
}
