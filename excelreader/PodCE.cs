namespace ExcelReader
{
    public class PodCE : GoodCE
    {
        public double Weight { get; set; }
        public string Material { get; set; }
        public short Battarey { get; set; }
        public double CartrigeCapacity { get; set; }
        public double EvaporatorResistance { get; set; }
        public string Power { get; set; }
        public string Port { get; set; }

        public PodCE()
        {
            Category = "Pods";
            IsSold = false;
            DiscountPrice = 0;
        }
    }
}
