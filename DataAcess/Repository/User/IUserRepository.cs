using BusinessObject;
using DataAcess.ViewModels;

namespace DataAcess.Repository.User
{
    public interface IUserRepository
    {
        public Task<List<UserObject>> GetUserList();
        public Task<UserObject> Login(string username, string password);
        public Task<bool> CreateAccount(AccountViewModel viewModel);
        public Task<bool> AccountActivate(int userId, bool activate);
    }
}
