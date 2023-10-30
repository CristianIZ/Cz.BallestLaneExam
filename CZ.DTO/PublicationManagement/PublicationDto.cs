using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZ.DTO.UserManagement;

namespace CZ.DTO.PublicationManagement
{
    public abstract class PublicationDto
    {
        public string Key { get; set; }
        public string UserKey { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
    }
}
