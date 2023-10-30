using CZ.DTO.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZ.Business.UseCases.Interfaces
{
    public interface IAddUserUC
    {
        Task<UserViewModel> Execute(AddUserDTO userDTO);
    }
}
