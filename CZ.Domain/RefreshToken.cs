using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZ.Domain
{
    public class RefreshToken
    {
        public RefreshToken()
        {
            
        }

        public RefreshToken(int userId, string token)
        {
            this.UserId = userId;
            this.Expires = DateTime.UtcNow.AddDays(1);
            this.RefreshTokenGuid = Guid.NewGuid();
            this.Token = token;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public Guid RefreshTokenGuid { get; set; }
        public DateTimeOffset Expires { get; set; }
    }
}
