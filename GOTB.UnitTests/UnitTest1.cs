using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GoTB.Domain.Abstract;
using GoTB.Domain.Entities;
using GoTB.WebUI.Controllers;
using GoTB.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GoTB.WebUI.HtmlHelpers;

namespace GoTB.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            Mock<ICharacterRepository> mock = new Mock<ICharacterRepository>();
            mock.Setup(m => m.Characters).Returns(new Character[]
            {
                new Character {Id = 1, Name = "P1"},
                new Character {Id = 2, Name = "P2"},
                new Character {Id = 3, Name = "P3"},
                new Character {Id = 4, Name = "P4"},
                new Character {Id = 5, Name = "P5"}
            }.AsQueryable());
            HomeController controller = new HomeController(mock.Object);
            controller.PageSize = 3;
            //Act
            var result =  (CharactersListViewModel)controller.Index(2).Model;
            // Assert
            Character[] prodArray = result.Characters.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
        }

        [TestMethod]
        public void Can_Paginate()
        {
            // Arrange
            Mock<ICharacterRepository> mock = new Mock<ICharacterRepository>();
            mock.Setup(m => m.Characters).Returns(
                new Character[]
                {
                    new Character {Id = 1, Name = "P1"},
                    new Character {Id = 2, Name = "P2"},
                    new Character {Id = 3, Name = "P3"},
                    new Character {Id = 4, Name = "P4"},
                    new Character {Id = 5, Name = "P5"}
                 }.AsQueryable());
            HomeController controller = new HomeController(mock.Object);
            controller.PageSize = 3;
            // Act
            var result = (CharactersListViewModel)controller.Index(2).Model;
            // Assert
            Character[] prodArray = result.Characters.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
        }

        [TestMethod]
        public void CanFilter()
        {
            Mock<ICharacterRepository> mock = new Mock<ICharacterRepository>();
            mock.Setup(m => m.Characters).Returns(
                new Character[]
                {
                    new Character {Id = 1, Name = "P1", Price = 1},
                    new Character {Id = 2, Name = "P2", Price = 2},
                    new Character {Id = 3, Name = "P3", Price = 3},
                    new Character {Id = 4, Name = "P4", Price = 4},
                    new Character {Id = 5, Name = "P5", Price = 5}
                 }.AsQueryable());
            var controller = new HomeController(mock.Object);
            var res = (CharactersListViewModel) controller.Index(2, FilterBy.PriceLessThenThree).Model;
            var array = res.Characters.ToArray();
            Assert.IsTrue(array.Length == 2);
            Assert.AreEqual(array[0].Name, "P4");
            Assert.AreEqual(array[1].Name, "P5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            // Arrange - define an HTML helper - we need to do this
            // in order to apply the extension method
            HtmlHelper myHelper = null;
            // Arrange - create PagingInfo data
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };
            // Arrange - set up the delegate using a lambda expression
            Func<int, string> pageUrlDelegate = i => "Page" + i;
            // Act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);
            // Assert
            Assert.AreEqual(result.ToString(), @"<a href=""Page1"">1</a>"
                                               + @"<a class=""selected"" href=""Page2"">2</a>"
                                               + @"<a href=""Page3"">3</a>");
        }
    }
}



