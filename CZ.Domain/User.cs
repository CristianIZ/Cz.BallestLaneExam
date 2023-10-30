using CZ.Domain.Base;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZ.Domain
{
    public class User : KeyEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User()
        {
            
        }

        public User(string name, string lastName, string email, string password)
        {
            this.Name = name;
            this.LastName = lastName;
            this.Email = email;
            this.Password = password;
        }
    }
}
