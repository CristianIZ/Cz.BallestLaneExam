using CZ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZ.SQLContext.Interfaces
{
    public interface IRefreshTokenRepository : ISQLRepository<RefreshToken>
    {
        Task AddRefreshToken(RefreshToken refreshToken);
        Task<RefreshToken> GetRefreshToken(Guid refreshToken, string token);
        Task<RefreshToken> GetRefreshTokenByToken(string token);
        Task RemoveRefreshToken(RefreshToken refreshToken);
        Task<RefreshToken> GetLastToken(int userId);
    }
}
