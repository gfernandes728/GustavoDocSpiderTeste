using GustavoDocSpiderTeste.UnitTests.Mock;
using GustavoDocSpiderTeste.ViewModels.ViewModels;
using Xunit;

namespace GustavoDocSpiderTeste.UnitTests.ViewModels.ViewModels
{
    public class DocumentViewModelTest
    {
        private readonly DocumentViewModel _documentViewModel;

        public DocumentViewModelTest()
            => _documentViewModel = new DocumentViewModelMock().Create();

        [Fact]
        public void DocumentViewModel_Test()
            => Assert.NotNull(_documentViewModel);
    }
}
