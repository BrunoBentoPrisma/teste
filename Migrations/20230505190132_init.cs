using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ms_Compras.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArquivosCotacaoCompra",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CoteFacil = table.Column<byte[]>(type: "bytea", nullable: true),
                    EmbraFarma = table.Column<byte[]>(type: "bytea", nullable: true),
                    CompraId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IntegracaoId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataDeExclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DataDeAlteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NomeCriador = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NomeEditor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArquivosCotacaoCompra", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IntegracaoId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataDeExclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DataDeAlteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NomeCriador = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NomeEditor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfigEstoqueFilial",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    EstoqueMinimo = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    EstoqueMaximo = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false),
                    ControlaEstoqueMinimoMaximoPorFilial = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigEstoqueFilial", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ErrorLogger",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    ErrorMessage = table.Column<string>(type: "text", nullable: false),
                    CustomErrorMessage = table.Column<string>(type: "text", nullable: false),
                    CustomErrorCode = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLogger", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormulaVenda",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IntegracaoId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataDeExclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DataDeAlteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NomeCriador = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NomeEditor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormulaVenda", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NomeFornecedor = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IntegracaoId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataDeExclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DataDeAlteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NomeCriador = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NomeEditor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grupo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Descricao = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IntegracaoId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataDeExclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DataDeAlteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NomeCriador = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NomeEditor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Laboratorio",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Descricao = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IntegracaoId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataDeExclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DataDeAlteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NomeCriador = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NomeEditor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laboratorio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Venda",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DataDeEmissao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Orcamento = table.Column<int>(type: "integer", nullable: false),
                    Total = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IntegracaoId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataDeExclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DataDeAlteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NomeCriador = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NomeEditor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venda", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendedor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IntegracaoId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataDeExclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DataDeAlteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NomeCriador = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NomeEditor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendedor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompraFornecedor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompraId = table.Column<Guid>(type: "uuid", nullable: false),
                    Observacao = table.Column<string>(type: "character varying(30000)", maxLength: 30000, nullable: true),
                    StatusCotacao = table.Column<int>(type: "integer", nullable: false),
                    FormaPagamento = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Frete = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DataPreveEntrega = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    StatusPedido = table.Column<int>(type: "integer", nullable: false),
                    NumeroNota = table.Column<int>(type: "integer", nullable: false),
                    FornecedorId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IntegracaoId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataDeExclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DataDeAlteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NomeCriador = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NomeEditor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraFornecedor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompraFornecedor_Fornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotaFiscalDeEntrada",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FornecedorId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataDeEmissao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    NumeroNota = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    SerieNota = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Total = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    Frete = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IntegracaoId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataDeExclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DataDeAlteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NomeCriador = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NomeEditor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotaFiscalDeEntrada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotaFiscalDeEntrada_Fornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StatusCompra = table.Column<int>(type: "integer", nullable: false),
                    TotalCompra = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    TempoDeReposicaoMaxima = table.Column<int>(type: "integer", nullable: true),
                    VendaDe = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    VendaAte = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CurvaAbc = table.Column<int>(type: "integer", nullable: false),
                    LaboratorioId = table.Column<Guid>(type: "uuid", nullable: true),
                    TipoCompra = table.Column<int>(type: "integer", nullable: false),
                    TipoDemanda = table.Column<int>(type: "integer", nullable: true),
                    ConsideraEncomendaFaltas = table.Column<bool>(type: "boolean", nullable: true),
                    TempoDeReposicao = table.Column<int>(type: "integer", nullable: true),
                    QuantidadeDias = table.Column<int>(type: "integer", nullable: true),
                    TipoValor = table.Column<int>(type: "integer", nullable: true),
                    APartirDe = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SaldoQuantidadeComprometida = table.Column<bool>(type: "boolean", nullable: true),
                    ConsiderarApenasEmpresaSelecionada = table.Column<bool>(type: "boolean", nullable: true),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IntegracaoId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataDeExclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DataDeAlteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NomeCriador = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NomeEditor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compra_Laboratorio_LaboratorioId",
                        column: x => x.LaboratorioId,
                        principalTable: "Laboratorio",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GrupoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Descricao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    UnidadeManipulacao = table.Column<string>(type: "text", nullable: false),
                    UnidadeEstoque = table.Column<string>(type: "text", nullable: false),
                    ValorCusto = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    ValorCustoMedio = table.Column<decimal>(type: "numeric(24,4)", nullable: true),
                    ValorVenda = table.Column<decimal>(type: "numeric(24,4)", nullable: true),
                    EstoqueMinimo = table.Column<decimal>(type: "numeric(24,4)", nullable: true),
                    EstoqueMaximo = table.Column<decimal>(type: "numeric(24,4)", nullable: true),
                    FornecedorId = table.Column<Guid>(type: "uuid", nullable: true),
                    DataUltimaCompra = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CurvaAbc = table.Column<int>(type: "integer", nullable: true),
                    AliquotaIcms = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    Calculo = table.Column<int>(type: "integer", nullable: false),
                    SituacaoTributaria = table.Column<int>(type: "integer", nullable: false),
                    CodigoBarra = table.Column<string>(type: "text", nullable: true),
                    CodigoDcb = table.Column<string>(type: "text", nullable: true),
                    CodigoCas = table.Column<string>(type: "text", nullable: true),
                    Inativo = table.Column<bool>(type: "boolean", nullable: false),
                    InativoCompras = table.Column<bool>(type: "boolean", nullable: false),
                    LaboratorioId = table.Column<Guid>(type: "uuid", nullable: true),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IntegracaoId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataDeExclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DataDeAlteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NomeCriador = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NomeEditor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produto_Fornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Produto_Grupo_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Produto_Laboratorio_LaboratorioId",
                        column: x => x.LaboratorioId,
                        principalTable: "Laboratorio",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrdemDeProducao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    AnoOrdemDeProducao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataDeEmissao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CodigoForma = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    NumeroOrdemDeProducao = table.Column<string>(type: "text", nullable: false),
                    ProdutoCapsulaId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoEmbalagemId = table.Column<Guid>(type: "uuid", nullable: false),
                    GrupoCapsulaId = table.Column<Guid>(type: "uuid", nullable: false),
                    GrupoEmbalagemId = table.Column<Guid>(type: "uuid", nullable: false),
                    CapsulaOrdemDeProducao = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    VendaId = table.Column<Guid>(type: "uuid", nullable: true),
                    FormulaVendaId = table.Column<Guid>(type: "uuid", nullable: true),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IntegracaoId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataDeExclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DataDeAlteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NomeCriador = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NomeEditor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdemDeProducao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdemDeProducao_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdemDeProducao_FormulaVenda_FormulaVendaId",
                        column: x => x.FormulaVendaId,
                        principalTable: "FormulaVenda",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrdemDeProducao_Venda_VendaId",
                        column: x => x.VendaId,
                        principalTable: "Venda",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmbalagemOrdemDeProducao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrdemDeProducaoId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    GrupoId = table.Column<Guid>(type: "uuid", nullable: false),
                    AnoOrdemProducao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    QuatidadeComprometidaLote = table.Column<decimal>(type: "numeric(24,5)", nullable: false),
                    QuatidadeUnidadeEstoque = table.Column<decimal>(type: "numeric(24,5)", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IntegracaoId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataDeExclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DataDeAlteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NomeCriador = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NomeEditor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmbalagemOrdemDeProducao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmbalagemOrdemDeProducao_Grupo_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmbalagemOrdemDeProducao_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FaltasEncomendas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uuid", nullable: true),
                    GrupoId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    VendedorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompraId = table.Column<Guid>(type: "uuid", nullable: true),
                    Observacao = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    PrevisaoDeEntrega = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Quantidade = table.Column<decimal>(type: "numeric(12,4)", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: true),
                    Telefone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    Ddd = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IntegracaoId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataDeExclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DataDeAlteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NomeCriador = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NomeEditor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaltasEncomendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FaltasEncomendas_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FaltasEncomendas_Grupo_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FaltasEncomendas_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FaltasEncomendas_Vendedor_VendedorId",
                        column: x => x.VendedorId,
                        principalTable: "Vendedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItensCompra",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LaboratorioId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Comprar = table.Column<bool>(type: "boolean", nullable: false),
                    Encomenda = table.Column<bool>(type: "boolean", nullable: false),
                    Estoque = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    CompraId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuantidadeCompra = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    QuantidadeVendida = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    QuantidadeTotal = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    ValorVendido = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    SelecionadoGerar = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    ConsumoDiario = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    QuantidadeCompraMaxima = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    Ddd = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IntegracaoId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataDeExclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DataDeAlteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NomeCriador = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NomeEditor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensCompra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensCompra_Compra_CompraId",
                        column: x => x.CompraId,
                        principalTable: "Compra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItensCompra_Laboratorio_LaboratorioId",
                        column: x => x.LaboratorioId,
                        principalTable: "Laboratorio",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItensCompra_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItensCompraFornecedor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Comprar = table.Column<bool>(type: "boolean", nullable: false),
                    DataValidade = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CompraFornecedorId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuantidadeCompra = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    QuatidadeCompraUnidadeEstoque = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    ValorUnitarioUnidadeEstoque = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    StatusItemPedido = table.Column<int>(type: "integer", nullable: false),
                    SelecionadoGerar = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IntegracaoId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataDeExclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DataDeAlteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NomeCriador = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NomeEditor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensCompraFornecedor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensCompraFornecedor_CompraFornecedor_CompraFornecedorId",
                        column: x => x.CompraFornecedorId,
                        principalTable: "CompraFornecedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItensCompraFornecedor_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItensFormulaVenda",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    FormulaVendaId = table.Column<Guid>(type: "uuid", nullable: false),
                    VendaId = table.Column<Guid>(type: "uuid", nullable: true),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IntegracaoId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataDeExclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DataDeAlteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NomeCriador = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NomeEditor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensFormulaVenda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensFormulaVenda_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItensNotaFiscalDeEntrada",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    NotaFiscalDeEntradaId = table.Column<Guid>(type: "uuid", nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    Total = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    Frete = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    Quantidade = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    ValorIpi = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    Validade = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IntegracaoId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataDeExclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DataDeAlteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NomeCriador = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NomeEditor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensNotaFiscalDeEntrada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensNotaFiscalDeEntrada_NotaFiscalDeEntrada_NotaFiscalDeEn~",
                        column: x => x.NotaFiscalDeEntradaId,
                        principalTable: "NotaFiscalDeEntrada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItensNotaFiscalDeEntrada_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItensVenda",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantidade = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    VendaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Sequencia = table.Column<int>(type: "integer", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IntegracaoId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataDeExclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DataDeAlteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NomeCriador = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NomeEditor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensVenda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensVenda_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItensVenda_Venda_VendaId",
                        column: x => x.VendaId,
                        principalTable: "Venda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lote",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FornecedorId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataDeEntradaDoLote = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataDeFabricacaoDoLote = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataDeValidadeDoLote = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    NumeroNota = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    SerieNota = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    QuantidadeComprometidaLote = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IntegracaoId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataDeExclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DataDeAlteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NomeCriador = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NomeEditor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lote_Fornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lote_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovimentoProduto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuantidadeMovimentada = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    Saldo = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    TipoMovimento = table.Column<int>(type: "integer", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IntegracaoId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataDeExclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DataDeAlteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NomeCriador = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NomeEditor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentoProduto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimentoProduto_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItensOrdemDeProducao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrdemDeProducaoId = table.Column<Guid>(type: "uuid", nullable: false),
                    SequenciaItem = table.Column<int>(type: "integer", nullable: false),
                    QuantidadeUnidadeEstoque = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    QuantidadeComprometidaLote = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IntegracaoId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataDeExclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DataDeAlteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NomeCriador = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NomeEditor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensOrdemDeProducao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensOrdemDeProducao_OrdemDeProducao_OrdemDeProducaoId",
                        column: x => x.OrdemDeProducaoId,
                        principalTable: "OrdemDeProducao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItensOrdemDeProducao_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItensEmbalagemOrdemDeProducao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmbalagemOrdemDeProducaoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Sequencia = table.Column<int>(type: "integer", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    FatorDeCorrecao = table.Column<decimal>(type: "numeric(14,4)", nullable: false),
                    LoteInterno = table.Column<int>(type: "integer", nullable: false),
                    QuantidadeComprometidaLote = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    QuantidadePesada = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    QuantidadeSemFator = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    QuantidadeUnidadeEstoque = table.Column<decimal>(type: "numeric(24,4)", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IntegracaoId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DataDeCadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataDeExclusao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DataDeAlteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NomeCriador = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NomeEditor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Excluido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensEmbalagemOrdemDeProducao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensEmbalagemOrdemDeProducao_EmbalagemOrdemDeProducao_Emba~",
                        column: x => x.EmbalagemOrdemDeProducaoId,
                        principalTable: "EmbalagemOrdemDeProducao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItensEmbalagemOrdemDeProducao_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compra_LaboratorioId",
                table: "Compra",
                column: "LaboratorioId");

            migrationBuilder.CreateIndex(
                name: "IX_CompraFornecedor_FornecedorId",
                table: "CompraFornecedor",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_EmbalagemOrdemDeProducao_GrupoId",
                table: "EmbalagemOrdemDeProducao",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_EmbalagemOrdemDeProducao_ProdutoId",
                table: "EmbalagemOrdemDeProducao",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_FaltasEncomendas_ClienteId",
                table: "FaltasEncomendas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_FaltasEncomendas_GrupoId",
                table: "FaltasEncomendas",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_FaltasEncomendas_ProdutoId",
                table: "FaltasEncomendas",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_FaltasEncomendas_VendedorId",
                table: "FaltasEncomendas",
                column: "VendedorId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensCompra_CompraId",
                table: "ItensCompra",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensCompra_LaboratorioId",
                table: "ItensCompra",
                column: "LaboratorioId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensCompra_ProdutoId",
                table: "ItensCompra",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensCompraFornecedor_CompraFornecedorId",
                table: "ItensCompraFornecedor",
                column: "CompraFornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensCompraFornecedor_ProdutoId",
                table: "ItensCompraFornecedor",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensEmbalagemOrdemDeProducao_EmbalagemOrdemDeProducaoId",
                table: "ItensEmbalagemOrdemDeProducao",
                column: "EmbalagemOrdemDeProducaoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensEmbalagemOrdemDeProducao_ProdutoId",
                table: "ItensEmbalagemOrdemDeProducao",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensFormulaVenda_ProdutoId",
                table: "ItensFormulaVenda",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensNotaFiscalDeEntrada_NotaFiscalDeEntradaId",
                table: "ItensNotaFiscalDeEntrada",
                column: "NotaFiscalDeEntradaId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensNotaFiscalDeEntrada_ProdutoId",
                table: "ItensNotaFiscalDeEntrada",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensOrdemDeProducao_OrdemDeProducaoId",
                table: "ItensOrdemDeProducao",
                column: "OrdemDeProducaoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensOrdemDeProducao_ProdutoId",
                table: "ItensOrdemDeProducao",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensVenda_ProdutoId",
                table: "ItensVenda",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensVenda_VendaId",
                table: "ItensVenda",
                column: "VendaId");

            migrationBuilder.CreateIndex(
                name: "IX_Lote_FornecedorId",
                table: "Lote",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lote_ProdutoId",
                table: "Lote",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentoProduto_ProdutoId",
                table: "MovimentoProduto",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_NotaFiscalDeEntrada_FornecedorId",
                table: "NotaFiscalDeEntrada",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdemDeProducao_ClienteId",
                table: "OrdemDeProducao",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdemDeProducao_FormulaVendaId",
                table: "OrdemDeProducao",
                column: "FormulaVendaId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdemDeProducao_VendaId",
                table: "OrdemDeProducao",
                column: "VendaId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_FornecedorId",
                table: "Produto",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_GrupoId",
                table: "Produto",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_LaboratorioId",
                table: "Produto",
                column: "LaboratorioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArquivosCotacaoCompra");

            migrationBuilder.DropTable(
                name: "ConfigEstoqueFilial");

            migrationBuilder.DropTable(
                name: "ErrorLogger");

            migrationBuilder.DropTable(
                name: "FaltasEncomendas");

            migrationBuilder.DropTable(
                name: "ItensCompra");

            migrationBuilder.DropTable(
                name: "ItensCompraFornecedor");

            migrationBuilder.DropTable(
                name: "ItensEmbalagemOrdemDeProducao");

            migrationBuilder.DropTable(
                name: "ItensFormulaVenda");

            migrationBuilder.DropTable(
                name: "ItensNotaFiscalDeEntrada");

            migrationBuilder.DropTable(
                name: "ItensOrdemDeProducao");

            migrationBuilder.DropTable(
                name: "ItensVenda");

            migrationBuilder.DropTable(
                name: "Lote");

            migrationBuilder.DropTable(
                name: "MovimentoProduto");

            migrationBuilder.DropTable(
                name: "Vendedor");

            migrationBuilder.DropTable(
                name: "Compra");

            migrationBuilder.DropTable(
                name: "CompraFornecedor");

            migrationBuilder.DropTable(
                name: "EmbalagemOrdemDeProducao");

            migrationBuilder.DropTable(
                name: "NotaFiscalDeEntrada");

            migrationBuilder.DropTable(
                name: "OrdemDeProducao");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "FormulaVenda");

            migrationBuilder.DropTable(
                name: "Venda");

            migrationBuilder.DropTable(
                name: "Fornecedor");

            migrationBuilder.DropTable(
                name: "Grupo");

            migrationBuilder.DropTable(
                name: "Laboratorio");
        }
    }
}
