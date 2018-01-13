using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using Moq;
using OwnersAndPets.Interface;
using OwnersAndPets.Implementation;
using Xunit;
using OwnersAndPets.Models;
using System.Collections.Generic;
using OwnersAndPets.Tests.Extensions;
using System.Configuration;
using OwnersAndPets.Constants;
using System.IO;

namespace OwnersAndPets.Tests.Controllers
{
    [TestClass]
    public class OwnerAccessorTest
    {
        //private readonly Mock<IHttpHandler> _httpHandler = new Mock<IHttpHandler>();
        private readonly Mock<HttpClientHandler> _mockHandler = new Mock<HttpClientHandler>();

        private IOwnerAccessor GetAccessor(HttpClientHandler httpClientHandler)
        {
            ConfigurationManager.AppSettings[SettingsConstants.PEOPLE_SERVICE_URL] = "http://www.sample.com";
            return new OwnerAccessor(httpClientHandler); 
        }

        [TestMethod]
        public void GetCatOwners_WithValidJsonResponse_Succeeds()
        {
            //Arrange
            _mockHandler.SetupGetStringAsync(It.IsAny<Uri>(), "[{'name':'Steve','gender':'Male','age':45,'pets':null}]");

            //Act
            List<OwnerModel> owners = GetAccessor(_mockHandler.Object).GetCatOwners();

            //Assert
            Xunit.Assert.NotNull(owners);
        }

        [TestMethod]
        public void GetCatOwners_WithInvalidJsonResponse_ThrowsError()
        {
            //Arrange
            _mockHandler.SetupGetStringAsync(It.IsAny<Uri>(), Path.GetRandomFileName());

            //Act
            Exception exception = Record.Exception(() => GetAccessor(_mockHandler.Object).GetCatOwners());

            //Assert
            Xunit.Assert.Equal(OwnerAccessor.INVALID_JSON_RESPONSE,exception.Message);
        }

        [TestMethod]
        public void GetCatOwners_WithNullOrEmptyJsonResponse_ThrowsError()
        {
            //Arrange
            _mockHandler.SetupGetStringAsync(It.IsAny<Uri>(), string.Empty);

            //Act
            Exception exception = Record.Exception(() => GetAccessor(_mockHandler.Object).GetCatOwners());

            //Assert
            Xunit.Assert.Equal(OwnerAccessor.NO_AZURE_RESPONSE, exception.Message);
        }
    }
}
