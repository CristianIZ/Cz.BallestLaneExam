using CZ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZ.DTO.UserManagement
{
    public class UserViewModel
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public UserViewModel(User user)
        {
            this.Key = user.Key;
            this.Name = user.Name;
            this.LastName = user.LastName;
            this.Email = user.Email;
        }
    }
}
