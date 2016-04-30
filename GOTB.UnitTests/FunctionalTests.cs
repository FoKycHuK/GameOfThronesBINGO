using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoTB.Domain.Abstract;
using GoTB.Domain.Concrete;
using GoTB.Domain.Entities;
using GoTB.WebUI.Controllers;
using GoTB.WebUI.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GoTB.UnitTests
{
    [TestClass]
    public class FunctionalTests
    {
        [TestMethod]
        public void FunctionalTest()
        {
            var cartMock = new Mock<ICartProvider>();
            var cart = new Cart() { Points = 10 };
            cartMock.Setup(
                    c => c.GetCart(
                        It.IsAny<CartController>()))
                            .Returns(cart);
            var repoMock = new Mock<ICharacterRepository>();
            repoMock.Setup(r => r.Characters)
                    .Returns(new Character[]
                    {
                    new Character() { Id = 1, Price = 100},
                    new Character() { Id = 2, Price = 4},
                    new Character() { Id = 3, Price = 8},
                    new Character() { Id = 4, Price = 2},
                    }.AsQueryable()
                    );

            var voteMock = new Mock<IVoteRepository>();
            voteMock.Setup(v => v.Votes);

            var weekMock = new Mock<IWeekProvider>();
            weekMock.Setup(c => c.GetCurrentWeek()).Returns(3);

            var controller = new CartController(repoMock.Object, voteMock.Object, cartMock.Object, weekMock.Object);
            controller.Manage(3);
            controller.Manage(4);
            controller.Submit();


            voteMock.Verify(x => x.Add(It.IsAny<Vote>()));

        }
    }
}
