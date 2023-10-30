using CZ.Domain;
using CZ.DTO;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using CZ.SQLContext.Interfaces;
using CZ.SQLContext;
using CZ.Business.Helpers;
using CZ.Business.UseCases.Interfaces;
using System.Data.Entity;
using CZ.Business.Services;

namespace CZ.Business.UseCases.UserManagement
{
    public class LoginUserUC : ILoginUserUC
    {
        private readonly IConfigurationSection _jwtConfiguration;
        private readonly IUserRepository _userRepository;
        private readonly IAccountServices _accountServices;

        public LoginUserUC(IConfiguration configuration,
            IUserRepository userRepository,
            IAccountServices accountServices)
        {
            this._jwtConfiguration = configuration.GetSection("Jwt");
            this._userRepository = userRepository;
            this._accountServices = accountServices;
        }

        public async Task<TokenResultDTO> Execute(LoginDTO loginDTO)
        {
            var user = _userRepository.GetByEmail(loginDTO.Email);

            if (user is null)
                throw new UnauthorizedAccessException("Invalid email");

            if (!HashHelper.VerifyHash(loginDTO.Password, HasAlgorithm.SHA128, user.Password))
                throw new Exception("Incorrect password");

            var token = await _accountServices.GenerateToken(user);

            return token;
        }
    }
}
