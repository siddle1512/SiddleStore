using BusinessObject;
using DataAcess.DAO;
using DataAcess.ViewModels;

namespace DataAcess.Repository.User
{
    public class UserRepository : IUserRepository
    {
        public List<UserObject> GetMembersList() => UsersDAO.Instance.GetUserList();
        public UserObject Login(string username, string password) => UsersDAO.Instance.Login(username, password);
        public void CreateAccount(AccountViewModel viewModel) => UsersDAO.Instance.CreateAccount(viewModel);
        public void AccountActivate(int userId, bool activate) => UsersDAO.Instance.AccountActivate(userId, activate);
    }
}
