using CZ.Domain;
using CZ.SQLContext.Helpers;
using CZ.SQLContext;
using CZ.SQLContext.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace CZ.Business.UseCases.UserManagement
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        public Task Add(RefreshToken value)
        {
            throw new NotImplementedException();
        }

        public Task<RefreshToken> GetByKey(string key)
        {
            throw new NotImplementedException();
        }

        public Task<RefreshToken> GetLastToken(int userId)
        {
            string query = $"SELECT * FROM [RefreshTokens] WHERE [UserId] = @UserId ORDER BY [Id] DESC";

            using (var DA = new SQLDataAccess())
            {
                var sqlParameters = new ArrayList();
                sqlParameters.Add(SqlHelper.CreateParameter("UserId", userId));

                var table = DA.Read(query, sqlParameters);
                return Task.FromResult(ReadEntity(table).First());
            }
        }

        public Task AddRefreshToken(RefreshToken refreshToken)
        {
            string query = $"INSERT INTO dbo.RefreshTokens([UserId], [Expires], [RefreshTokenGuid], [Token]) VALUES (@UserId, @Expires, @RefreshTokenGuid, @Token)";

            using (var DA = new SQLDataAccess())
            {
                var sqlParameters = new ArrayList();
                sqlParameters.Add(SqlHelper.CreateParameter("UserId", refreshToken.UserId));
                sqlParameters.Add(SqlHelper.CreateParameter("Expires", refreshToken.Expires));
                sqlParameters.Add(SqlHelper.CreateParameter("RefreshTokenGuid", refreshToken.RefreshTokenGuid));
                sqlParameters.Add(SqlHelper.CreateParameter("Token", refreshToken.Token));

                DA.ExecuteQuery(query, sqlParameters);
            }

            return Task.FromResult(() => { });
        }

        public Task<RefreshToken> GetRefreshToken(Guid refreshToken, string token)
        {
            string query = $"SELECT * FROM [RefreshTokens] WHERE [RefreshTokenGuid] = @RefreshTokenGuid";

            using (var DA = new SQLDataAccess())
            {
                var sqlParameters = new ArrayList();
                sqlParameters.Add(SqlHelper.CreateParameter("RefreshTokenGuid", token));

                var table = DA.Read(query, sqlParameters);
                return Task.FromResult(ReadEntity(table).First());
            }
        }

        public Task<RefreshToken> GetRefreshTokenByToken(string token)
        {
            string query = $"SELECT * FROM [RefreshTokens] WHERE [Token] = @Token";

            using (var DA = new SQLDataAccess())
            {
                var sqlParameters = new ArrayList();
                sqlParameters.Add(SqlHelper.CreateParameter("Token", token));

                var table = DA.Read(query, sqlParameters);
                return Task.FromResult(ReadEntity(table).First());
            }
        }

        public Task RemoveRefreshToken(RefreshToken refreshToken)
        {

            // _refreshTokenRepository.Set<RefreshToken>().Remove(refreshToken);

            return Task.FromResult(() => { });
        }

        public ICollection<RefreshToken> ReadEntity(DataTable dataTable)
        {
            ICollection<RefreshToken> refreshToken = new List<RefreshToken>();

            foreach (DataRow item in dataTable.Rows)
            {
                refreshToken.Add(MapEntity(item));
            }

            return refreshToken;
        }

        public RefreshToken MapEntity(DataRow dataRow)
        {
            return new RefreshToken()
            {
                Id = Convert.ToInt32(dataRow["Id"]),
                UserId = Convert.ToInt32(dataRow["UserId"]),
                Expires = Convert.ToDateTime(dataRow["Expires"].ToString()),
                Token = dataRow["Token"].ToString(),
                RefreshTokenGuid = Guid.Parse(dataRow["RefreshTokenGuid"].ToString()),
            };
        }
    }
}
