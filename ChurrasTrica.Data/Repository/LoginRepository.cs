using ChurrasTrica.Data.Data;
using ChurrasTrinca.Domain.Entities;
using ChurrasTrinca.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ChurrasTrica.Data.Repository
{
    public class LoginRepository : BaseRepository<LoginEntity>, ILoginRepository
    {
        private DbSet<LoginEntity> _dataSet;
        public LoginRepository(DataContext dataContext) : base(dataContext)
        {
            _dataSet = dataContext.Set<LoginEntity>();
        }
        public async Task<LoginEntity> FindByLogin(string email, string name, string password)
        {
            return await _dataSet.FirstOrDefaultAsync(u => u.Email.Equals(email) && u.Name.Equals(name) && u.Password.Equals(password));
        }
    }
}
