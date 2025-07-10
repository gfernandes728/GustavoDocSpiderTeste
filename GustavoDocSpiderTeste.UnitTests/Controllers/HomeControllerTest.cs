using AutoBogus;
using GustavoDocSpiderTeste.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Xunit;

namespace GustavoDocSpiderTeste.UnitTests.Controllers
{
    public class HomeControllerTest
    {
        private readonly IAutoFaker _autoFaker;
        private readonly HomeController _homeController;

        public HomeControllerTest()
        {
            _autoFaker = AutoFaker.Create();
            _homeController = new HomeController();
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
            => Assert.NotNull(_homeController.Error());

        [Fact]
        public void Error_ActivityCurrent_Test()
        {
            var activity = new Activity(_autoFaker.Generate<string>());
            activity.Start();
            Activity.Current = activity;

            Assert.NotNull(_homeController.Error());
        }

        [Fact]
        public void Error_HttpContext_Test()
        {
            _homeController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
                {
                    TraceIdentifier = "trace-xyz-123"
                }
            };

            Assert.NotNull(_homeController.Error());
        }

        #endregion
    }
}
