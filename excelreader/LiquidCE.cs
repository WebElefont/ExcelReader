namespace ExcelReader
{
    public class LiquidCE : GoodCE
    {
        public string NicotineType { get; set; }
        public byte NicotineStrength { get; set; }
        public byte Capacity { get; set; }
        public string TasteGroup { get; set; }
        public string Taste { get; set; }

        public LiquidCE()
        {
            Category = "Liquids";
            IsSold = false;
            DiscountPrice = 0;
        }
    }
}
