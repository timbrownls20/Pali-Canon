using Microsoft.EntityFrameworkCore;
using PaliCanon.Contracts;

namespace PaliCanon.Data.Sql.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly SqlContext _context;

        public AdminRepository(SqlContext context)
        {
            _context = context;
        }

        public bool CanConnect()
        {
            return _context.Database.CanConnect();
        }

        public bool Migrate()
        {
            _context.Database.Migrate();
            return true;
        }
    }
}
