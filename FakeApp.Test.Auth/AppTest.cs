using FakeApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;



namespace FakeApp.Test.Auth
{
    public abstract class AppTest
    {
        protected AppDbContext GetDbContext()
        {
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql("Server=127.0.0.1;Port=5432;Database=fakeapptest;Username=ilnar861;Password=qwerty1234")
                .Options;
            
            return new AppDbContext(dbContextOptions);
        }
    }
}