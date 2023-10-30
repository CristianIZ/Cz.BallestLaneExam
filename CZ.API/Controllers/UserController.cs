using CZ.Business.UseCases.Interfaces;
using CZ.DTO;
using CZ.DTO.UserManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CZ.API.Controllers
{
    public class UserController : Controller
    {
        private readonly IAddUserUC _addUserUC;
        private readonly ILoginUserUC _loginUserUC;

        public UserController(IAddUserUC addUserUC,
            ILoginUserUC loginUserUC)
        {
            this._loginUserUC = loginUserUC;
            this._addUserUC = addUserUC;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Add(AddUserDTO userDTO)
        {
            var userVM = await _addUserUC.Execute(userDTO);
            return Ok(userVM);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Signin([FromBody] LoginDTO loginDTO)
        {
            var token = await _loginUserUC.Execute(loginDTO);
            return Ok(token);
        }
    }
}
