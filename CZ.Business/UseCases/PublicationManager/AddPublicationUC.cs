using CZ.Business.Services;
using CZ.Business.UseCases.Interfaces;
using CZ.Domain.PublicationManagement;
using CZ.DTO.PublicationManagement;
using CZ.SQLContext.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZ.Business.UseCases.PublicationManager
{
    public class AddPublicationUC : IAddPublicationUC
    {
        private readonly IAccountServices _accountServices;
        private readonly IUserRepository _userRepository;
        private readonly IPublicationTextRepository _publicationTextRepository;
        private readonly IPublicationImageRepository _publicationImageRepository;

        public AddPublicationUC(IAccountServices accountServices,
            IUserRepository userRepository,
            IPublicationTextRepository publicationTextRepository,
            IPublicationImageRepository publicationImageRepository)
        {
            this._accountServices = accountServices;
            this._userRepository = userRepository;
            this._publicationTextRepository = publicationTextRepository;
            this._publicationImageRepository = publicationImageRepository;
        }

        public async Task<PublicationTextViewModel> CreatePublicationText(PublicationTextDto publicationDto)
        {
            var author = await _userRepository.GetByKey(publicationDto.UserKey);

            var publication = new PublicationText()
            {
                Author = author,
                Date = DateTime.Now,
                Title = publicationDto.Title,
                Text = publicationDto.Text,
                UserId = author.Id
            };

            await this._publicationTextRepository.Add(publication);

            return new PublicationTextViewModel(publication);
        }

        public async Task<PublicationImageViewModel> CreatePublicationImage(PublicationImageDto publicationDto)
        {
            var author = await _userRepository.GetByKey(publicationDto.UserKey);

            var publication = new PublicationImage()
            {
                Author = author,
                Date = DateTime.Now,
                Title = publicationDto.Title,
                PhotoContent = publicationDto.PhotoContent,
                Base64Image = publicationDto.Base64Image,
                UserId = author.Id
            };

            await this._publicationImageRepository.Add(publication);

            return new PublicationImageViewModel(publication);
        }
    }
}
