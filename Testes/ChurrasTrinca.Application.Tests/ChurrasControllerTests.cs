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

    public class ChurrasControllerTests : DatabaseTests, IClassFixture<DbChurras>
    {
        private ServiceProvider _serviceProvide;
        public ChurrasControllerTests(DbChurras dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact]
        public async Task Post_Retorna_Ok()
        {
            var serviceMock = new Mock<IChurrasService>();
            var churras = new ChurrasEntity(1, "thomaz", DateTime.Now, "churras", 10, 10);

            serviceMock.Setup(x => x.Insert(It.IsAny<ChurrasEntity>())).ReturnsAsync(churras);

            var controller = new ChurrasController(serviceMock.Object, _serviceProvide.GetService<DataContext>());

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("https://localhost:5001");
            controller.Url = url.Object;

            var result = await controller.Post(churras);
            Assert.True(result is OkObjectResult);
        }

        [Fact]
        public async Task Post_Retorna_BadRequest()
        {
            var serviceMock = new Mock<IChurrasService>();
            var churras = new ChurrasEntity(1, "thomaz", DateTime.Now, "churras", 10, 10);

            serviceMock.Setup(x => x.Insert(It.IsAny<ChurrasEntity>())).ReturnsAsync(churras);

            var controller = new ChurrasController(serviceMock.Object, _serviceProvide.GetService<DataContext>());
            controller.ModelState.AddModelError("Name", "É um campo obrigatorio");

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("https://localhost:5001");
            controller.Url = url.Object;

            var result = await controller.Post(churras);
            Assert.True(result is BadRequestObjectResult);
        }

        [Fact]
        public async Task Delete_Retorna_Ok()
        {
            var serviceMock = new Mock<IChurrasService>();
            var churras = new ChurrasEntity(1, "thomaz", DateTime.Now, "churras", 10, 10);

            serviceMock.Setup(x => x.Delete(churras)).ReturnsAsync(true);

            var controller = new ChurrasController(serviceMock.Object, _serviceProvide.GetService<DataContext>());

            var result = await controller.Delete(churras);
            Assert.True(result is OkObjectResult);
        }

        [Fact]
        public async Task Delete_Retorna_BadRequest()
        {
            var serviceMock = new Mock<IChurrasService>();
            var churras = new ChurrasEntity(1, "thomaz", DateTime.Now, "churras", 10, 10);

            serviceMock.Setup(x => x.Delete(churras)).ReturnsAsync(false);

            var controller = new ChurrasController(serviceMock.Object, _serviceProvide.GetService<DataContext>());
            controller.ModelState.AddModelError("Name", "É um campo obrigatorio");

            var result = await controller.Delete(churras);
            Assert.True(result is BadRequestObjectResult);
            Assert.False(controller.ModelState.IsValid);
        }

        [Fact]
        public async Task GetAll_Retorna_Ok()
        {
            var serviceMock = new Mock<IChurrasService>();
            List<ChurrasEntity> listaChurras = new List<ChurrasEntity>();

            for (int i = 0; i < 5; i++)
            {
                var churras = new ChurrasEntity(i, "thomaz", DateTime.Now, "churras", 10, 10);
                listaChurras.Add(churras);
            }

            serviceMock.Setup(x => x.GetAll()).ReturnsAsync(listaChurras);

            var controller = new ChurrasController(serviceMock.Object, _serviceProvide.GetService<DataContext>());


            var result = await controller.GetAll();
            Assert.True(result is OkObjectResult);

            Assert.True(listaChurras.Count() == 5);
        }

        [Fact]
        public async Task GetAll_Retorna_BadRequest()
        {
            var serviceMock = new Mock<IChurrasService>();
            List<ChurrasEntity> listaChurras = new List<ChurrasEntity>();

            for (int i = 0; i < 5; i++)
            {
                var churras = new ChurrasEntity(i, "thomaz", DateTime.Now, "churras", 10, 10);
                listaChurras.Add(churras);
            }

            serviceMock.Setup(x => x.GetAll()).ReturnsAsync(listaChurras);

            var controller = new ChurrasController(serviceMock.Object, _serviceProvide.GetService<DataContext>());
            controller.ModelState.AddModelError("Date", "Ja existe churras marcado neste dia");

            var result = await controller.GetAll();
            Assert.True(result is BadRequestObjectResult);
        }
    }

}