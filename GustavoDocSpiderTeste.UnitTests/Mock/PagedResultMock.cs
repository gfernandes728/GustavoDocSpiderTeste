using AutoBogus;
using GustavoDocSpiderTeste.ViewModels;

namespace GustavoDocSpiderTeste.UnitTests.Mock
{
    public class PagedResultMock
    {
        private readonly IAutoFaker _autoFaker;

        public PagedResultMock()
            => _autoFaker = AutoFaker.Create();

        public PagedResult<T> Create<T>()
        {
            return new PagedResult<T>
            {
                Items = _autoFaker.Generate<T>(2),
                PageNumber = _autoFaker.Generate<int>(),
                PageSize = _autoFaker.Generate<int>(),
                TotalItems = _autoFaker.Generate<int>()
            };
        }
    }
}
