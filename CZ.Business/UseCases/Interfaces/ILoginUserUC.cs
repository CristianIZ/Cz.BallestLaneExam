using CZ.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZ.Business.UseCases.Interfaces
{
    public interface ILoginUserUC
    {
        Task<TokenResultDTO> Execute(LoginDTO loginDTO);
    }
}
