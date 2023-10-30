using CZ.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZ.Domain.PublicationManagement
{
    public abstract class Publication : KeyEntity
    {
        public int UserId { get; set; }
        public User Author { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
    }
}
