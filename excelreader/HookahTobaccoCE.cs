namespace ExcelReader
{
    public class HookahTobaccoCE : GoodCE
    {
        public byte Sweet { get; set; }
        public byte Sour { get; set; }
        public byte Fresh { get; set; }
        public byte Spicy { get; set; }
        public string Taste { get; set; }
        public string Strength { get; set; }
        public double Weight { get; set; }

        public HookahTobaccoCE()
        {
            Category = "HookahTobacco";
            IsSold = false;
            DiscountPrice = 0;
        }
    }

}
