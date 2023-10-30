using CZ.Business.UseCases.Interfaces;
using CZ.Domain;
using CZ.DTO;
using CZ.SQLContext.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CZ.Business.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly IConfigurationSection _jwtConfiguration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AccountServices(IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository)
        {
            this._jwtConfiguration = configuration.GetSection("Jwt");
            this._httpContextAccessor = httpContextAccessor;
            this._userRepository = userRepository;
            this._refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<TokenResultDTO> GenerateToken(User user)
        {
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name.ToString()),
                    new Claim("userKey", user.Key.ToString()),
                    new Claim("email", user.Email.ToString())
                };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration["Key"].ToString()));
            var singIn = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtConfiguration["Issuer"].ToString(),
                audience: _jwtConfiguration["Audience"].ToString(),
                claims: authClaims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: singIn);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = new RefreshToken(user.Id, jwtToken);

            await _refreshTokenRepository.AddRefreshToken(refreshToken);

            return new TokenResultDTO
            {
                Token = jwtToken,
                Expiration = token.ValidTo,
                RefreshToken = refreshToken.RefreshTokenGuid
            };
        }

        public async Task<User> GetUserFromToken(ClaimsIdentity identity)
        {
            if (identity.Claims.Count() == 0)
                throw new Exception("Invalid token");

            var userKey = identity.Claims.First(t => t.Type == "userKey").Value;

            return await _userRepository.GetByKey(userKey);
        }

        public async Task<bool> ValidateToken(ClaimsIdentity identity, string token)
        {
            var user = await this.GetUserFromToken(identity);
            var lastToken = await this._refreshTokenRepository.GetLastToken(user.Id);

            return lastToken.Token == token.Replace("Bearer","").Trim();
        }

        public async Task<TokenResultDTO> RefreshToken(RefreshTokenDTO refreshTokenDTO)
        {
            _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out var token);
            token = token.ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
                throw new UnauthorizedAccessException("Invalid refresh token");

            RefreshToken? refreshToken = await _refreshTokenRepository.GetRefreshToken(refreshTokenDTO.RefreshToken, token.ToString());

            if (refreshToken is null)
                throw new UnauthorizedAccessException("Invalid refresh token");

            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(token) as JwtSecurityToken;
            var userEmailClaim = tokenS.Claims.FirstOrDefault(x => x.Type == "userName");

            var user = _userRepository.GetByEmail(userEmailClaim.Value);

            if (user is null)
                throw new UnauthorizedAccessException("Invalid refresh token");

            var newToken = await GenerateToken(user);

            await _refreshTokenRepository.RemoveRefreshToken(refreshToken);

            return newToken;
        }
    }
}
