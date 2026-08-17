using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using MiniTravel.Application.Features.Reservas.Commands.CriarReserva;
using MiniTravel.Application.Interfaces.Repositories;
using MiniTravel.Domain.Entities;

namespace MiniTravel.UnitTests.Application.Reservas
{
    public class CriarReservaCommandHandlerTests
    {
        [Fact]
        public async Task HandleAsync_ComDadosValidos_DeveSalvarReserva()
        {
            // Arrange
            var clienteId = Guid.NewGuid();
            var destinoId = Guid.NewGuid();

            var destino = new Destino(
                "Paris",
                "França",
                5000m);

            var destinoRepositoryMock =
                new Mock<IDestinoRepository>();

            var reservaRepositoryMock =
                new Mock<IReservaRepository>();

            destinoRepositoryMock
                .Setup(x => x.ObterPorIdAsync(destinoId))
                .ReturnsAsync(destino);

            var handler = new CriarReservaCommandHandler(
                destinoRepositoryMock.Object,
                reservaRepositoryMock.Object);

            var command = new CriarReservaCommand
            {
                ClienteId = clienteId,
                DestinoId = destinoId,
                DataViagem = DateTime.Today.AddMonths(3),
                QuantidadeViajantes = 2
            };

            // Act
            var reserva = await handler.HandleAsync(command);

            // Assert
            Assert.NotNull(reserva);

            Assert.Equal(clienteId, reserva.ClienteId);

            Assert.Equal(10000m, reserva.ValorTotal);

            reservaRepositoryMock.Verify(
                x => x.AdicionarAsync(It.IsAny<Reserva>()),
                Times.Once);
        }

        [Fact]
        public async Task HandleAsync_DestinoNaoEncontrado_NaoDeveSalvarReserva()
        {
            // Arrange
            var clienteId = Guid.NewGuid();
            var destinoId = Guid.NewGuid();

            var destinoRepositoryMock =
                new Mock<IDestinoRepository>();

            var reservaRepositoryMock =
                new Mock<IReservaRepository>();

            destinoRepositoryMock
                .Setup(x => x.ObterPorIdAsync(destinoId))
                .ReturnsAsync((Destino?)null);

            var handler = new CriarReservaCommandHandler(
                destinoRepositoryMock.Object,
                reservaRepositoryMock.Object);

            var command = new CriarReservaCommand
            {
                ClienteId = clienteId,
                DestinoId = destinoId,
                DataViagem = DateTime.Today.AddMonths(3),
                QuantidadeViajantes = 2
            };

            // Act
            Func<Task> acao =
                () => handler.HandleAsync(command);

            // Assert
            var excecao =
                await Assert.ThrowsAsync<InvalidOperationException>(acao);

            Assert.Equal(
                "Destino não encontrado.",
                excecao.Message);

            reservaRepositoryMock.Verify(
                x => x.AdicionarAsync(It.IsAny<Reserva>()),
                Times.Never);
        }
    }
}
