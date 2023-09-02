namespace ProductMove_Model
{
    public class Product
    {
        public int idProduct { get; set; }
        public int idCategory { get; set; }
        public string productName { get; set; } = null!;
        public string price { get; set; } = null!;
        public string description { get; set; } = null!;
        public string warrantyPeriod { get; set; } = null!;
        public string categoryName { get; set; } = null!;
    }
}
