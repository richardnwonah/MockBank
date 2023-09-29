using CAVBackEndUpdate.Models;

namespace CAVBackEndUpdate.Reopsitory
{
    public interface IUserRepository
    {
        void AddUsersToDb(User user);
        List<User> GetAllUsers();
        User GetById(int userId);
        List<User> GetAllUsersByDate(AccountDateParameter daterange);
    }
}
