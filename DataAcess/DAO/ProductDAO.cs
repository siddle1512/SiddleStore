using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAcess.DAO
{
    public class ProductDAO
    {
        private static ProductDAO? instance;

        public static ProductDAO Instance
        {
            get { if (instance == null) instance = new ProductDAO(); return ProductDAO.instance; }
            private set { ProductDAO.instance = value; }
        }

        public List<ProductObject> GetProductsList(bool order = false)
        {
            List<ProductObject> products;
            try
            {
                var context = new SiddleSroteDbContext();
                if (order)
                {
                    products = context.Products.Where(p => p.InStock > 0).ToList();
                }
                else
                {
                    products = context.Products.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }

        public List<ProductObject> SearchProduct(string name, List<ProductObject> searchList)
        {
            List<ProductObject> searchResult;

            try
            {
                if (searchList == null)
                {
                    var context = new SiddleSroteDbContext();
                    searchResult = context.Products
                                        .Where(p => p.ProductName.ToLower().Contains(name.ToLower()))
                                        .ToList();
                }
                else
                {
                    searchResult = searchList.Where(p => p.ProductName.ToLower().Contains(name.ToLower())).ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return searchResult;
        }

        public ProductObject GetProduct(int productId, List<ProductObject>? searchList = null)
        {
            ProductObject product = null!;

            try
            {
                if (searchList == null)
                {
                    var context = new SiddleSroteDbContext();
                    var _ = context.Products.Where(p => p.ProductId == productId);              
                    if (_ != null && _.Any())
                    {
                        product = _.First();
                    }
                }
                else
                {
                    product = searchList.Where(p => p.ProductId == productId)
                        .AsQueryable()
                        .First();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return product;
        }

        public void AddProduct(ProductObject product)
        {
            if (product == null)
            {
                throw new Exception("Product is undefined!!");
            }
            try
            {
                if (GetProduct(product.ProductId) == null)
                {
                    var context = new SiddleSroteDbContext();
                    context.Products.Add(product);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Product is existed!!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(ProductObject product)
        {
            if (product == null)
            {
                throw new Exception("Product is undefined!");
            }
            try
            {
                ProductObject _mem = GetProduct(product.ProductId);
                if (_mem != null)
                {
                    var context = new SiddleSroteDbContext();
                    context.Products.Update(product);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Product does not exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void Delete(int productId)
        {
            try
            {
                ProductObject Product = GetProduct(productId);
                if (Product != null)
                {
                    var context = new SiddleSroteDbContext();
                    context.Products.Remove(Product);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Product does not exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
