using CZ.Business.Services;
using CZ.Domain;
using CZ.SQLContext.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CZ.Test
{
    [TestFixture]
    public class AccountServicesTests
    {
        private AccountServices _accountServices;
        private Mock<IConfiguration> _configurationMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IRefreshTokenRepository> _refreshTokenRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _configurationMock = new Mock<IConfiguration>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _refreshTokenRepositoryMock = new Mock<IRefreshTokenRepository>();

            var jwtConfigurationSectionMock = new Mock<IConfigurationSection>();
            jwtConfigurationSectionMock.Setup(x => x["Key"]).Returns("IE93~eF4zBXI95-EGkB-Y1oP9tZCvvVpNNzInXrToAhvur2vGOEIZU-dm34QvkH1LMGMYLfR6aA_9R6bUZDu76MYMjQ");
            jwtConfigurationSectionMock.Setup(x => x["Issuer"]).Returns("http://localhost:7200/");
            jwtConfigurationSectionMock.Setup(x => x["Audience"]).Returns("http://localhost:7200/");
            jwtConfigurationSectionMock.Setup(x => x["Subject"]).Returns("baseWebApiSubject");

            _configurationMock.Setup(x => x.GetSection("Jwt")).Returns(jwtConfigurationSectionMock.Object);

            _accountServices = new AccountServices(
                _configurationMock.Object,
                _httpContextAccessorMock.Object,
                _userRepositoryMock.Object,
                _refreshTokenRepositoryMock.Object
            );
        }

        [Test]
        public async Task GenerateToken_ValidUser_ReturnsTokenResultDTO()
        {
            // Arrange
            var user = new User { Id = 1, Name = "Test User", Key = "testkey", Email = "test@example.com" };

            // Mocking configuration
            _configurationMock.Setup(c => c.GetSection("Jwt")).Returns(new Mock<IConfigurationSection>().Object);

            // Mocking refresh token repository
            _refreshTokenRepositoryMock.Setup(r => r.AddRefreshToken(It.IsAny<RefreshToken>()));

            // Act
            var result = await _accountServices.GenerateToken(user);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Token);
            Assert.IsNotNull(result.Expiration);
            Assert.IsNotNull(result.RefreshToken);
        }

        [Test]
        public void GetUserFromToken_InvalidIdentity_ThrowsException()
        {
            // Arrange
            var invalidIdentity = new ClaimsIdentity();

            // Act & Assert
            Assert.Throws<Exception>(() => _accountServices.GetUserFromToken(invalidIdentity));
        }

        // Similar tests can be written for other scenarios for the GetUserFromToken and RefreshToken methods.
    }
}
