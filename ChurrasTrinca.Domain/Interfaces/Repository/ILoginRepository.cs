using ChurrasTrinca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurrasTrinca.Domain.Interfaces.Repository
{
    public interface ILoginRepository : IRepositoryBase<LoginEntity>
    {
        Task<LoginEntity> FindByLogin(string email, string name, string password);
    }
}
