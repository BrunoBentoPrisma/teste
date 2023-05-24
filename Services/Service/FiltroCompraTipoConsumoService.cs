using Ms_Compras.Database.Entidades;
using Ms_Compras.Database.Interfaces;
using Ms_Compras.Dtos;
using Ms_Compras.Dtos.Compra;
using Ms_Compras.Services.Interfaces;

namespace Ms_Compras.Services.Service
{
    public class FiltroCompraTipoConsumoService : IFiltroCompraTipoConsumoService
    {
        private readonly IGrupoRepository _grupoRepository;
        private readonly IRel_ConsumoProdutoRepository _rel_ConsumoProdutoRepository;

        public FiltroCompraTipoConsumoService( 
            IGrupoRepository grupoRepository, 
            IRel_ConsumoProdutoRepository rel_ConsumoProdutoRepository)
        {
            _grupoRepository = grupoRepository;
            _rel_ConsumoProdutoRepository = rel_ConsumoProdutoRepository;
        }

        public async Task<List<ItensCompraFiltroViewDto>> GetFiltroItensCompra(FiltroCompraDto filtroCompraDto)
        {
            try
            {
                if(filtroCompraDto.GruposIds is null)
                {
                    throw new Exception("Parametros inválidos para a consulta de consumo !");
                }

                var gruposIds = new List<string>();

                filtroCompraDto.GruposIds.ForEach(grupoId => gruposIds.Add(grupoId.ToString()));
                //var grupos = new List<Grupo>();

                //foreach(var grupoId in filtroCompraDto.GruposIds)
                //{
                //    var grupo = await _grupoRepository.GetGrupoById(grupoId);
                //    grupos.Add(grupo);
                //}


                //var gruposTipoUm = grupos.Where(x => x.Tipo == 0 || x.Tipo == 1 || x.Tipo == 6 || x.Tipo == 7).Select(x => x.Id).ToList();

                //filtroCompraDto.GruposIds = gruposTipoUm;

                //var itensConsumo = await _verificaTipoGrupoFiltroConsumoSevice.GetFiltroItensCompraGrupoUm(filtroCompraDto);

                //if (!itensConsumo) throw new Exception("Ocorreu um erro ao filtrar os produtos da compra por consumo");


                var itensComprasViewDto = await _rel_ConsumoProdutoRepository.GetItensPorConsumo(filtroCompraDto, gruposIds);

                //itensComprasViewDto.ForEach(item =>
                //{
                //    item.TotalParaDias = (decimal)(item.ConsumoDiario * filtroCompraDto.QuantidadeDias);
                //});

                return itensComprasViewDto;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
