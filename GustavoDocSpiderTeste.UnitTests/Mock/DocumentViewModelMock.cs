using AutoBogus;
using GustavoDocSpiderTeste.ViewModels.ViewModels;
using Microsoft.AspNetCore.Http;

namespace GustavoDocSpiderTeste.UnitTests.Mock
{
    public class DocumentViewModelMock
    {
        private readonly IAutoFaker _autoFaker;

        private readonly IFormFileMock _iFormFileMock;
        private readonly IFormFile _iFormFile;

        public DocumentViewModelMock()
        {
            _autoFaker = AutoFaker.Create();

            _iFormFileMock = new IFormFileMock();
            _iFormFile = _iFormFileMock.CreateFakeFormFile();
        }

        public DocumentViewModel Create()
            => CreateWithFile(_iFormFile);

        public DocumentViewModel CreateWithForbiddenFileExtensions()
            => CreateWithFile(_iFormFileMock.CreateFakeFormFile(".zip"));

        public DocumentViewModel CreateWithFile(IFormFile file)
        {
            return new DocumentViewModel
            {
                Id = _autoFaker.Generate<int>(),
                Title = _autoFaker.Generate<string>(),
                Description = _autoFaker.Generate<string>(),
                FileName = _autoFaker.Generate<string>(),
                File = file
            };
        }
    }
}
