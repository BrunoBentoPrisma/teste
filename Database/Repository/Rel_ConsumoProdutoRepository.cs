using Microsoft.EntityFrameworkCore;
using Ms_Compras.Context;
using Ms_Compras.Database.Entidades;
using Ms_Compras.Database.Interfaces;
using Ms_Compras.Dtos;
using Ms_Compras.Dtos.Compra;
using System.Data;

namespace Ms_Compras.Database.Repository
{
    public class Rel_ConsumoProdutoRepository : GenericRepository<Rel_ConsumoProduto>, IRel_ConsumoProdutoRepository
    {
        public async Task<List<ItensCompraFiltroViewDto>> GetItensPorConsumo(FiltroCompraDto filtroCompraDto, List<string> gruposIds)
        {
            try
            {
                using (var data = new MsContext(_OptionsBuilder))
                {
                    using (var command = data.Database.GetDbConnection().CreateCommand()) 
                    {
                        bool saldoConprometido = filtroCompraDto.SaldoQuantidadeComprometida is not null ? filtroCompraDto.SaldoQuantidadeComprometida.Value : false;

                        string produtoId = filtroCompraDto.ProdutosIds is not null && filtroCompraDto.ProdutosIds.Count > 0 ? $"'{filtroCompraDto.ProdutosIds[0].ToString()}'" : "NULL";

                        var sqlFunction = """SELECT "ConsumoProduto" """ +
                            $"(0," +
                            $"'{filtroCompraDto.VendaDe.Value.ToString("yyyy-MM-dd")}'," +
                            $"'{filtroCompraDto.VendaAte.Value.ToString("yyyy-MM-dd")}', " +
                            $"'{string.Join(",", gruposIds)}', " +
                            $"{saldoConprometido}, " +
                            $"{filtroCompraDto.ConsiderarApenasEmpresaSelecionada}," +
                            $"'{filtroCompraDto.EmpresaId.Value}'," +
                            $"{produtoId})";


                        command.CommandText = sqlFunction;
                        command.CommandType = CommandType.Text;
                        await data.Database.OpenConnectionAsync();
                        await command.ExecuteReaderAsync();
                        await data.Database.CloseConnectionAsync();
                    }
                }

                        var _query = $"""
                    SELECT R."GrupoId",R."ProdutoId",PR."Descricao" AS "DescricaoProduto",NULL AS "Laboratorioid",PR."ValorCusto",
                        CASE WHEN (R."Curva" = 0) THEN 'A' ELSE 
                        CASE WHEN (R."Curva" = 1) THEN 'B' ELSE 
                        CASE WHEN (R."Curva" = 2) THEN 'C' ELSE '-' END END END AS "CurvaAbc",
                        R."Unidade" AS "SiglaUnidade",SUM(R."Estoque") AS "Estoque",NULL AS "EstoqueTotal",
                        SUM(R."Consumo") AS "QuantidadeVendida",
                        CAST(ROUND(SUM(R."Consumo") / CAST(1 AS numeric) * CAST(0 AS numeric)) AS numeric) AS "TotalParaDias",
                        CAST(ROUND(SUM(R."Consumo") / CAST(1 AS numeric) * CAST(0 AS numeric)) AS numeric) AS "Comprar",
                        SUM(CASE WHEN F."Tipo" = 1 THEN F."Quantidade" ELSE 0 END) AS "Encomenda",
                        PR."Descricao",NULL AS DescricaoLaboratorio,PR."CodigoCas",PR."CodigoDcb",PR."CodigoBarra",
                        CAST(SUM(R."Consumo") / CAST(1 AS numeric) AS numeric) AS "ConsumoDiario"
                    FROM "Rel_ConsumoProduto" R JOIN "Produto" PR ON (R."GrupoId" = PR."GrupoId" AND R."ProdutoId" = PR."Id")
                    LEFT JOIN "FaltasEncomendas" F ON (F."GrupoId" = R."GrupoId" AND F."ProdutoId" = R."ProdutoId" 
                        AND F."DataDeCadastro" >= '{filtroCompraDto.VendaDe.Value.ToString("yyyy-MM-dd HH:mm:ss")}' 
                        AND F."DataDeCadastro" <= '{filtroCompraDto.VendaAte.Value.ToString("yyyy-MM-dd HH:mm:ss")}' 
                		AND F."EmpresaId" = '{filtroCompraDto.EmpresaId.Value}' AND F."Status" = 0)
                    WHERE PR."Inativo" = FALSE AND PR."InativoCompras" = FALSE GROUP BY
                        R."GrupoId",R."ProdutoId",R."Descricao",R."Curva",R."Unidade",R."Estoque",PR."ValorCusto",
                        CAST(NULL AS double precision),
                        CASE WHEN (R."Curva" = 0) THEN 'A'ELSE 
                        CASE WHEN (R."Curva" = 1) THEN 'B'ELSE 
                        CASE WHEN (R."Curva" = 2) THEN 'C'ELSE '-' END END END,
                        CAST(NULL AS CHARACTER varying),
                        PR."Descricao",
                        PR."CodigoCas",
                        PR."CodigoDcb",
                        PR."CodigoBarra"
                """;

                if (filtroCompraDto.ConsideraEncomendaFaltas is not null && filtroCompraDto.ConsideraEncomendaFaltas.Value)
                {
                    _query += $"""
                        UNION ALL SELECT PR."GrupoId",PR."Id",PR."Descricao" AS "DescricaoProduto", PR."LaboratorioId",PR."ValorCusto",
                        	CASE WHEN(PR."CurvaAbc" = 0) THEN 'A' ELSE 
                            CASE WHEN(PR."CurvaAbc" = 1) THEN 'B' ELSE 
                            CASE WHEN(PR."CurvaAbc" = 2) THEN 'C' ELSE 
                            '-' END END END AS "CurvaAbc",PR."UnidadeEstoque" AS "SiglaUnidade",
                        	CAST(NULL AS double precision) AS "EstoqueTotal",
                        	CAST(0 AS double precision) AS "QuantidadeVendida",0 "TotalParaDias",0 "Compras",
                        	SUM((CASE F."Tipo" WHEN 0 THEN 0 WHEN 1 THEN F."Quantidade" END)) AS "Encomenda",
                        	SUM((CASE F."Tipo" WHEN 0 THEN 0 WHEN 1 THEN F."Quantidade" END)),
                        	PR."Descricao", L."Descricao" AS "DescricaoLaboratorio",PR."CodigoCas",PR."CodigoDcb",PR."CodigoBarra",0 "ConsumoDiario"
                        FROM "Produto" AS PR LEFT JOIN "Laboratorio" L ON (PR."LaboratorioId" = L."Id")
                        JOIN "FaltasEncomendas" F ON (PR."GrupoId" = F."GrupoId" AND PR."Id" = F."ProdutoId" AND F."Status" = 0 AND NOT EXISTS (SELECT "GrupoId","ProdutoId"
                        FROM "Rel_ConsumoProduto" WHERE "GrupoId" = F."GrupoId" AND "ProdutoId" = F."ProdutoId")) WHERE 1 = 1
                        	AND PR."Inativo" = FALSE AND PR."InativoCompras" = FALSE
                        	AND F."DataDeCadastro" >= '{filtroCompraDto.VendaDe.Value.ToString("yyyy-MM-dd HH:mm:ss")}' 
                        	AND F."DataDeCadastro" <= '{filtroCompraDto.VendaAte.Value.ToString("yyyy-MM-dd HH:mm:ss")}'
                        GROUP BY 1,2,3,4,5,6,7,8,9,10,16,17,18,19,PR."CodigoBarra",PR."GrupoId",PR."Id",L."Descricao"
                        ORDER BY 1,2
                        """;
                }

                var itensCompraViewDto = new List<ItensCompraFiltroViewDto>();

                using (var data = new MsContext(_OptionsBuilder))
                {
                    using (var command = data.Database.GetDbConnection().CreateCommand())
                    {
                        command.CommandText = _query;
                        command.CommandType = CommandType.Text;
                        await data.Database.OpenConnectionAsync();

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            while (await result.ReadAsync())
                            {
                                var itemCompraViewDto = new ItensCompraFiltroViewDto();

                                itemCompraViewDto.GrupoId = result.GetGuid(0);
                                itemCompraViewDto.ProdutoId = result.GetGuid(1);
                                itemCompraViewDto.DescricaoProduto = result.GetString(2);
                                itemCompraViewDto.LaboratorioId = !string.IsNullOrEmpty(result["LaboratorioId"].ToString()) ? result.GetGuid(3) : null;
                                itemCompraViewDto.ValorUnitario = result.GetDecimal(4);
                                itemCompraViewDto.CurvaAbc = result.GetString(5);
                                itemCompraViewDto.Estoque = result.GetDecimal(7);
                                itemCompraViewDto.QuantidadeVendida = result.GetDecimal(9);
                                itemCompraViewDto.TotalParaDias = result.GetDecimal(10);
                                itemCompraViewDto.Quantidade = result.GetDecimal(11);
                                itemCompraViewDto.Encomenda = result.GetInt32(12);
                                itemCompraViewDto.DescricaoLaboratorio = !string.IsNullOrEmpty(result["descricaolaboratorio"].ToString()) ? result.GetString(14) : null;
                                itemCompraViewDto.CodigoCas = result.GetString(15);
                                itemCompraViewDto.CodigoDcb = result.GetString(16);
                                itemCompraViewDto.CodigoBarras = result.GetString(17);
                                itemCompraViewDto.ConsumoDiario = result.GetDecimal(18);

                                itensCompraViewDto.Add(itemCompraViewDto);
                            }
                        }
                    }

                    await data.Database.CloseConnectionAsync();

                }

                return itensCompraViewDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
