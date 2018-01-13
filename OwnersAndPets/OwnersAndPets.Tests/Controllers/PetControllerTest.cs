using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OwnersAndPets.Controllers;
using OwnersAndPets.Interface;
using OwnersAndPets.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OwnersAndPets.Tests.Controllers
{
    [TestClass]
    public class PetControllerTest
    {
        private readonly Mock<IOwnerAccessor> _ownerAccessor = new Mock<IOwnerAccessor>();

        private PetController GetController(IOwnerAccessor ownerAccessor)
        {
            return new PetController(ownerAccessor);
        }

        [TestMethod]
        public void Cats()
        {
            // Arrange
            _ownerAccessor.Setup(x => x.GetCatOwners()).Returns(new List<OwnerModel>());

            // Act
            ViewResult result = GetController(_ownerAccessor.Object).Cats() as ViewResult;

            // Assert
            Xunit.Assert.NotNull(result);
        }
    }
}
