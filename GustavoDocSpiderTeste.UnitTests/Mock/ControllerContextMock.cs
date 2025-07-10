using AutoBogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GustavoDocSpiderTeste.UnitTests.Mock
{
    public class ControllerContextMock
    {
        private readonly IAutoFaker _autoFaker;

        public ControllerContextMock()
            => _autoFaker = AutoFaker.Create();

        public ControllerContext Create()
        {
            return new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
                {
                    TraceIdentifier = _autoFaker.Generate<string>()
                }
            };
        }
    }
}
