using Microsoft.EntityFrameworkCore;
using Ms_Compras.Context;
using Ms_Compras.Database.Interfaces;
using Ms_Compras.Dtos;
using System.Data;

namespace Ms_Compras.Database.Repository
{
    public class FiltroComprasPorDemandaRepository : GenericRepository<ItensCompraFiltroViewDto>, IFiltroComprasPorDemandaRepository
    {

        public async Task<List<ItensCompraViewPorDemanda>> GetListaFiltroDemanda(Guid empresaId, DateTime dataInicial, DateTime dataFinal)
        {

            try
            {

                var _query = $"""
                    select 
                    p."GrupoId", 
                    p."Id", 
                    p."Descricao", 
                    p."LaboratorioId", 
                	  l."Descricao"  as NomeLaboratorio, 
                    p."UnidadeEstoque" as SiglaUnidade, 
                    case when(p."CurvaAbc" = 0) then 'A' else 
                    case when(p."CurvaAbc" = 1) then 'B' else 
                    case when(p."CurvaAbc" = 2) then 'C' else  '-' end end 
                    end AS CurvaAbc, 
                    p."ValorCusto", 
                    p."ValorCustoMedio", 
                    p."CodigoCas", 
                    p."CodigoDcb", 
                    p."CodigoBarra" 
                	,(COALESCE((select sum(iv."Quantidade") from "ItensVenda" iv inner join "Venda" v on (
                		v."Id" = iv."VendaId" 
                		and iv."ProdutoId" = p."Id" 
                		and v."Status" = 1 
                		and v."Orcamento" = 0 
                        and v."EmpresaId" = '{empresaId}'
                		and v."DataDeEmissao" >= '{dataInicial.ToString("yyyy-MM-dd HH:mm:ss")}' 
                		and v."DataDeEmissao" <= '{dataFinal.ToString("yyyy-MM-dd HH:mm:ss")}')),0)  
                		+ 
                		COALESCE((select sum(iop."QuantidadeUnidadeEstoque") from "ItensOrdemDeProducao" iop inner join "OrdemDeProducao" op on ( 
                         iop."OrdemDeProducaoId" = op."Id" 
                		 and iop."ProdutoId" = p."Id" 
                		 and extract(year from op."AnoOrdemDeProducao") = '{dataInicial.Year}' 
                		 and op."Status" = 2 
                         and op."EmpresaId" = '{empresaId}'
                		 and op."DataDeEmissao" >= '{dataInicial.ToString("yyyy-MM-dd HH:mm:ss")}' 
                		 and op."DataDeEmissao" <= '{dataFinal.ToString("yyyy-MM-dd HH:mm:ss")}') 
                		inner join "ItensFormulaVenda" ifv on (ifv."FormulaVendaId" = op."FormulaVendaId" and ifv."VendaId" = op."VendaId" and ifv."ProdutoId" = p."Id") ), 0)) 
                		+ 
                		coalesce((select sum(iop."QuantidadeUnidadeEstoque") from "ItensOrdemDeProducao" iop inner join "OrdemDeProducao" op on ( 
                		iop."OrdemDeProducaoId" = op."Id" 
                		and iop."ProdutoId" = p."Id" 
                		and op."Status" = 2 
                        and op."EmpresaId" = '{empresaId}'
                		and op."DataDeEmissao" >= '{dataInicial.ToString("yyyy-MM-dd HH:mm:ss")}' 
                		and op."DataDeEmissao" <= '{dataFinal.ToString("yyyy-MM-dd HH:mm:ss")}' 
                		and op."VendaId" is NULL) 
                		inner join "MovimentoProduto" mp on (mp."ItensOrdemDeProducaoId" = iop."Id" and mp."ProdutoId" = p."Id" )),0)
                		as QuantidadeVendida 
                		from "Produto" p 
                		left join "Laboratorio" as l on (p."LaboratorioId" = l."Id")  
                		where 1 = 1 
                		and (COALESCE((select sum(iv."Quantidade") from 
                		"ItensVenda" iv 
                		inner join "Venda" v on ( 
                        v."Id" = iv."VendaId" and
                        iv."ProdutoId" = p."Id" and
                        v."Status" = 1 and 
                        v."EmpresaId" = '{empresaId}' and
                        v."Orcamento" = 0 and 
                        v."DataDeEmissao" >= '{dataInicial.ToString("yyyy-MM-dd HH:mm:ss")}' and 
                        v."DataDeEmissao" <= '{dataFinal.ToString("yyyy-MM-dd HH:mm:ss")}' )),0)  
                		+ 
                		COALESCE((select sum(iop."QuantidadeUnidadeEstoque") from "ItensOrdemDeProducao" iop inner join "OrdemDeProducao" op on ( 
                         iop."OrdemDeProducaoId" = op."Id"   
                		 and iop."ProdutoId" = p."Id" 
                         and op."EmpresaId" = '{empresaId}'
                		 and extract(year from op."AnoOrdemDeProducao") = '{dataInicial.Year}'
                		 and op."Status" = 2 and 
                         op."DataDeEmissao" >= '{dataInicial.ToString("yyyy-MM-dd HH:mm:ss")}' and 
                         op."DataDeEmissao" <= '{dataFinal.ToString("yyyy-MM-dd HH:mm:ss")}') 
                		 inner join "ItensFormulaVenda" ifv on (ifv."FormulaVendaId" = op."FormulaVendaId" and ifv."VendaId" = op."VendaId" and ifv."ProdutoId" = p."Id" )), 0) 
                		+ 
                		coalesce((select sum(iop."QuantidadeUnidadeEstoque") from "ItensOrdemDeProducao" iop inner join "OrdemDeProducao" op on ( 
                		iop."OrdemDeProducaoId" = op."Id" 
                		and iop."ProdutoId" = p."Id" 
                		and op."Status" = 2 
                        and op."EmpresaId" = '{empresaId}'
                		and op."DataDeEmissao" >= '{dataInicial.ToString("yyyy-MM-dd HH:mm:ss")}' 
                		and op."DataDeEmissao" <= '{dataFinal.ToString("yyyy-MM-dd HH:mm:ss")}' 
                		and op."VendaId" is NULL) 
                		inner join "MovimentoProduto" mp on (mp."ItensOrdemDeProducaoId" = iop."Id" and mp."ProdutoId" = p."Id" )),0)
                			) > 0
                 and p."Inativo" = FALSE 
                 and p."InativoCompras" = FALSE 
                group by 
                p."Id", 
                p."Descricao", 
                p."LaboratorioId", 
                p."UnidadeEstoque", 
                p."CurvaAbc", 
                l."Descricao", 
                p."EstoqueMinimo",
                p."ValorCusto", 
                p."ValorCustoMedio",  
                p."CodigoCas",    
                p."CodigoDcb",  
                p."CodigoBarra"  
                order by p."Descricao"
                """;

                var itensCompraViewDto = new List<ItensCompraViewPorDemanda>();

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
                                var itemCompraViewDto = new ItensCompraViewPorDemanda();

                                itemCompraViewDto.GrupoId = result.GetGuid(0);
                                itemCompraViewDto.Id = result.GetGuid(1);
                                itemCompraViewDto.Descricao = result.GetString(2);
                                itemCompraViewDto.LaboratorioId = !string.IsNullOrEmpty(result["LaboratorioId"].ToString()) ? result.GetGuid(3) : null;
                                itemCompraViewDto.NomeLaboratorio = result["nomelaboratorio"].ToString();
                                itemCompraViewDto.SiglaUnidade = result.GetString(5);
                                itemCompraViewDto.CurvaAbc = result.GetString(6);
                                itemCompraViewDto.ValorCusto = result.GetDecimal(7);
                                itemCompraViewDto.ValorCustoMedio = result.GetDecimal(8);
                                itemCompraViewDto.CodigoCas = result.GetString(9);
                                itemCompraViewDto.CodigoDcb = result.GetString(10);
                                itemCompraViewDto.CodigoBarra = result.GetString(11);
                                itemCompraViewDto.QuantidadeVendida = result.GetDecimal(12);

                                itensCompraViewDto.Add(itemCompraViewDto);
                            }
                        }
                    }

                    await data.Database.CloseConnectionAsync();
                    
                    return itensCompraViewDto;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
    }
}
