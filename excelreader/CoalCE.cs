namespace ExcelReader
{
    public class CoalCE : GoodCE
    {
        public string Type { get; set; }
        public double Weight { get; set; }

        public CoalCE() 
        {
            Category = "Coals";
            IsSold = false;
            DiscountPrice = 0;
        }
    }
}
