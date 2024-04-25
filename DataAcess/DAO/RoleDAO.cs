using BusinessObject;

namespace DataAcess.DAO
{
    public class RoleDAO
    {
        private static RoleDAO? instance;

        public static RoleDAO Instance
        {
            get { if (instance == null) instance = new RoleDAO(); return RoleDAO.instance; }
            private set { RoleDAO.instance = value; }
        }

        public Role GetRole(int roleId)
        {
            Role role;
            try
            {
                var context = new SiddleStoreDbContext();
                role = context.Roles.Where(r => r.RoleId == roleId).First();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return role;
        }
    }
}
