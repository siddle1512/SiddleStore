using BusinessObject;

namespace DataAcess.Repository.Product
{
    public interface IProductRepository
    {
        public void AddProduct(ProductObject product);
        public List<ProductObject> GetProductList(bool order = false);
        public ProductObject GetProduct(int productId, List<ProductObject>? searchList = null);
        public List<ProductObject> SearchProduct(string search, List<ProductObject> products);
        public void Update(ProductObject product);
        public void Delete(int productId);
    }
}
