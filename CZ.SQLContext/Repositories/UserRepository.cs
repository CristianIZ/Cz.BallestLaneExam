using CZ.Domain;
using CZ.SQLContext.Helpers;
using CZ.SQLContext.Interfaces;
using System.Collections;
using System.Data;

namespace CZ.SQLContext.Services
{
    public class UserRepository : IUserRepository
    {
        public Task<User> GetByKey(string key)
        {
            string query = $"SELECT * FROM [Users] WHERE [Key] = @Key";

            using (var DA = new SQLDataAccess())
            {
                var sqlParameters = new ArrayList();
                sqlParameters.Add(SqlHelper.CreateParameter("Key", key));

                var table = DA.Read(query, sqlParameters);
                return Task.FromResult(ReadEntity(table).First());
            }
        }

        public Task Add(User user)
        {
            string query = $"INSERT INTO [dbo].[Users] ([Email], [Name], [LastName], [Password], [Key]) VALUES (@Email, @Name, @LastName, @Password, @Key);";

            user.Key = Guid.NewGuid().ToString();

            var sqlParameters = new ArrayList();

            sqlParameters.Add(SqlHelper.CreateParameter("Name", user.Name));
            sqlParameters.Add(SqlHelper.CreateParameter("LastName", user.LastName));
            sqlParameters.Add(SqlHelper.CreateParameter("Password", user.Password));
            sqlParameters.Add(SqlHelper.CreateParameter("Key", user.Key));
            sqlParameters.Add(SqlHelper.CreateParameter("Email", user.Email));


            using (var DA = new SQLDataAccess())
            {
                DA.ExecuteQuery(query, sqlParameters);
            }

            return Task.CompletedTask;
        }

        public User GetByEmail(string email)
        {
            string query = $"SELECT * FROM [Users] WHERE [Email] = @Email";

            using (var DA = new SQLDataAccess())
            {
                var sqlParameters = new ArrayList();
                sqlParameters.Add(SqlHelper.CreateParameter("Email", email));

                var table = DA.Read(query, sqlParameters);
                return ReadEntity(table).First();
            }
        }

        public ICollection<User> ReadEntity(DataTable dataTable)
        {
            ICollection<User> user = new List<User>();

            foreach (DataRow item in dataTable.Rows)
            {
                user.Add(MapEntity(item));
            }

            return user;
        }

        public User MapEntity(DataRow dataRow)
        {
            return new User()
            {
                Id = Convert.ToInt32(dataRow["Id"]),
                Key = dataRow["Key"].ToString(),
                Name = dataRow["Name"].ToString(),
                LastName = dataRow["LastName"].ToString(),
                Password = dataRow["Password"].ToString(),
                Email = dataRow["Email"].ToString()
            };
        }
    }
}