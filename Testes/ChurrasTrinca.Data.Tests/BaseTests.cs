using ChurrasTrica.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ChurrasTrinca.Data.Tests
{
    public abstract class BaseTest
    {
        public BaseTest()
        {

        }
    }

    public class DbTeste : IDisposable
    {
        private string dataBaseName = $"DbChurrasTeste_{Guid.NewGuid().ToString().Replace("-", string.Empty)}.db";
        public ServiceProvider ServiceProvider { get; private set; }

        public DbTeste()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<DataContext>(o =>
                o.UseSqlite($"Persist Security Info=True;Server=localhost;Data Source={dataBaseName}"),
                  ServiceLifetime.Transient
            );

            ServiceProvider = serviceCollection.BuildServiceProvider();
            using var context = ServiceProvider.GetService<DataContext>();
            context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            using var context = ServiceProvider.GetService<DataContext>();
            context.Database.EnsureDeleted();
        }
    }
}