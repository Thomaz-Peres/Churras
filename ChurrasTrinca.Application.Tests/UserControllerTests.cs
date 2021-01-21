using ChurrasTrica.Application.Controllers;
using ChurrasTrica.Data.Data;
using ChurrasTrica.Domain.Entities;
using ChurrasTrinca.Application.Tests.Database;
using ChurrasTrinca.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ChurrasTrinca.Application.Tests
{
    public class UserControllerTests : DatabaseTests, IClassFixture<DbUser>
    {
        private ServiceProvider _serviceProvide;
        public UserControllerTests(DbUser dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact]
        public async Task Post_Retorna_Ok()
        {
            var serviceMock = new Mock<IUserService>();
            var user = new UserEntity(1, "thomaz", 200, true, true, false);

            serviceMock.Setup(x => x.Insert(It.IsAny<UserEntity>())).ReturnsAsync(user);

            var controller = new UsersController(serviceMock.Object, _serviceProvide.GetService<DataContext>());

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("https://localhost:5001");
            controller.Url = url.Object;

            var result = await controller.Post(user);
            Assert.True(result is OkObjectResult);
        }

        [Fact]
        public async Task Post_Retorna_BadRequest()
        {
            var serviceMock = new Mock<IUserService>();
            var user = new UserEntity(1, "thomaz", 200, true, true, false);

            serviceMock.Setup(x => x.Insert(It.IsAny<UserEntity>())).ReturnsAsync(user);

            var controller = new UsersController(serviceMock.Object, _serviceProvide.GetService<DataContext>());
            controller.ModelState.AddModelError("Name", "É um campo obrigatorio");

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("https://localhost:5001");
            controller.Url = url.Object;

            var result = await controller.Post(user);
            Assert.True(result is BadRequestObjectResult);
        }

        [Fact]
        public async Task Delete_Retorna_Ok()
        {
            var serviceMock = new Mock<IUserService>();
            var user = new UserEntity(1, "thomaz", 200, true, true, false);

            serviceMock.Setup(x => x.Delete(user)).ReturnsAsync(true);

            var controller = new UsersController(serviceMock.Object, _serviceProvide.GetService<DataContext>());

            var result = await controller.Delete(user);
            Assert.True(result is OkObjectResult);
        }

        [Fact]
        public async Task Delete_Retorna_BadRequest()
        {
            var serviceMock = new Mock<IUserService>();
            var user = new UserEntity(1, "thomaz", 200, true, true, false);

            serviceMock.Setup(x => x.Delete(user)).ReturnsAsync(false);

            var controller = new UsersController(serviceMock.Object, _serviceProvide.GetService<DataContext>());
            controller.ModelState.AddModelError("Name", "É um campo obrigatorio");

            var result = await controller.Delete(user);
            Assert.True(result is BadRequestObjectResult);
            Assert.False(controller.ModelState.IsValid);
        }

        [Fact]
        public async Task GetAll_Retorna_Ok()
        {
            var serviceMock = new Mock<IUserService>();
            List<UserEntity> listaUsers = new();

            for (int i = 0; i < 5; i++)
            {
                var users = new UserEntity(i, "thomaz", 200, true, true, false);
                listaUsers.Add(users);
            }

            serviceMock.Setup(x => x.GetAll()).ReturnsAsync(listaUsers);


            var controller = new UsersController(serviceMock.Object, _serviceProvide.GetService<DataContext>());

            var result = await controller.GetAll();
            Assert.True(result is OkObjectResult);

            Assert.True(listaUsers.Count() == 5);
        }

        [Fact]
        public async Task GetAll_Retorna_BadRequest()
        {
            var serviceMock = new Mock<IUserService>();
            List<UserEntity> listaUsers = new();

            for (int i = 0; i < 5; i++)
            {
                var users = new UserEntity(i, "thomaz", 200, true, true, false);
                listaUsers.Add(users);
            }

            serviceMock.Setup(x => x.GetAll()).ReturnsAsync(listaUsers);

            var controller = new UsersController(serviceMock.Object, _serviceProvide.GetService<DataContext>());
            controller.ModelState.AddModelError("UserID", "Ele ja vai pro churras");

            var result = await controller.GetAll();
            Assert.True(result is BadRequestObjectResult);
        }
    }
}
