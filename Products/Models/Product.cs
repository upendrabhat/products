namespace Products.Models
{
    public class Product
    {
        public int Pid { get; set; }
        public string? PName { get; set; }
        public string? PCategory { get; set; }
        public float PPrice { get; set; }
        public int PStock { get; set; }
        public bool PIsInStock { get; set; }
    }
}
