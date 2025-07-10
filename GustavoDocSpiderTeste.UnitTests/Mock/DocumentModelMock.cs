using System;
using AutoBogus;
using GustavoDocSpiderTeste.Models.Models;

namespace GustavoDocSpiderTeste.UnitTests.Mock
{
    public class DocumentModelMock
    {
        private readonly IAutoFaker _autoFaker;

        public DocumentModelMock()
            => _autoFaker = AutoFaker.Create();

        public DocumentModel Create()
            => CreateWithFileData(_autoFaker.Generate<byte[]>());

        public DocumentModel CreateWithFileData(byte[] fileData)
        {
            return new DocumentModel
            {
                Id = _autoFaker.Generate<int>(),
                Title = _autoFaker.Generate<string>(),
                Description = _autoFaker.Generate<string>(),
                FileName = _autoFaker.Generate<string>(),
                FileData = fileData,
                CreatedAt = _autoFaker.Generate<DateTime>()
            };
        }
    }
}
