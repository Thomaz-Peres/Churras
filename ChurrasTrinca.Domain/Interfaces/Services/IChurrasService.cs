using ChurrasTrica.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurrasTrinca.Domain.Interfaces.Services
{
    public interface IChurrasService
    {
        Task<bool> Delete(ChurrasEntity churras);
        Task<IEnumerable<ChurrasEntity>> GetAll();
        Task<ChurrasEntity> Insert(ChurrasEntity churras);
    }
}
