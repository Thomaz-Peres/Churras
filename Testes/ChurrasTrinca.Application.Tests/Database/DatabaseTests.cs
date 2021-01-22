using System;
using ChurrasTrica.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ChurrasTrinca.Application.Tests.Database
{
    public abstract class DatabaseTests
    {
        public DatabaseTests()
        {

        }
    }
    public class DbUser : IDisposable
    {
        private readonly string dataBaseName = $"Data Source = TesteDbUser";
        public ServiceProvider ServiceProvider { get; private set; }

        public DbUser()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<DataContext>(o =>
                o.UseSqlite($"{dataBaseName}"),
                  ServiceLifetime.Transient
            );

            ServiceProvider = serviceCollection.BuildServiceProvider();
            using (var context = ServiceProvider.GetService<DataContext>())
            {
                context.Database.EnsureCreated();
            }
        }

        public void Dispose()
        {
            using (var context = ServiceProvider.GetService<DataContext>())
            {
                context.Database.EnsureDeleted();
            }
        }
    }

    public class DbChurras : IDisposable
    {
        private readonly string dataBaseName = $"Data Source = TesteDbChurras";
        public ServiceProvider ServiceProvider { get; private set; }

        public DbChurras()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<DataContext>(o =>
                o.UseSqlite($"{dataBaseName}"),
                  ServiceLifetime.Transient
            );

            ServiceProvider = serviceCollection.BuildServiceProvider();
            using (var context = ServiceProvider.GetService<DataContext>())
            {
                context.Database.EnsureCreated();
            }
        }

        public void Dispose()
        {
            using (var context = ServiceProvider.GetService<DataContext>())
            {
                context.Database.EnsureDeleted();
            }
        }
    }

}
