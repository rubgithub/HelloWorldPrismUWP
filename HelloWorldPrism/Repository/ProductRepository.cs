namespace HelloWorldPrism.Repository
{
    public class ProductRepository : IProductRepository
    {
        private string ProductName { get; set; }
        public void Add(string produtctName)
        {
            ProductName = produtctName;
        }

        public string Get()
        {
            return ProductName;
        }
    }
}
