using CZ.Domain.PublicationManagement;
using CZ.DTO.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZ.DTO.PublicationManagement
{
    public class PublicationImageViewModel : PublicationViewModel
    {
        public string PhotoContent { get; set; }
        public string Base64Image { get; set; }

        public PublicationImageViewModel(PublicationImage publicationImage)
        {
            this.Author = new UserViewModel(publicationImage.Author);
            this.Date = publicationImage.Date;
            this.Title = publicationImage.Title;
            this.Key = publicationImage.Key;
            this.Base64Image = publicationImage.Base64Image;
            this.PhotoContent = publicationImage.PhotoContent;
        }
    }
}
