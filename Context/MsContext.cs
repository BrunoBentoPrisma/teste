using Microsoft.EntityFrameworkCore;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Dtos;

namespace Ms_Compras.Context
{
    public class MsContext : DbContext
    {
        public MsContext(DbContextOptions<MsContext> options)
            : base(options)
        {
        }

        public DbSet<Compra> Compra { get; set; }
        public DbSet<CompraFornecedor> CompraFornecedor { get; set; }
        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<Grupo> Grupo { get; set; }
        public DbSet<ItensCompra> ItensCompra { get; set; }
        public DbSet<ItensCompraFornecedor> ItensCompraFornecedor { get; set; }
        public DbSet<Laboratorio> Laboratorio { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<FaltasEncomendas> FaltasEncomendas { get; set; }
        public DbSet<Vendedor> Vendedor { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<NotaFiscalDeEntrada> NotaFiscalDeEntrada { get; set; }
        public DbSet<ItensNotaFiscalDeEntrada> ItensNotaFiscalDeEntrada { get; set; }
        public DbSet<OrdemDeProducao> OrdemDeProducao { get; set; }
        public DbSet<ItensOrdemDeProducao> ItensOrdemDeProducao { get; set; }
        public DbSet<ErrorLogger> ErrorLogger { get; set; } 
        public DbSet<ArquivosCotacaoCompra> ArquivosCotacaoCompra { get; set; } 
        public DbSet<MovimentoProduto> MovimentoProduto { get; set; } 
        public DbSet<EmbalagemOrdemDeProducao> EmbalagemOrdemDeProducao { get; set; }
        public DbSet<ItensEmbalagemOrdemDeProducao> ItensEmbalagemOrdemDeProducao { get; set; }
        public DbSet<ItensVenda> ItensVenda { get; set; }
        public DbSet<Lote> Lote { get; set; }
        public DbSet<Venda> Venda { get; set; }
        public DbSet<ItensFormulaVenda> ItensFormulaVenda { get; set; }
        public DbSet<ConfigEstoqueFilial> ConfigEstoqueFilial { get; set; }
        public DbSet<Rel_ConsumoProduto> Rel_ConsumoProduto { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(GetStringConectionConfig());
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        private static string GetStringConectionConfig()
        {
            return "User ID=postgres; Password=prixpto; Host=186.250.186.157; Port=49153; Database=Ms-Compras; Pooling=true;";
        }

    }
}
