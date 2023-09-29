using CAVBackEndUpdate.Data;
using CAVBackEndUpdate.Models;
using Microsoft.EntityFrameworkCore;

namespace CAVBackEndUpdate.Reopsitory
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public void AddUsersToDb(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public List<User> GetAllUsers()
        {
           var currentUsers =  _context.Users.ToList();
            return currentUsers;
        }

        public List<User> GetAllUsersByDate(AccountDateParameter daterange)
        {

            var currentUsers = _context.Users.Where(x => x.CreatedAt >= daterange.StartDate && x.CreatedAt <= daterange.EndDate).ToList();
            return currentUsers;
        }

        public User GetById(int userId)
        {
            return _context.Users.FirstOrDefault(x => x.Id == userId);
        }
    }
}
