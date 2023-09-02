using ProductMove_Model;

namespace ExecutiveBoard_API.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User GetUser(int id);
        void DeleteUser(int id);
        void UpdateUser(int id, User user);
        void AddUser(User user);
    }
}
