using CZ.Domain.PublicationManagement;
using CZ.DTO.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZ.DTO.PublicationManagement
{
    public class PublicationTextViewModel : PublicationViewModel
    {
        public string Text { get; set; }

        public PublicationTextViewModel(PublicationText publicationText)
        {
            this.Author = new UserViewModel(publicationText.Author);
            this.Text = publicationText.Text;
            this.Date = publicationText.Date;
            this.Title = publicationText.Title;
            this.Key = publicationText.Key;
        }
    }
}
