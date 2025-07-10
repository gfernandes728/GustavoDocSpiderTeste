using AutoBogus;
using System.Diagnostics;

namespace GustavoDocSpiderTeste.UnitTests.Mock
{
    public class ActivityMock
    {
        private readonly IAutoFaker _autoFaker;

        public ActivityMock()
            => _autoFaker = AutoFaker.Create();

        public Activity Create()
        {
            var activity = new Activity(_autoFaker.Generate<string>());
            activity.Start();

            return activity;
        }
    }
}
