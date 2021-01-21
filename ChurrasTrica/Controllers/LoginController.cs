using ChurrasTrica.Data.Data;
using ChurrasTrinca.Domain.Entities;
using ChurrasTrinca.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChurrasTrica.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILoginService _login;
        public LoginController(ILoginService login)
        {
            _login = login;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<object> Login([FromBody] LoginEntity loginEntity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _login.FindByLogin(loginEntity);

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("Usuario não encontrado");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
