namespace CodingCraft.Domain.Models
{
    public interface IMovimentacaoItem
    {
        
        Movimentacao Movimentacao { get; set; }

        int MovimentacaoId { get; set; }

        Produto Produto { get; set; }

        int Quantidade { get; set; }

        decimal ValorTotal { get; }

        decimal ValorUnitario { get; set; }
    }
}