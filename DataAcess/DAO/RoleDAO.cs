using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAcess.DAO
{
    public class RoleDAO
    {
        private readonly SiddleStoreDbContext _context;

        public RoleDAO(SiddleStoreDbContext context)
        {
            _context = context;
        }

        public RoleObject GetRole(int roleId)
        {
            RoleObject role;
            try
            {
                role = _context.Roles.Where(r => r.RoleId == roleId).First();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return role;
        }
    }
}
