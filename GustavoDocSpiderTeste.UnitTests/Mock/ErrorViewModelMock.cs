using AutoBogus;
using GustavoDocSpiderTeste.ViewModels.ViewModels;

namespace GustavoDocSpiderTeste.UnitTests.Mock
{
    public class ErrorViewModelMock
    {
        private readonly IAutoFaker _autoFaker;

        public ErrorViewModelMock()
            => _autoFaker = AutoFaker.Create();

        public ErrorViewModel Create()
        {
            return new ErrorViewModel
            {
                RequestId = _autoFaker.Generate<string>()
            };
        }
    }
}
