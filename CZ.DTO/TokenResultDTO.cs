using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZ.DTO
{
    public class TokenResultDTO
    {
        public string Token { get; set; }

        public DateTimeOffset Expiration { get; set; }
        public Guid RefreshToken { get; set; }
    }
}
