using GustavoDocSpiderTeste.Models.Models;
using GustavoDocSpiderTeste.UnitTests.Mock;
using Xunit;

namespace GustavoDocSpiderTeste.UnitTests.Models.Models
{
    public class DocumentModelTests
    {
        private readonly DocumentModel _documentModel;

        public DocumentModelTests()
            => _documentModel = new DocumentModelMock().Create();

        [Fact]
        public void DocumentModel_Test()
            => Assert.NotNull(_documentModel);
    }
}
