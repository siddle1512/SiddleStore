using BusinessObject;
using DataAcess.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace DataAcess.DAO
{
    public class UsersDAO
    {
        private static UsersDAO? instance;

        public static UsersDAO Instance
        {
            get { if (instance == null) instance = new UsersDAO(); return UsersDAO.instance; }
            private set { UsersDAO.instance = value; }
        }
        public List<UserObject> GetUserList()
        {
            List<UserObject> users;

            try
            {
                var context = new SiddleStoreDbContext();
                users = context.Users
                    .Include(c => c.Customer)
                    .Include(s => s.Store)
                    .Include(r => r.Role).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return users;
        }

        public UserObject GetUser(int userId)
        {
            UserObject user;
            try
            {
                var context = new SiddleStoreDbContext();
                user = context.Users.Where(u => u.UserId == userId).First();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public UserObject Login(string username, string password)
        {
            UserObject? user;
            try
            {
                string hashedPassword = HashPassword(password);

                List<UserObject> users = GetUserList();
                user = users.SingleOrDefault(us => us.UserName.Equals(username) && us.PasswordHashed == hashedPassword && us.Status == UserStatus.Activated);
                if (user == null)
                {
                    throw new Exception("Login failed! Please check your email and password or Account is not Activated!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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

        public async void CreateAccount(AccountViewModel viewModel)
        {
            try
            {
                if (viewModel == null)
                {
                    throw new Exception("User is undefined!!");
                }

                var context = new SiddleStoreDbContext();

                UserObject user = new UserObject();

                user.StoreId = viewModel.StoreId;
                user.RoleId = viewModel.RoleId;
                user.UserName = viewModel.UserName;
                user.PasswordHashed = HashPassword(viewModel.PasswordHashed);
                user.Status = UserStatus.Deactivate;

                var storeCheck = StoreDAO.Instance.GetStore(viewModel.StoreId);
                if (storeCheck == null)
                {
                    throw new Exception("Store is undefined!");
                }

                var roleCheck = RoleDAO.Instance.GetRole(viewModel.RoleId);
                if (roleCheck != null)
                {
                    context.Users.Add(user);
                    await context.SaveChangesAsync();

                    if (roleCheck.RoleName.Equals("Customer"))
                    {
                        var customer = CustomerDAO.Instance.GetCustomer(viewModel.CustomerId, null);
                        if (customer.UserId == null)
                        {
                            customer.UserId = user.UserId;
                            context.Customers.Update(customer);
                            await context.SaveChangesAsync();
                        }
                        else
                        {
                            throw new Exception("Customer already had an account!");
                        }
                    }
                    else if (roleCheck.RoleName.Equals("Manager") || roleCheck.RoleName.Equals("Employee"))
                    {
                        user.Status = UserStatus.Activated;
                        context.Users.Update(user);
                        await context.SaveChangesAsync();
                    }
                }
                else
                {
                    throw new Exception("Role is undefined!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AccountActivate(int userId, bool activate)
        {
            var context = new SiddleStoreDbContext();
            UserObject user = GetUser(userId);
            if (activate)
            {
                user.Status = UserStatus.Activated;
            }
            else
            {
                user.Status = UserStatus.Deactivate;
            }
            context.Users.Update(user);
            context.SaveChanges();
        }
    }
}
