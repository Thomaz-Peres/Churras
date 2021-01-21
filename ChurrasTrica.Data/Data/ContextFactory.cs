using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ChurrasTrica.Data.Data
{
    public class ContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var connectionString = "Data Source=ChurrasTrinca.db";
            var optionBuilder = new DbContextOptionsBuilder<DataContext>();
            optionBuilder.UseSqlite(connectionString);
            return new DataContext(optionBuilder.Options);
        }
    }
}
