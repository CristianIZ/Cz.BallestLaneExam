using CZ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZ.SQLContext.Interfaces
{
    public interface IUserRepository : ISQLRepository<User>
    {
        User GetByEmail(string email);
    }
}
