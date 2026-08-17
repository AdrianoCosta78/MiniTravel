using System;

namespace MiniTravel.Domain.Entities;

public class Cliente
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string Documento { get; private set; }
    private Cliente()
    {
    }

    public Cliente(string nome, string email, string documento)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome do cliente é obrigatório.");

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("O e-mail do cliente é obrigatório.");

        if (string.IsNullOrWhiteSpace(documento))
            throw new ArgumentException("O documento do cliente é obrigatório.");

        Id = Guid.NewGuid();
        Nome = nome;
        Email = email;
        Documento = documento;
    }
}