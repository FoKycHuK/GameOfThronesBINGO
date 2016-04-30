using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using GoTB.Domain.Abstract;
using GoTB.Domain.Concrete;
using GoTB.Domain.Entities;
using GoTB.WebUI.Controllers;
using GoTB.WebUI.Infrastructure;
using GoTB.WebUI.Infrastructure.Abstract;
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

            Vote vote = null;
            var voteMock = new Mock<IVoteRepository>();
            voteMock.Setup(x => x.Add(It.IsAny<Vote>()))
                .Callback<Vote>(v => vote = v);

            var weekMock = new Mock<IWeekProvider>();
            weekMock.Setup(c => c.GetCurrentWeek()).Returns(3);

            var userMock = new Mock<IUserProvider>();
            userMock.Setup(x => x.IsAuthentificated(It.IsAny<Controller>())).Returns(true);
            userMock.Setup(x => x.GetUserName(It.IsAny<Controller>())).Returns("user");

            var controller = new CartController(repoMock.Object, voteMock.Object, cartMock.Object, weekMock.Object, userMock.Object);
            controller.Manage(3);
            controller.Manage(4);
            controller.Submit();

            Assert.IsTrue(vote != null);
            Assert.AreEqual("user", vote.User);
            Assert.AreEqual(3, vote.Week);
            Assert.AreEqual(2, vote.VoteItems.Count);
            Assert.IsTrue(vote.VoteItems.Any(vi => vi.Character.Id == 3));
            Assert.IsTrue(vote.VoteItems.Any(vi => vi.Character.Id == 4));

            //voteMock.Verify(x => x.Add(It.Is<Vote>(
            //    v => v.User == "user" && 
            //    v.Week == 3 && 
            //    v.VoteItems.Count == 2 && 
            //    v.VoteItems.Any(vi => vi.Character.Id == 3) &&
            //    v.VoteItems.Any(vi => vi.Character.Id == 4))));
        }
    }
}
