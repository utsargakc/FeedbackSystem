using AuthWeb.Data;
using AuthWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthWeb.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;
        public UserService(ApplicationDbContext db)
        {
            _db = db;
        }

        public ApplicationUser GetUserByUserName(string userName)
        {
            
            return _db.Users.Where(u => u.UserName == userName).SingleOrDefault();
        }
    }
}
