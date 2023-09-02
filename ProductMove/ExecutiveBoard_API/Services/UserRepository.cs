using Dapper;
using ExecutiveBoard_API.Interfaces;
using ProductMove_Model;

namespace ExecutiveBoard_API.Services
{
    public class UserRepository : IUserRepository
    {
        public void AddUser(User user)
        {
            try
            {
                Program.Sql.Execute("Insert into [dbo].[User](userName,passWord,decentralization, address,email) " +
                    "values (@userName, @passWord, @decentralization, @address,@email);"
                    , new User
                    {
                        userName = user.userName,
                        passWord = user.passWord,
                        decentralization = user.decentralization,
                        address = user.address,
                        email = user.email
                    });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteUser(int id)
        {
            try
            {
                var data = Program.Sql.Execute("DELETE FROM [dbo].[User] WHERE idUser = @idUser", new User
                {
                    idUser = id
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public User GetUser(int id)
        {
            try
            {
                var data = Program.Sql.QuerySingle<User>("Select * from [dbo].[User] p where p.idUser = @idUser", new User
                {
                    idUser = id
                });
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<User> GetUsers()
        {
            try
            {
                var data = Program.Sql.Query<User>("Select * from [dbo].[User]").AsList();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateUser(int id, User user)
        {
            try
            {
                var data = Program.Sql.Execute("Update [User] Set " +
                    "userName = @userName, " +
                    "passWord = @passWord, " +
                    "decentralization = @decentralization, " +
                    "address = @address, " +
                    "email = @email " +
                    "where idUser = @idUser;", new User
                    {
                        userName = user.userName,
                        passWord = user.passWord,
                        decentralization = user.decentralization,
                        address = user.address,
                        email = user.email,
                        idUser = id
                    });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
