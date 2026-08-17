using MiniTravel.Domain.Entities;
using MiniTravel.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;


namespace MiniTravel.UnitTests.Domain;

public class ReservaTests
{
    [Fact]
    public void CriarReserva_ComZeroViajantes_DeveGerarExcecao()
    {
        // Arrange
        var clienteId = Guid.NewGuid();

        var destino = new Destino(
            "Paris",
            "França",
            5000m);

        var dataViagem = DateTime.Today.AddMonths(3);

        // Act
        Action acao = () => new Reserva(
            clienteId,
            destino,
            dataViagem,
            0);

        // Assert
        var excecao = Assert.Throws<ArgumentException>(acao);

        Assert.Equal(
            "A quantidade de viajantes deve ser maior que zero.",
            excecao.Message);
    }
    [Fact]
    public void CriarReserva_ComDadosValidos_DeveNascerComoPendente()
    {
        // Arrange
        var clienteId = Guid.NewGuid();

        var destino = new Destino(
            "Paris",
            "França",
            5000m);

        var dataViagem = DateTime.Today.AddMonths(3);

        // Act
        var reserva = new Reserva(
            clienteId,
            destino,
            dataViagem,
            2);

        // Assert
        Assert.Equal(
            StatusReserva.Pendente,
            reserva.Status);
    }

    [Fact]
    public void CriarReserva_ComTresViajantes_DeveCalcularValorTotalCorretamente()
    {
        // Arrange
        var clienteId = Guid.NewGuid();

        var destino = new Destino(
            "Paris",
            "França",
            5000m);

        var dataViagem = DateTime.Today.AddMonths(3);

        // Act
        var reserva = new Reserva(
            clienteId,
            destino,
            dataViagem,
            3);

        // Assert
        Assert.Equal(
            15000m,
            reserva.ValorTotal);
    }

    [Fact]
    public void Confirmar_ReservaPendente_DeveAlterarStatusParaConfirmada()
    {
        // Arrange
        var clienteId = Guid.NewGuid();

        var destino = new Destino(
            "Paris",
            "França",
            5000m);

        var reserva = new Reserva(
            clienteId,
            destino,
            DateTime.Today.AddMonths(3),
            2);

        // Act
        reserva.Confirmar();

        // Assert
        Assert.Equal(
            StatusReserva.Confirmada,
            reserva.Status);
    }

    [Fact]
    public void Confirmar_ReservaCancelada_DeveGerarExcecao()
    {
        // Arrange
        var clienteId = Guid.NewGuid();

        var destino = new Destino(
            "Paris",
            "França",
            5000m);

        var reserva = new Reserva(
            clienteId,
            destino,
            DateTime.Today.AddMonths(3),
            2);

        reserva.Cancelar();

        // Act
        Action acao = () => reserva.Confirmar();

        // Assert
        Assert.Throws<InvalidOperationException>(acao);
    }

    [Fact]
    public void CriarReserva_ComDestinoInativo_DeveGerarExcecao()
    {
        // Arrange
        var clienteId = Guid.NewGuid();

        var destino = new Destino(
            "Paris",
            "França",
            5000m);

        destino.Desativar();

        var dataViagem = DateTime.Today.AddMonths(3);

        // Act
        Action acao = () => new Reserva(
            clienteId,
            destino,
            dataViagem,
            2);

        // Assert
        Assert.Throws<ArgumentException>(acao);
    }
}