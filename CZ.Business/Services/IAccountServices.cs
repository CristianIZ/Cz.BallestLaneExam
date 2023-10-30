using CZ.Domain;
using CZ.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CZ.Business.Services
{
    public interface IAccountServices
    {
        Task<bool> ValidateToken(ClaimsIdentity identity, string token);
        Task<TokenResultDTO> GenerateToken(User user);
        Task<TokenResultDTO> RefreshToken(RefreshTokenDTO refreshTokenDTO);
        Task<User> GetUserFromToken(ClaimsIdentity identity);
    }
}
