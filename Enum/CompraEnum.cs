namespace Ms_Compras.Enum
{
    public enum StatusCompra
    {
        Todos,
        EmAberto,
        Parcial,
        Completo,
        Cancelado
    }
    public enum CurvaAbc
    {
        Geral,
        A,
        B,
        C
    }
    public enum TipoCompra
    {
        Vazio,
        Venda,
        Demanda,
        EstoqueMinimo,
        EstoqueMaximo,
        Consumo,
        Encomendas

    }
    public enum TipoMovimentoProduto
    {
        Entrada,
        Saida
    }
}