using ChurrasTrica.Domain.Entities;
using ChurrasTrinca.Domain.Interfaces.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurrasTrinca.Services.Tests.Churras
{
    [TestFixture]
    public class ChurrasServiceTests
    {
        private IChurrasService _churrasService;
        [Test]
        public async Task E_Possivel_Adicionar_Churras()
        {
            var serviceMock = new Mock<IChurrasService>();

            var churras = new ChurrasEntity(1, "thomaz", DateTime.Now, "churras", 10, 10);

            serviceMock.Setup(x => x.Insert(It.IsAny<ChurrasEntity>())).ReturnsAsync(churras);
            _churrasService = serviceMock.Object;

            var result = await _churrasService.Insert(churras);
            Assert.NotNull(result);
        }

        [Test]
        public async Task E_Possivel_Deletar_Churras()
        {
            var serviceMock = new Mock<IChurrasService>();

            var churras = new ChurrasEntity(1, "thomaz", DateTime.Now, "churras", 10, 10);

            serviceMock.Setup(x => x.Delete(churras)).ReturnsAsync(true);
            _churrasService = serviceMock.Object;

            var deleted = await _churrasService.Delete(churras);
            Assert.True(deleted);
        }

        [Test]
        public async Task E_Possivel_Visualizar_Todos_Churras()
        {
            var serviceMock = new Mock<IChurrasService>();
            List<ChurrasEntity> listaChurras = new List<ChurrasEntity>();
            
            for (int i = 0; i < 5; i++)
            {
                var churras = new ChurrasEntity(i, "thomaz", DateTime.Now, "churras", 10, 10);
                listaChurras.Add(churras);
            }

            serviceMock.Setup(x => x.GetAll()).ReturnsAsync(listaChurras);
            _churrasService = serviceMock.Object;

            var result = await _churrasService.GetAll();
            Assert.NotNull(result);
            Assert.True(result.Count() == 5);
        }
    }
}
