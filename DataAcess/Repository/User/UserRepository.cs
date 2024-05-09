using BusinessObject;
using DataAcess.ViewModels;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;

namespace DataAcess.Repository.User
{
    public class UserRepository : IUserRepository
    {
        public readonly SiddleStoreDbContext _context;
        public UserRepository(SiddleStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserObject>> GetUserList()
        {
            List<UserObject> users;
            return users = await _context.Users.AsNoTracking().Include(c => c.Customer).Include(s => s.Store).Include(r => r.Role).ToListAsync();
        }

        public async Task<UserObject> GetUser(int userId)
        {
            UserObject user;
            return user = await _context.Users.Where(u => u.UserId == userId).FirstAsync();
        }

        public async Task<bool> AccountActivate(int userId, bool activate)
        {
            UserObject user = await GetUser(userId);
            if (user == null)
            {
                throw new InvalidOperationException("User is not existed!");
            }
            if (activate)
            {
                user.Status = UserStatus.Activated;
            }
            else
            {
                user.Status = UserStatus.Deactivate;
            }
            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CreateAccount(AccountViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new InvalidOperationException("User is undefined!");
            }

            UserObject user = new UserObject();

            user.StoreId = viewModel.StoreId;
            user.RoleId = viewModel.RoleId;
            user.UserName = viewModel.UserName;
            user.PasswordHashed = HashPassword(viewModel.PasswordHashed);
            user.Status = UserStatus.Deactivate;

            var storeCheck = _context.Stores.Any(s => s.StoreId == viewModel.StoreId);
            if (storeCheck)
            {
                RoleObject roleCheck = _context.Roles.First(r => r.RoleId == viewModel.RoleId);
                if (roleCheck != null)
                {
                    var existingUser = _context.Users.FirstOrDefault(u => u.UserName == viewModel.UserName);
                    if (existingUser == null)
                    {
                        _context.Users.Add(user);

                        if (roleCheck.RoleName.Equals("Customer"))
                        {
                            var customer = _context.Customers.Where(c => c.CustomerId == viewModel.CustomerId).FirstOrDefault();
                            if (customer != null)
                            {
                                if (customer.UserId == null)
                                {
                                    customer.UserId = user.UserId;
                                    _context.Customers.Update(customer);
                                }
                                else
                                {
                                    throw new InvalidOperationException("Customer already has an account!");
                                }
                            }
                            else
                            {
                                throw new InvalidOperationException("Customer doesn't exist!");
                            }
                        }
                        else if (roleCheck.RoleName.Equals("Manager") || roleCheck.RoleName.Equals("Employee"))
                        {
                            user.Status = UserStatus.Activated;
                        }
                        return await _context.SaveChangesAsync() > 0;
                    }
                    else
                    {
                        throw new InvalidOperationException("Username already exists!");
                    }
                }
                else
                {
                    throw new InvalidOperationException("Role is undefined!");
                }
            }
            else
            {
                throw new InvalidOperationException("Store is undefined!");
            }
        }

        public async Task<UserObject> Login(string username, string password)
        {
            UserObject? user;
            string hashedPassword = HashPassword(password);
            List<UserObject> users = await GetUserList();
            user = users.SingleOrDefault(us => us.UserName.Equals(username) && us.PasswordHashed == hashedPassword && us.Status == UserStatus.Activated);
            if (user == null)
            {
                throw new KeyNotFoundException("Login failed! Please check your email and password or Account is not Activated!");
            }
            return user;
        }
        public string HashPassword(string? password)
        {
            byte[] salt = { 15, 12 };

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
