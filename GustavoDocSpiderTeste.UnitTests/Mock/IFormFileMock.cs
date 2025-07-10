using AutoBogus;
using Microsoft.AspNetCore.Http;
using Moq;
using System.IO;
using System.Text;

namespace GustavoDocSpiderTeste.UnitTests.Mock
{
    public class IFormFileMock
    {
        private readonly IAutoFaker _autoFaker;

        public IFormFileMock()
            => _autoFaker = AutoFaker.Create();

        public IFormFile CreateFakeFormFile(string fileExtension = "")
        {
            var contentBytes = Encoding.UTF8.GetBytes(_autoFaker.Generate<string>());
            var stream = new MemoryStream(contentBytes);

            var formFileMock = new Mock<IFormFile>();
            formFileMock.Setup(f => f.FileName).Returns($"{_autoFaker.Generate<string>()}{fileExtension}");
            formFileMock.Setup(f => f.Length).Returns(stream.Length);
            formFileMock.Setup(f => f.OpenReadStream()).Returns(stream);
            formFileMock.Setup(f => f.ContentType).Returns(_autoFaker.Generate<string>());
            formFileMock.Setup(f => f.Name).Returns("File");

            return formFileMock.Object;
        }
    }
}
