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
    public class ChurrasService : IChurrasService
    {
        private readonly IRepositoryBase<ChurrasEntity> _churrasRepository;
        public ChurrasService(IRepositoryBase<ChurrasEntity> churrasRepository) => this._churrasRepository = churrasRepository;

        public async Task<bool> Delete(ChurrasEntity churras)
        {
            return await _churrasRepository.Delete(churras);
        }

        public async Task<IEnumerable<ChurrasEntity>> GetAll()
        {
            return await _churrasRepository.SelectAll();
        }

        public async Task<ChurrasEntity> Insert(ChurrasEntity churras)
        {
            return await _churrasRepository.Insert(churras);
        }
    }
}
