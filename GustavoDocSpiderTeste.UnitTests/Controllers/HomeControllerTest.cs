using AutoBogus;
using Bogus.Extensions;
using GustavoDocSpiderTeste.Controllers;
using GustavoDocSpiderTeste.UnitTests.Mock;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Xunit;

namespace GustavoDocSpiderTeste.UnitTests.Controllers
{
    public class HomeControllerTest
    {
        private readonly IAutoFaker _autoFaker;
        private readonly HomeController _homeController;
        private readonly ControllerContextMock _controllerContext;
        private readonly ActivityMock _activity;

        public HomeControllerTest()
        {
            _autoFaker = AutoFaker.Create();
            _homeController = new HomeController();
            _controllerContext = new ControllerContextMock();
            _activity = new ActivityMock();
        }

        [Fact]
        public void Index_Test()
            => Assert.NotNull(_homeController.Index());

        [Fact]
        public void Privacy_Test()
            => Assert.NotNull(_homeController.Privacy());

        #region - Test : Error -

        [Fact]
        public void Error_Test()
            => Assert.NotNull(Error());

        [Fact]
        public void Error_ActivityCurrent_Test()
        {
            Activity.Current = _activity.Create();
            Assert.NotNull(Error());
        }

        [Fact]
        public void Error_HttpContext_Test()
        {
            _homeController.ControllerContext = _controllerContext.Create();
            Assert.NotNull(Error());
        }

        #endregion

        #region - Private methods -

        private IActionResult Error()
            => _homeController.Error();

        #endregion
    }
}
