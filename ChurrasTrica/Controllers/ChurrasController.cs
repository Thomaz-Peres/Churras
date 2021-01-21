using ChurrasTrica.Data.Data;
using ChurrasTrica.Domain.Entities;
using ChurrasTrinca.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChurrasTrica.Application.Controllers
{
    [Route("[controller]")]
    public class ChurrasController : Controller
    {
        private readonly IChurrasService _churrasService;
        private readonly DataContext _dataContext;

        public ChurrasController(IChurrasService churrasService, DataContext dataContext)
        {
            _churrasService = churrasService;
            _dataContext = dataContext;
        }

        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var churras = await _dataContext.Set<ChurrasEntity>().AsNoTracking().Include(x => x.Participants).OrderBy(x => x.ChurrasID).ToListAsync();

                return Ok(churras);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("")]
        [Authorize("Bearer")]
        public async Task<IActionResult> Post([FromBody] ChurrasEntity churrasEntity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _churrasService.Insert(churrasEntity);
                return Ok($"Churras marcado com sucesso");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Delete([FromBody] ChurrasEntity churrasEntity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _churrasService.Delete(churrasEntity);
                return Ok($"Churras desmarcado com sucesso");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
