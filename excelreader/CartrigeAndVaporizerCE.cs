namespace ExcelReader
{
    public class CartrigeAndVaporizerCE : GoodCE
    {
        public double? CartrigeCapacity { get; set; }
        public string SpiralType { get; set; }
        public bool IsVaporizer { get; set; }
        public double Resistance { get; set; }

        public CartrigeAndVaporizerCE()
        {
            Category = "CartrigesAndVaporizers";
            IsSold = false;
            DiscountPrice = 0;
            CartrigeCapacity = null;
            SpiralType = null;
        }
    }
}