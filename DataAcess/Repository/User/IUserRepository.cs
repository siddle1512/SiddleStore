using BusinessObject;
using DataAcess.ViewModels;

namespace DataAcess.Repository.User
{
    public interface IUserRepository
    {
        public List<UserObject> GetMembersList();
        public UserObject Login(string username, string password);
        public void CreateAccount(AccountViewModel viewModel);
        public void AccountActivate(int userId, bool activate);
    }
}
