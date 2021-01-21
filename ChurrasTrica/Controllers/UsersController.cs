using ChurrasTrica.Data.Data;
using ChurrasTrica.Domain.Entities;
using ChurrasTrinca.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChurrasTrica.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly DataContext _dataContext;
        public UsersController(IUserService userService, DataContext dataContext)
        {
            _userService = userService;
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
                var users = await _dataContext.Set<UserEntity>().AsNoTracking().Include(x => x.Churras).OrderBy(x => x.UserID).ToListAsync();

                return Ok(users);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("")]
        [Authorize("Bearer")]
        public async Task<IActionResult> Post([FromBody] UserEntity userEntity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _userService.Insert(userEntity);
                return Ok($"Participante adicionado com sucesso");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize("Bearer")]
        public async Task<IActionResult> Delete([FromBody] UserEntity userEntity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _userService.Delete(userEntity);
                return Ok($"Participante removido com sucesso");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
