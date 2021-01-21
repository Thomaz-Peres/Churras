using ChurrasTrica.Domain.Entities;
using ChurrasTrinca.Domain.Interfaces.Repository;
using ChurrasTrinca.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurrasTrinca.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryBase<UserEntity> _userRepository;

        public UserService(IRepositoryBase<UserEntity> userRepository) => this._userRepository = userRepository;

        public async Task<bool> Delete(UserEntity user)
        {
            return await _userRepository.Delete(user);
        }

        public async Task<IEnumerable<UserEntity>> GetAll()
        {
            return await _userRepository.SelectAll();
        }

        public async Task<UserEntity> Insert(UserEntity user)
        {
            return await _userRepository.Insert(user);
        }
    }
}
