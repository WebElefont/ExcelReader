namespace ExcelReader
{
    public class ECigaretteCE : GoodCE
    {
        public byte Sweet { get; set; }
        public byte Sour { get; set; }
        public byte Fresh { get; set; }
        public byte Spicy { get; set; }
        public string Taste { get; set; }
        public byte EvaporatorVolume { get; set; }
        public short BattareyCapacity { get; set; }
        public int PuffsCount { get; set; }

        public ECigaretteCE()
        {
            Category = "ECigarettes";
            IsSold = false;
            DiscountPrice = 0;
        }
    }
}
