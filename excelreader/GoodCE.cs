using System;

namespace ExcelReader
{
    public class GoodCE
    {
        public Guid GoodID { get; set; }
        public Guid ImageId { get; set; }
        public string ImageUrl { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public short Price { get; set; }
        public short DiscountPrice { get; set; }
        public bool IsSold { get; set; }

        public Guid ProducerId { get; set; }
        public string ProducerName { get; set; }

        public GoodCE()
        {
            GoodID = Guid.Empty;
            ImageUrl = String.Empty;
            ProducerName = String.Empty;
            Category = String.Empty;
        }
    }
}
