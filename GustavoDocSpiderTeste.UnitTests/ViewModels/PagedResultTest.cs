using GustavoDocSpiderTeste.UnitTests.Mock;
using GustavoDocSpiderTeste.ViewModels;
using GustavoDocSpiderTeste.ViewModels.ViewModels;
using Xunit;

namespace GustavoDocSpiderTeste.UnitTests.ViewModels
{
    public class PagedResultTest
    {
        private readonly PagedResult<DocumentViewModel> _pagedResult;

        public PagedResultTest()
            => _pagedResult = new PagedResultMock().Create<DocumentViewModel>();

        [Fact]
        public void PagedResult_Test()
            => Assert.NotNull(_pagedResult);

        [Fact]
        public void PagedResultTotalPages_Test()
            => Assert.IsType<int>(_pagedResult.TotalPages);
    }
}
