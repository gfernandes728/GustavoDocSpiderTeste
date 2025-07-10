using GustavoDocSpiderTeste.UnitTests.Mock;
using GustavoDocSpiderTeste.ViewModels.ViewModels;
using Xunit;

namespace GustavoDocSpiderTeste.UnitTests.ViewModels.ViewModels
{
    public class ErrorViewModelTest
    {
        private readonly ErrorViewModel _errorViewModel;

        public ErrorViewModelTest()
            => _errorViewModel = new ErrorViewModelMock().Create();

        [Fact]
        public void ErrorViewModel_Test()
            => Assert.NotNull(_errorViewModel);

        [Fact]
        public void ErrorViewModelShowRequestId_Test()
            => Assert.IsType<bool>(_errorViewModel.ShowRequestId);
    }
}
