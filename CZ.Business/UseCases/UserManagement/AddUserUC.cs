using CZ.Business.Helpers;
using CZ.Business.UseCases.Interfaces;
using CZ.Domain;
using CZ.DTO.UserManagement;
using CZ.SQLContext.Interfaces;
using FluentValidation;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZ.Business.UseCases.UserManagement
{
    public class AddUserUC : IAddUserUC
    {
        private readonly IUserRepository _userRepository;

        public AddUserUC(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public Task<UserViewModel> Execute(AddUserDTO userDTO)
        {
            var password = HashHelper.Encrypt(userDTO.Password, HasAlgorithm.SHA128, null);
            var user = new User(userDTO.Name, userDTO.Lastname, userDTO.Email, password);

            _userRepository.Add(user);

            return Task.FromResult(new UserViewModel(user));
        }
    }
}
