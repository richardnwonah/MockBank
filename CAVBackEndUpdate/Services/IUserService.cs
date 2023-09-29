using CAVBackEndUpdate.Models;
using CAVBackEndUpdate.Reopsitory;

namespace CAVBackEndUpdate.Services
{
    public interface IUserService
    {
        User Createuser();
        void GenerateUsers();
        List<User> GetUsersFromDb(AccountDateParameter daterange);
        List<User> GetUsersDataWithOutDate();
        User GetUserById(int userId);
    }
}
