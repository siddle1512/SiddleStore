using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAcess.DAO
{
    public class ProductDAO
    {
        private readonly SiddleStoreDbContext _context;

        public ProductDAO(SiddleStoreDbContext context)
        {
            _context = context;
        }

        public List<ProductObject> GetProductsList(bool order = false)
        {
            List<ProductObject> products;
            try
            {
                if (order)
                {
                    products = _context.Products.Where(p => p.InStock > 0).ToList();
                }
                else
                {
                    products = _context.Products.ToList();
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
                    searchResult = _context.Products
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
                    var _ = _context.Products.Where(p => p.ProductId == productId);              
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

        public int AddProduct(ProductObject product)
        {
            if (product == null)
            {
                throw new Exception("Product is undefined!!");
            }
            try
            {
                if (GetProduct(product.ProductId) == null)
                {
                    _context.Products.Add(product);
                    return _context.SaveChanges();
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

        public int Update(ProductObject product)
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
                    var context = new SiddleStoreDbContext();
                    _context.Products.Update(product);
                    return _context.SaveChanges();
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

        public int Delete(int productId)
        {
            try
            {
                ProductObject Product = GetProduct(productId);
                if (Product != null)
                {
                    _context.Products.Remove(Product);
                    return _context.SaveChanges();
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
