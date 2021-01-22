using ChurrasTrica.Domain.Entities;
using ChurrasTrinca.Domain.Interfaces.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurrasTrinca.Services.Tests.Userrs
{
    [TestFixture]
    public class UserServiceTests
    {
        private IUserService _userService;

        [Test]
        public async Task E_Possivel_Adicionar_Participante()
        {
            var serviceMock = new Mock<IUserService>();

            var user = new UserEntity(1, "thomaz", 200, true, true, false);

            serviceMock.Setup(x => x.Insert(It.IsAny<UserEntity>())).ReturnsAsync(user);
            _userService = serviceMock.Object;

            var result = await _userService.Insert(user);
            Assert.NotNull(result);
        }

        [Test]
        public async Task E_Possivel_Deletar_Participante()
        {
            var serviceMock = new Mock<IUserService>();

            var user = new UserEntity(1, "thomaz", 200, true, true, false);

            serviceMock.Setup(x => x.Delete(user)).ReturnsAsync(true);
            _userService = serviceMock.Object;

            var deleted = await _userService.Delete(user);
            Assert.True(deleted);
        }

        [Test]
        public async Task E_Possivel_Visualizar_Todos_Participante()
        {
            var serviceMock = new Mock<IUserService>();
            List<UserEntity> listaUsers = new();

            for (int i = 0; i < 5; i++)
            {
                var users = new UserEntity(i, "thomaz", 200, true, true, false);
                listaUsers.Add(users);
            }
            

            serviceMock.Setup(x => x.GetAll()).ReturnsAsync(listaUsers);
            _userService = serviceMock.Object;

            var result = await _userService.GetAll();
            Assert.NotNull(result);
            Assert.True(result.Count() == 5);
        }
    }
}