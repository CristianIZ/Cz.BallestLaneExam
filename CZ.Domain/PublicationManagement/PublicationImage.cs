using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZ.Domain.PublicationManagement
{
    public class PublicationImage : Publication
    {
        public string PhotoContent { get; set; }
        public string Base64Image { get; set; }
    }
}
