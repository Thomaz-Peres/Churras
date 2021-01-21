using ChurrasTrica.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurrasTrinca.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> Delete(UserEntity userEntity);
        Task<IEnumerable<UserEntity>> GetAll();
        Task<UserEntity> Insert(UserEntity user);
    }
}
