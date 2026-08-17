using System;

namespace MiniTravel.Domain.Entities;

public class Destino 
{
    public Guid Id { get; private set; }
    public string Cidade { get; private set; }
    public string Pais {  get; private set; }
    public decimal PrecoBase { get; private set; }
    public bool Ativo {  get; private set; }

    private Destino()
    {
    }
    public Destino(string cidade, string pais, decimal precoBase)
    {
        if (string.IsNullOrWhiteSpace(cidade))
            throw new ArgumentException("A cidade é obrigatória.");

        if (string.IsNullOrWhiteSpace(pais))
            throw new ArgumentException("O país é obrigatório.");

        if (precoBase <= 0)
            throw new ArgumentException("O preço base deve ser maior que zero.");

        Id = Guid.NewGuid();
        Cidade = cidade;
        Pais = pais;
        PrecoBase = precoBase;
        Ativo = true;
    }
    
    public void Desativar()
    {
        Ativo = false;
    }

    public void Ativar()
    {
        Ativo = true;
    }
}
