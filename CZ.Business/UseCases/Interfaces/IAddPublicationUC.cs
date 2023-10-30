using CZ.DTO.PublicationManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZ.Business.UseCases.Interfaces
{
    public interface IAddPublicationUC
    {
        Task<PublicationTextViewModel> CreatePublicationText(PublicationTextDto publicationDto);
        Task<PublicationImageViewModel> CreatePublicationImage(PublicationImageDto publicationDto);
    }
}
