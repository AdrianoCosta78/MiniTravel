using MiniTravel.Domain.Enums;

namespace MiniTravel.Domain.Entities;

public class Reserva
{
    public Guid Id { get; private set; }
    public Guid ClienteId { get; private set; }
    public Guid DestinoId { get; private set; }
    public DateTime DataViagem { get; private set; }
    public int QuantidadeViajantes { get; private set; }
    public decimal ValorTotal { get; private set; }
    public StatusReserva Status { get; private set; }
    public DateTime DataCriacao { get; private set; }

    private Reserva()
    {

    }

    public Reserva(
        Guid clienteId,
        Destino destino,
        DateTime dataViagem,
        int quantidadeViajantes)
    {
        if (clienteId == Guid.Empty)
            throw new ArgumentException("O cliente é obrigatório.");

        if (destino is null)
            throw new ArgumentNullException(nameof(destino));

        if (!destino.Ativo)
            throw new ArgumentException("Não é possível reservar um destino inativo.");

        if (dataViagem.Date <= DateTime.Today)
            throw new ArgumentException("A data da viagem deve ser futura.");

        if (quantidadeViajantes <= 0)
            throw new ArgumentException(
                "A quantidade de viajantes deve ser maior que zero.");

        Id = Guid.NewGuid();

        ClienteId = clienteId;
        DestinoId = destino.Id;

        DataViagem = dataViagem;
        QuantidadeViajantes = quantidadeViajantes;

        ValorTotal = destino.PrecoBase * quantidadeViajantes;

        Status = StatusReserva.Pendente;

        DataCriacao = DateTime.Now;
    }

    public void Confirmar()
    {
        if (Status == StatusReserva.Cancelada)
            throw new InvalidOperationException(
                "Não é possível confirmar uma reserva cancelada.");

        if (Status == StatusReserva.Confirmada)
            throw new InvalidOperationException(
                "A reserva já está confirmada.");

        Status = StatusReserva.Confirmada;
    }

    public void Cancelar()
    {
        if (Status == StatusReserva.Cancelada)
            throw new InvalidOperationException(
                "A reserva já está cancelada.");

        Status = StatusReserva.Cancelada;
    }
    
}


