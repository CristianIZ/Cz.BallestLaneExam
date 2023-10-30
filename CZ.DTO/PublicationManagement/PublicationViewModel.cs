using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZ.DTO.UserManagement;

namespace CZ.DTO.PublicationManagement
{
    public abstract class PublicationViewModel
    {
        public string Key { get; set; }
        public UserViewModel Author { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
    }
}
