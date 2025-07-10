using GustavoDocSpiderTeste.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace GustavoDocSpiderTeste.UnitTests.Mock
{
    public class ApplicationDbContextMock
    {
        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDb_{System.Guid.NewGuid()}")
                .Options;

            return new ApplicationDbContext(options);
        }
    }
}
