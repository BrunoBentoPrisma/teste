using Ms_Compras.Database.Entidades;
using Ms_Compras.Database.Interfaces;
using Ms_Compras.SincronizadosDeDados;
using Npgsql;

namespace Ms_Compras.MigadorDeDados
{
    public class Sincronizador : ISincronizador
    {
        private readonly IGenericRepository<Grupo> _genericGrupo;
        private readonly IGenericRepository<Laboratorio> _genericLaboratorio;
        private readonly IGenericRepository<Fornecedor> _fornecedor;
        private readonly IGenericRepository<Produto> _produto;
        private readonly IGenericRepository<Cliente> _cliente;
        private readonly IGenericRepository<NotaFiscalDeEntrada> _notaFiscalDeEntrada;
        private readonly IGenericRepository<ItensNotaFiscalDeEntrada> _itemNotaFiscalDeEntrada;
        private readonly IGenericRepository<OrdemDeProducao> _ordemDeProducao;
        private readonly IGenericRepository<ItensOrdemDeProducao> _itemOrdemDeProducao;

        private readonly IGenericRepository<EmbalagemOrdemDeProducao> _embalagem;
        private readonly IGenericRepository<ItensEmbalagemOrdemDeProducao> _itemEmbalagem;

        private readonly IGenericRepository<Venda> _venda;
        private readonly IGenericRepository<ItensVenda> _itensVenda;
        private readonly IGenericRepository<FaltasEncomendas> _faltas;
        private readonly IGenericRepository<Vendedor> _vendedor;
        private readonly IGenericRepository<Lote> _lote;

        private readonly IGenericRepository<MovimentoProduto> _movimento;
        private readonly IGenericRepository<ItensFormulaVenda> _itensFormula;

        private readonly string stringDb = "User ID=postgres; Password=prixpto; Host=186.250.186.157; Port=49153; Database=farmacil-web-compras; Pooling=true;";
        public Sincronizador(IGenericRepository<Grupo> genericGrupo,
            IGenericRepository<Laboratorio> genericLaboratorio,
            IGenericRepository<Fornecedor> fornecedor,
            IGenericRepository<Produto> produto,
            IGenericRepository<Cliente> cliente,
            IGenericRepository<NotaFiscalDeEntrada> notaFiscalDeEntrada,
            IGenericRepository<ItensNotaFiscalDeEntrada> itemNotaFiscalDeEntrada,
            IGenericRepository<ItensOrdemDeProducao> itemOrdemDeProducao,
            IGenericRepository<OrdemDeProducao> ordemDeProducao,
            IGenericRepository<EmbalagemOrdemDeProducao> embalagem,
            IGenericRepository<ItensEmbalagemOrdemDeProducao> itemEmbalagem,
            IGenericRepository<Venda> venda,
            IGenericRepository<ItensVenda> itensVenda,
            IGenericRepository<Lote> lote,
            IGenericRepository<FaltasEncomendas> faltas,
            IGenericRepository<Vendedor> vendedor,
            IGenericRepository<ItensFormulaVenda> itensFormula,
            IGenericRepository<MovimentoProduto> movimento
            )
        {
            _itensFormula = itensFormula;
            _movimento = movimento;
            _vendedor = vendedor;
            _faltas = faltas;
            _embalagem = embalagem;
            _itemEmbalagem = itemEmbalagem;
            _venda = venda;
            _itensVenda = itensVenda;
            _lote = lote;
            _ordemDeProducao = ordemDeProducao;
            _itemOrdemDeProducao = itemOrdemDeProducao;
            _notaFiscalDeEntrada = notaFiscalDeEntrada;
            _itemNotaFiscalDeEntrada = itemNotaFiscalDeEntrada;
            _cliente = cliente;
            _genericGrupo = genericGrupo;
            _genericLaboratorio = genericLaboratorio;
            _fornecedor = fornecedor;
            _produto = produto;
        }

        public async Task SincronizarFornecedor()
        {
            using (var pgConecction = new NpgsqlConnection(stringDb))
            {
                var sql = "select * from data.fornecedor";
                NpgsqlCommand comand = new NpgsqlCommand(sql, pgConecction);
                await pgConecction.OpenAsync();
                NpgsqlDataReader npgsqlDataReader = comand.ExecuteReader();
                while (await npgsqlDataReader.ReadAsync())
                {
                    try
                    {
                        var obj = new Fornecedor();

                        obj.IntegracaoId = npgsqlDataReader["codigofornecedor"].ToString().Replace(",", "");
                        obj.NomeFornecedor = npgsqlDataReader["nomefornecedor"].ToString();
                        obj.Excluido = false;
                        obj.EmpresaId = Guid.Parse("fe588f0a-dfa9-11ed-b5ea-0242ac120002");
                        await _fornecedor.AdicionarAsync(obj);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        public async Task SincronizarGrupo()
        {
            using (var pgConecction = new NpgsqlConnection(stringDb))
            {
                var sql = "select * from data.grupo";
                NpgsqlCommand comand = new NpgsqlCommand(sql, pgConecction);
                await pgConecction.OpenAsync();
                NpgsqlDataReader npgsqlDataReader = comand.ExecuteReader();
                while (await npgsqlDataReader.ReadAsync())
                {
                    try
                    {
                        var grupo = new Grupo();

                        grupo.IntegracaoId = npgsqlDataReader["codigogrupo"].ToString().Replace(",", "");
                        grupo.Descricao = npgsqlDataReader["descricaogrupo"].ToString();
                        grupo.Excluido = false;
                        grupo.EmpresaId = Guid.Parse("fe588f0a-dfa9-11ed-b5ea-0242ac120002");
                        await _genericGrupo.AdicionarAsync(grupo);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        public async Task SincronizarLaboratorio()
        {
            using (var pgConecction = new NpgsqlConnection(stringDb))
            {
                var sql = "select * from data.laboratorio";
                NpgsqlCommand comand = new NpgsqlCommand(sql, pgConecction);
                await pgConecction.OpenAsync();
                NpgsqlDataReader npgsqlDataReader = comand.ExecuteReader();
                while (await npgsqlDataReader.ReadAsync())
                {
                    try
                    {
                        var obj = new Laboratorio();

                        obj.IntegracaoId = npgsqlDataReader["codigolaboratorio"].ToString().Replace(",", "");
                        obj.Descricao = npgsqlDataReader["nomelaboratorio"].ToString();
                        obj.Excluido = false;
                        obj.EmpresaId = Guid.Parse("fe588f0a-dfa9-11ed-b5ea-0242ac120002");
                        await _genericLaboratorio.AdicionarAsync(obj);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        public async Task SincronizarProduto()
        {
            using (var pgConecction = new NpgsqlConnection(stringDb))
            {
                var sql = "select * from data.produto";

                NpgsqlCommand comand = new NpgsqlCommand(sql, pgConecction);
                comand.CommandTimeout = 100000;
                await pgConecction.OpenAsync();

                NpgsqlDataReader npgsqlDataReader = comand.ExecuteReader();

                while (await npgsqlDataReader.ReadAsync())
                {
                    try
                    {

                        var produto = new Produto();

                        produto.IntegracaoId = npgsqlDataReader["codigoproduto"].ToString().Replace(",", "");
                        produto.Descricao = npgsqlDataReader["descricaoproduto"].ToString();
                        produto.UnidadeManipulacao = !string.IsNullOrEmpty(npgsqlDataReader["siglaunidade"].ToString()) ? npgsqlDataReader["siglaunidade"].ToString() : "";

                        var grupo = await _genericGrupo.GetById(x => x.IntegracaoId == npgsqlDataReader["codigogrupo"].ToString().Replace(",", ""));

                        if (grupo != null) produto.GrupoId = grupo.Id;

                        var fornecedor = await _fornecedor.GetById(x => x.IntegracaoId == npgsqlDataReader["codigoprodutofornecedor"].ToString().Replace(",", ""));

                        var laboratorio = await _genericLaboratorio.GetById(x => x.IntegracaoId == npgsqlDataReader["codigolaboratorio"].ToString().Replace(",", ""));

                        if (laboratorio is not null)
                        {
                            produto.LaboratorioId = laboratorio.Id;
                        }

                        if (fornecedor != null) produto.FornecedorId = fornecedor.Id;
                        produto.EmpresaId = Guid.Parse("fe588f0a-dfa9-11ed-b5ea-0242ac120002");
                        produto.UnidadeEstoque = !string.IsNullOrEmpty(npgsqlDataReader["siglaunidadeestoque"].ToString()) ? npgsqlDataReader["siglaunidadeestoque"].ToString() : "";
                        produto.ValorCusto = npgsqlDataReader["valorcustoproduto"] != null ? decimal.Parse(npgsqlDataReader["valorcustoproduto"].ToString()) : 0;
                        produto.ValorCustoMedio = !string.IsNullOrEmpty(npgsqlDataReader["customedioproduto"].ToString()) ? decimal.Parse(npgsqlDataReader["customedioproduto"].ToString()) : 0;
                        produto.ValorVenda = !string.IsNullOrEmpty(npgsqlDataReader["valorvendaproduto"].ToString()) ? decimal.Parse(npgsqlDataReader["valorvendaproduto"].ToString()) : 0;
                        produto.EstoqueMinimo = !string.IsNullOrEmpty(npgsqlDataReader["estoqueminimoproduto"].ToString()) ? Convert.ToInt32(decimal.Parse(npgsqlDataReader["estoqueminimoproduto"].ToString())) : 0;
                        produto.EstoqueMaximo = !string.IsNullOrEmpty(npgsqlDataReader["estoquemaximoproduto"].ToString()) ? decimal.Parse(npgsqlDataReader["estoquemaximoproduto"].ToString()) : 0;
                        produto.DataUltimaCompra = DateTime.Now;
                        produto.CurvaAbc = int.Parse(npgsqlDataReader["curvaabcproduto"].ToString().Replace(",", "").Replace(".", ""));
                        produto.AliquotaIcms = !string.IsNullOrEmpty(npgsqlDataReader["aliquotaicmsproduto"].ToString()) ? decimal.Parse(npgsqlDataReader["aliquotaicmsproduto"].ToString()) : 0;
                        produto.Inativo = int.Parse(npgsqlDataReader["inativoproduto"].ToString()) == 0 ? false : true;
                        produto.InativoCompras = !string.IsNullOrEmpty(npgsqlDataReader["inativocompras"].ToString()) ? int.Parse(npgsqlDataReader["inativocompras"].ToString()) == 0 ? false : true : false;
                        produto.Calculo = TipoCalculo.Percentual;
                        produto.SituacaoTributaria = (SituacaoTributaria)(!string.IsNullOrEmpty(npgsqlDataReader["situacaotributariaproduto"].ToString()) ? Convert.ToInt32(decimal.Parse(npgsqlDataReader["situacaotributariaproduto"].ToString())) : 0);
                        produto.CodigoBarra = !string.IsNullOrEmpty(npgsqlDataReader["codigobarraproduto"].ToString()) ? npgsqlDataReader["codigobarraproduto"].ToString() : "";
                        produto.CodigoDcb = !string.IsNullOrEmpty(npgsqlDataReader["codigodcb"].ToString()) ? npgsqlDataReader["codigodcb"].ToString() : "";
                        produto.CodigoCas = !string.IsNullOrEmpty(npgsqlDataReader["codigocas"].ToString()) ? npgsqlDataReader["codigocas"].ToString() : "";

                        var produtoCadastrado = await _produto.GetById(x => x.IntegracaoId == produto.IntegracaoId);

                        if (produtoCadastrado == null)
                        {
                            await _produto.AdicionarAsync(produto);
                        }

                    }
                    catch (Exception ex)
                    {

                        throw new Exception(ex.Message);
                    }

                }

            }
        }

        public async Task SincronizarClientes()
        {
            using (var pgConecction = new NpgsqlConnection(stringDb))
            {
                var sql = "select * from data.cliente";

                NpgsqlCommand comand = new NpgsqlCommand(sql, pgConecction);
                comand.CommandTimeout = 100000;
                await pgConecction.OpenAsync();

                NpgsqlDataReader npgsqlDataReader = comand.ExecuteReader();

                while (await npgsqlDataReader.ReadAsync())
                {
                    try
                    {

                        var cliente = new Cliente();

                        cliente.IntegracaoId = npgsqlDataReader["codigocliente"].ToString().Replace(",", "");
                        cliente.Nome = npgsqlDataReader["nomecliente"].ToString();
                        cliente.EmpresaId = Guid.Parse("fe588f0a-dfa9-11ed-b5ea-0242ac120002");

                        await _cliente.AdicionarAsync(cliente);
                    }
                    catch (Exception ex)
                    {

                        throw new Exception(ex.Message);
                    }

                }

            }
        }

        public async Task SincronizarNfeEntrada()
        {
            using (var pgConecction = new NpgsqlConnection(stringDb))
            {
                var sql = "select * from data.notafiscalentrada";

                NpgsqlCommand comand = new NpgsqlCommand(sql, pgConecction);
                comand.CommandTimeout = 100000;
                await pgConecction.OpenAsync();

                NpgsqlDataReader npgsqlDataReader = comand.ExecuteReader();

                while (await npgsqlDataReader.ReadAsync())
                {
                    try
                    {

                        var nfe = new NotaFiscalDeEntrada();
                        nfe.IntegracaoId = npgsqlDataReader["idnotafiscalentrada"].ToString().Replace(",", "");

                        var fornecedor = await _fornecedor.GetById(x => x.IntegracaoId == npgsqlDataReader["codigofornecedor"].ToString().Replace(",", "").Replace(".", ""));

                        if (fornecedor is not null)
                        {
                            nfe.EmpresaId = Guid.Parse("fe588f0a-dfa9-11ed-b5ea-0242ac120002");
                            nfe.FornecedorId = fornecedor.Id;
                            nfe.DataDeEmissao = DateTime.Parse(npgsqlDataReader["dataemissaonota"].ToString());
                            nfe.DataDeEntrada = DateTime.Parse(npgsqlDataReader["dataentradanota"].ToString());
                            nfe.NumeroNota = npgsqlDataReader["numeronota"].ToString();

                            nfe.SerieNota = npgsqlDataReader["serienota"].ToString();
                            nfe.Total = decimal.Parse(npgsqlDataReader["valornota"].ToString());
                            nfe.Frete = decimal.Parse(npgsqlDataReader["valorfretenota"].ToString());

                            await _notaFiscalDeEntrada.AdicionarAsync(nfe);
                        }

                    }
                    catch (Exception ex)
                    {

                        throw new Exception(ex.Message);
                    }

                }

            }
        }

        public async Task SincronizarItensNfeEntrada()
        {
            using (var pgConecction = new NpgsqlConnection(stringDb))
            {
                var sql = "SELECT * FROM data.itemnotafiscalentrada " +
                    "ORDER BY serienota ASC, dataoperacao ASC, horaoperacao ASC, codigofornecedor ASC, numeronota ASC";

                NpgsqlCommand comand = new NpgsqlCommand(sql, pgConecction);
                comand.CommandTimeout = 100000;
                await pgConecction.OpenAsync();

                NpgsqlDataReader npgsqlDataReader = comand.ExecuteReader();

                while (await npgsqlDataReader.ReadAsync())
                {
                    try
                    {

                        var item = new ItensNotaFiscalDeEntrada();
                        var nfe = await _notaFiscalDeEntrada.GetById(x => x.IntegracaoId == npgsqlDataReader["idnotafiscalentrada"].ToString().Replace(",", "").Replace(".", ""));
                        var produto = await _produto.GetById(x => x.IntegracaoId == npgsqlDataReader["codigoproduto"].ToString().Replace(",", "").Replace(".", ""));

                        if (nfe is null || produto is null) throw new Exception("nfe ou produto null");
                        item.EmpresaId = Guid.Parse("fe588f0a-dfa9-11ed-b5ea-0242ac120002");
                        item.ProdutoId = produto.Id;
                        item.NotaFiscalDeEntradaId = nfe.Id;
                        item.ValorUnitario = !string.IsNullOrEmpty(npgsqlDataReader["valorunitarioitementrada"].ToString()) ? decimal.Parse(npgsqlDataReader["valorunitarioitementrada"].ToString().Replace(".", ",")) : 0;
                        item.Total = !string.IsNullOrEmpty(npgsqlDataReader["valortotalitementrada"].ToString()) ? decimal.Parse(npgsqlDataReader["valortotalitementrada"].ToString().Replace(".", ",")) : 0;
                        item.Frete = !string.IsNullOrEmpty(npgsqlDataReader["valorfreteitem"].ToString()) ? decimal.Parse(npgsqlDataReader["valorfreteitem"].ToString().Replace(".", ",")) : 0;
                        item.Quantidade = !string.IsNullOrEmpty(npgsqlDataReader["quantidadeitementrada"].ToString()) ? decimal.Parse(npgsqlDataReader["quantidadeitementrada"].ToString().Replace(".", ",")) : 0;
                        item.ValorIpi = !string.IsNullOrEmpty(npgsqlDataReader["valoripiitem"].ToString()) ? decimal.Parse(npgsqlDataReader["valoripiitem"].ToString().Replace(".", ",")) : 0;
                        item.Validade = !string.IsNullOrEmpty(npgsqlDataReader["datavalidadeitementrada"].ToString()) ? DateTime.Parse(npgsqlDataReader["datavalidadeitementrada"].ToString()) : DateTime.MinValue;
                        item.LoteFornecedorEntrada = npgsqlDataReader["lotefornecedoritementrada"].ToString();
                        await _itemNotaFiscalDeEntrada.AdicionarAsync(item);

                    }
                    catch (Exception ex)
                    {

                        throw new Exception(ex.Message);
                    }

                }

                await pgConecction.CloseAsync();
            }
        }

        public async Task SincronizarOrdemDeProducao()
        {
            using (var pgConecction = new NpgsqlConnection(stringDb))
            {
                var sql = "select * from data.ordemproducao";

                NpgsqlCommand comand = new NpgsqlCommand(sql, pgConecction);
                comand.CommandTimeout = 100000;
                await pgConecction.OpenAsync();

                NpgsqlDataReader npgsqlDataReader = comand.ExecuteReader();

                while (await npgsqlDataReader.ReadAsync())
                {
                    try
                    {

                        var ordemValid = await _ordemDeProducao.GetById(x => x.IntegracaoId == npgsqlDataReader["anoordemproducao"].ToString().Replace(",", "") + npgsqlDataReader["numeroordemproducao"].ToString().Replace(",", ""));

                        if (ordemValid is null)
                        {

                            var ordem = new OrdemDeProducao();

                            ordem.IntegracaoId = npgsqlDataReader["anoordemproducao"].ToString().Replace(",", "") + npgsqlDataReader["numeroordemproducao"].ToString().Replace(",", "");
                            
                            var cliente = await _cliente.GetById(x => x.IntegracaoId == npgsqlDataReader["codigocliente"].ToString().Replace(",", "").Replace(".", ""));

                            if (cliente != null)
                            {
                                var produto = await _produto.GetById(x => x.IntegracaoId == npgsqlDataReader["codigoprodutoembalagem"].ToString().Replace(",", "").Replace(".", ""));
                                var grupo = await _genericGrupo.GetById(x => x.IntegracaoId == npgsqlDataReader["codigogrupoembalagem"].ToString().Replace(",", "").Replace(".", ""));
                                var grupoCapsula = await _genericGrupo.GetById(x => x.IntegracaoId == npgsqlDataReader["codigogrupocapsula"].ToString().Replace(",", "").Replace(".", ""));
                                var produtoCapsula = await _produto.GetById(x => x.IntegracaoId == npgsqlDataReader["codigoprodutocapsula"].ToString().Replace(",", "").Replace(".", ""));

                                if (produto is not null) ordem.ProdutoEmbalagemId = produto.Id;
                                if (grupo is not null) ordem.GrupoEmbalagemId = grupo.Id;
                                if (grupoCapsula is not null) ordem.GrupoCapsulaId = grupoCapsula.Id;
                                if(produtoCapsula is not null) ordem.ProdutoCapsulaId = produtoCapsula.Id;

                                ordem.ClienteId = cliente.Id;
                                ordem.DataDeEmissao = DateTime.Parse(npgsqlDataReader["dataemissaoordemproducao"].ToString());
                                ordem.Status = int.Parse(npgsqlDataReader["statusordemproducao"].ToString().Replace(",", "").Replace(".", ""));
                                ordem.EmpresaId = Guid.Parse("fe588f0a-dfa9-11ed-b5ea-0242ac120002");
                                var ano = int.Parse(npgsqlDataReader["anoordemproducao"].ToString().Replace(",", "").Replace(".", ""));
                                var anoDate = new DateTime();
                                anoDate.AddYears(ano);

                                ordem.AnoOrdemDeProducao = anoDate;

                                ordem.CodigoForma = int.Parse(npgsqlDataReader["codigoforma"].ToString().Replace(",", "").Replace(".", ""));
                                ordem.NumeroOrdemDeProducao = npgsqlDataReader["numeroordemproducao"].ToString();
                                ordem.CapsulaOrdemDeProducao = !string.IsNullOrEmpty(npgsqlDataReader["capsulasordemproducao"].ToString()) ? int.Parse(npgsqlDataReader["capsulasordemproducao"].ToString().Replace(",","").Replace(".","")) : 0;

                                var venda = await _venda.GetById(x => x.IntegracaoId == npgsqlDataReader["numerovenda"].ToString());

                                if (venda is not null) ordem.VendaId = venda.Id;

                                var integracaoId = npgsqlDataReader["numeroformula"].ToString().Replace(",", "").Replace(".", "") +
                                npgsqlDataReader["numerovenda"].ToString().Replace(",", "").Replace(".", "");

                                var itensFormulaVendaId = await _itensFormula.GetById(x => x.IntegracaoId == integracaoId);


                                if (itensFormulaVendaId is not null) ordem.ItensFormulaVendaId = itensFormulaVendaId.Id;

                                await _ordemDeProducao.AdicionarAsync(ordem);
                            }

                        }



                    }
                    catch (Exception ex)
                    {

                        throw new Exception(ex.Message);
                    }

                }

            }
        }

        public async Task SincronizarItensOrdemDeProducao()
        {
            using (var pgConecction = new NpgsqlConnection(stringDb))
            {
                var sql = "SELECT * FROM data.itemordemproducao";

                NpgsqlCommand comand = new NpgsqlCommand(sql, pgConecction);
                comand.CommandTimeout = 100000;
                await pgConecction.OpenAsync();

                NpgsqlDataReader npgsqlDataReader = comand.ExecuteReader();

                while (await npgsqlDataReader.ReadAsync())
                {
                    try
                    {

                        var item = new ItensOrdemDeProducao();

                        var ordem = await _ordemDeProducao.GetById(x => x.IntegracaoId == npgsqlDataReader["anoordemproducao"].ToString().Replace(",", "") + npgsqlDataReader["numeroordemproducao"].ToString().Replace(",", ""));
                        var produto = await _produto.GetById(x => x.IntegracaoId == npgsqlDataReader["codigoproduto"].ToString().Replace(",", "").Replace(".", ""));

                        if (ordem is not null && produto is not null)
                        {
                            item.EmpresaId = Guid.Parse("fe588f0a-dfa9-11ed-b5ea-0242ac120002");
                            item.ProdutoId = produto.Id;
                            item.GrupoId = produto.GrupoId;
                            item.OrdemDeProducaoId = ordem.Id;
                            item.SequenciaItem = !string.IsNullOrEmpty(npgsqlDataReader["sequenciaitemordemproducao"].ToString()) ? int.Parse(npgsqlDataReader["sequenciaitemordemproducao"].ToString().Replace(".", "").Replace(",", "")) : 0;
                            item.QuantidadeUnidadeEstoque = !string.IsNullOrEmpty(npgsqlDataReader["quantidadeunidadeestoque"].ToString()) ? decimal.Parse(npgsqlDataReader["quantidadeunidadeestoque"].ToString()) : 0;
                            item.QuantidadeComprometidaLote = !string.IsNullOrEmpty(npgsqlDataReader["quantidadecomprometidalote"].ToString()) ? decimal.Parse(npgsqlDataReader["quantidadecomprometidalote"].ToString()) : 0;

                            await _itemOrdemDeProducao.AdicionarAsync(item);
                        }


                    }
                    catch (Exception ex)
                    {

                        throw new Exception(ex.Message);
                    }

                }

                await pgConecction.CloseAsync();
            }
        }

        public async Task SincronizarEmbalagem()
        {
            using (var pgConecction = new NpgsqlConnection(stringDb))
            {
                var sql = "SELECT * FROM data.embalagemordemproducao";

                NpgsqlCommand comand = new NpgsqlCommand(sql, pgConecction);
                comand.CommandTimeout = 100000;
                await pgConecction.OpenAsync();

                NpgsqlDataReader npgsqlDataReader = comand.ExecuteReader();

                while (await npgsqlDataReader.ReadAsync())
                {
                    try
                    {

                        var embalagem = await _embalagem.GetById(x => x.IntegracaoId == npgsqlDataReader["anoordemproducao"].ToString().Replace(",", "").Replace(".", "") + npgsqlDataReader["numeroordemproducao"].ToString().Replace(",", "").Replace(".", ""));

                        if (embalagem is null)
                        {
                            var item = new EmbalagemOrdemDeProducao();

                            var ordem = await _ordemDeProducao.GetById(x => x.IntegracaoId == npgsqlDataReader["anoordemproducao"].ToString().Replace(",", "").Replace(".", "") + npgsqlDataReader["numeroordemproducao"].ToString().Replace(",", "").Replace(".", ""));

                            if (ordem is not null) item.OrdemDeProducaoId = ordem.Id;

                            var produto = await _produto.GetById(x => x.IntegracaoId == npgsqlDataReader["codigoproduto"].ToString().Replace(",", "").Replace(".", ""));
                            var grupo = await _genericGrupo.GetById(x => x.IntegracaoId == npgsqlDataReader["codigogrupo"].ToString().Replace(",", "").Replace(".", ""));

                            if (produto is not null) item.ProdutoId = produto.Id;
                            if (grupo is not null) item.GrupoId = grupo.Id;

                            item.QuatidadeComprometidaLote = decimal.Parse(npgsqlDataReader["quantidadecomprometidalote"].ToString());
                            item.QuatidadeUnidadeEstoque = decimal.Parse(npgsqlDataReader["quantidadeunidadeestoque"].ToString());
                            item.EmpresaId = Guid.Parse("fe588f0a-dfa9-11ed-b5ea-0242ac120002");
                            item.DataDeCadastro = DateTime.Parse(npgsqlDataReader["dt_creation"].ToString());
                            item.IntegracaoId = npgsqlDataReader["anoordemproducao"].ToString().Replace(",", "").Replace(".", "") + npgsqlDataReader["numeroordemproducao"].ToString().Replace(",", "").Replace(".", "");
                            //item.NumeroOrdemDeProducao = npgsqlDataReader["numeroordemproducao"].ToString().Replace(",", "").Replace(".", "");
                            await _embalagem.AdicionarAsync(item);

                        }

                    }
                    catch (Exception ex)
                    {

                        throw new Exception(ex.Message);
                    }

                }

                await pgConecction.CloseAsync();
            }
        }

        public async Task SincronizarItemEmbalagem()
        {
            using (var pgConecction = new NpgsqlConnection(stringDb))
            {
                var sql = "SELECT * FROM data.itemembalagemordemproducao";

                NpgsqlCommand comand = new NpgsqlCommand(sql, pgConecction);
                comand.CommandTimeout = 100000;
                await pgConecction.OpenAsync();

                NpgsqlDataReader npgsqlDataReader = comand.ExecuteReader();

                while (await npgsqlDataReader.ReadAsync())
                {
                    try
                    {

                        var item = new ItensEmbalagemOrdemDeProducao();

                        var embalagem = await _embalagem.GetById(x => x.IntegracaoId == npgsqlDataReader["anoordemproducao"].ToString().Replace(",", "").Replace(".", "") + npgsqlDataReader["numeroordemproducao"].ToString().Replace(",", "").Replace(".", ""));
                        var produto = await _produto.GetById(x => x.IntegracaoId == npgsqlDataReader["codigoproduto"].ToString().Replace(",", "").Replace(".", ""));
                        var grupo = await _genericGrupo.GetById(x => x.IntegracaoId == npgsqlDataReader["codigogrupo"].ToString().Replace(",", "").Replace(".", ""));
                        if (embalagem is not null && produto is not null)
                        {
                            if(grupo is not null) item.GrupoId = grupo.Id;
                            item.EmbalagemOrdemDeProducaoId = embalagem.Id;
                            item.ProdutoId = produto.Id;
                            item.FatorDeCorrecao = !string.IsNullOrEmpty(npgsqlDataReader["fatorcorrecaoitemordem"].ToString()) ? decimal.Parse(npgsqlDataReader["fatorcorrecaoitemordem"].ToString()) : 0;
                            item.LoteInterno = !string.IsNullOrEmpty(npgsqlDataReader["loteinterno"].ToString().Replace(",", "").Replace(".", "")) ? int.Parse(npgsqlDataReader["loteinterno"].ToString().Replace(",", "").Replace(".", "")) : 0;
                            item.Sequencia = int.Parse(npgsqlDataReader["sequenciaitem"].ToString());
                            item.EmpresaId = Guid.Parse("fe588f0a-dfa9-11ed-b5ea-0242ac120002");
                            item.DataDeCadastro = DateTime.Parse(npgsqlDataReader["dt_creation"].ToString());
                            item.IntegracaoId = npgsqlDataReader["numeroordemproducao"].ToString().Replace(",", "").Replace(".", "");
                            item.QuantidadeComprometidaLote = !string.IsNullOrEmpty(npgsqlDataReader["quantidadecomprometidalote"].ToString()) ? decimal.Parse(npgsqlDataReader["quantidadecomprometidalote"].ToString()) : 0;
                            item.QuantidadePesada = !string.IsNullOrEmpty(npgsqlDataReader["quantidadepesadaitemordem"].ToString()) ? decimal.Parse(npgsqlDataReader["quantidadepesadaitemordem"].ToString()) : 0;
                            item.QuantidadeSemFator = !string.IsNullOrEmpty(npgsqlDataReader["quantidadesemfatoritemordem"].ToString()) ? decimal.Parse(npgsqlDataReader["quantidadesemfatoritemordem"].ToString()) : 0;
                            item.QuantidadeUnidadeEstoque = !string.IsNullOrEmpty(npgsqlDataReader["quantidadeunidadeestoque"].ToString()) ? decimal.Parse(npgsqlDataReader["quantidadeunidadeestoque"].ToString()) : 0;

                            await _itemEmbalagem.AdicionarAsync(item);
                        }


                    }
                    catch (Exception ex)
                    {

                        throw new Exception(ex.Message);
                    }

                }

                await pgConecction.CloseAsync();
            }
        }

        public async Task SincronizarVenda()
        {
            using (var pgConecction = new NpgsqlConnection(stringDb))
            {
                var sql = "SELECT * FROM data.venda";

                NpgsqlCommand comand = new NpgsqlCommand(sql, pgConecction);
                comand.CommandTimeout = 100000;
                await pgConecction.OpenAsync();

                NpgsqlDataReader npgsqlDataReader = comand.ExecuteReader();

                while (await npgsqlDataReader.ReadAsync())
                {
                    try
                    {

                        var item = new Venda();

                        item.EmpresaId = Guid.Parse("fe588f0a-dfa9-11ed-b5ea-0242ac120002");
                        item.DataDeCadastro = DateTime.Parse(npgsqlDataReader["dt_creation"].ToString());
                        item.IntegracaoId = npgsqlDataReader["numerovenda"].ToString().Replace(",", "").Replace(".", "");
                        item.Status = int.Parse(npgsqlDataReader["statusvenda"].ToString().Replace(",", "").Replace(".", ""));
                        item.Orcamento = int.Parse(npgsqlDataReader["statusorcamentovenda"].ToString().Replace(",", "").Replace(".", ""));
                        item.DataDeEmissao = DateTime.Parse(npgsqlDataReader["dataemissaovenda"].ToString());
                        item.Total = decimal.Parse(npgsqlDataReader["valortotalvenda"].ToString());
                        item.NumeroVenda = decimal.Parse(npgsqlDataReader["numerovenda"].ToString());

                        await _venda.AdicionarAsync(item);

                    }
                    catch (Exception ex)
                    {

                        throw new Exception(ex.Message);
                    }

                }

                await pgConecction.CloseAsync();
            }
        }

        public async Task SincronizarItemVenda()
        {
            using (var pgConecction = new NpgsqlConnection(stringDb))
            {
                var sql = "SELECT * FROM data.itemvenda";

                NpgsqlCommand comand = new NpgsqlCommand(sql, pgConecction);
                comand.CommandTimeout = 100000;
                await pgConecction.OpenAsync();

                NpgsqlDataReader npgsqlDataReader = comand.ExecuteReader();

                while (await npgsqlDataReader.ReadAsync())
                {
                    try
                    {
                        var venda = await _venda.GetById(x => x.IntegracaoId == npgsqlDataReader["numerovenda"].ToString().Replace(",", "").Replace(".", ""));
                        var produto = await _produto.GetById(x => x.IntegracaoId == npgsqlDataReader["codigoproduto"].ToString().Replace(",", "").Replace(".", ""));
                        var grupo = await _genericGrupo.GetById(x => x.IntegracaoId == npgsqlDataReader["codigogrupo"].ToString().Replace(",", "").Replace(".", ""));

                        if (venda is not null && produto is not null)
                        {
                            
                            var item = new ItensVenda();
                            
                            if(grupo is not null) item.GrupoId = grupo.Id;

                            item.ProdutoId = produto.Id;
                            item.GrupoId = produto.GrupoId;
                            item.EmpresaId = Guid.Parse("fe588f0a-dfa9-11ed-b5ea-0242ac120002");
                            item.DataDeCadastro = DateTime.Parse(npgsqlDataReader["dt_creation"].ToString());
                            item.IntegracaoId = npgsqlDataReader["numerovenda"].ToString().Replace(",", "").Replace(".", "");
                            item.Quantidade = decimal.Parse(npgsqlDataReader["quantidadeitemvenda"].ToString());
                            item.ValorUnitario = decimal.Parse(npgsqlDataReader["valorliquidovendaitem"].ToString());
                            item.Total = item.ValorUnitario * item.Quantidade;
                            item.VendaId = venda.Id;
                            item.SequenciaItem = int.Parse(npgsqlDataReader["sequenciaitemvenda"].ToString().Replace(",", "").Replace(".", ""));
                            item.ValorCusto = !string.IsNullOrEmpty(npgsqlDataReader["valorcustoitemvenda"].ToString()) ? decimal.Parse(npgsqlDataReader["valorcustoitemvenda"].ToString()) : 0;
                            item.ValorVenda = !string.IsNullOrEmpty(npgsqlDataReader["valorvendaitemvenda"].ToString()) ? decimal.Parse(npgsqlDataReader["valorvendaitemvenda"].ToString()) : 0;
                            item.ValorLiquido = !string.IsNullOrEmpty(npgsqlDataReader["valorliquidoitemvenda"].ToString()) ? decimal.Parse(npgsqlDataReader["valorliquidoitemvenda"].ToString()) : 0;

                            await _itensVenda.AdicionarAsync(item);
                        }


                    }
                    catch (Exception ex)
                    {

                        throw new Exception(ex.Message);
                    }

                }

                await pgConecction.CloseAsync();
            }
        }

        public async Task SincronizarLote()
        {
            using (var pgConecction = new NpgsqlConnection(stringDb))
            {
                var sql = "SELECT * FROM data.lote";

                NpgsqlCommand comand = new NpgsqlCommand(sql, pgConecction);
                comand.CommandTimeout = 100000;
                await pgConecction.OpenAsync();

                NpgsqlDataReader npgsqlDataReader = comand.ExecuteReader();

                while (await npgsqlDataReader.ReadAsync())
                {
                    try
                    {

                        var item = new Lote();

                        var fornecedor = await _fornecedor.GetById(x => x.IntegracaoId == npgsqlDataReader["codigofornecedor"].ToString().Replace(",", "").Replace(".", ""));
                        var produto = await _produto.GetById(x => x.IntegracaoId == npgsqlDataReader["codigoproduto"].ToString().Replace(",", "").Replace(".", ""));
                        var grupo = await _genericGrupo.GetById(x => x.IntegracaoId == npgsqlDataReader["codigogrupo"].ToString().Replace(",", "").Replace(".", ""));
                        if (fornecedor is not null && produto is not null)
                        {
                            item.FornecedorId = fornecedor.Id;
                            item.ProdutoId = produto.Id;
                            if(grupo is not null) item.GrupoId = grupo.Id;

                            item.QuantidadeComprometidaLote = decimal.Parse(npgsqlDataReader["quantidadecomprometidalote"].ToString());
                            item.EmpresaId = Guid.Parse("fe588f0a-dfa9-11ed-b5ea-0242ac120002");
                            item.DataDeCadastro = DateTime.Parse(npgsqlDataReader["dt_creation"].ToString());
                            item.DataDeEntradaDoLote = !string.IsNullOrEmpty(npgsqlDataReader["dataentradalote"].ToString()) ? DateTime.Parse(npgsqlDataReader["dataentradalote"].ToString()) : DateTime.MinValue;
                            item.DataDeFabricacaoDoLote = !string.IsNullOrEmpty(npgsqlDataReader["datafabricacaolote"].ToString()) ? DateTime.Parse(npgsqlDataReader["datafabricacaolote"].ToString()) : DateTime.MinValue;
                            item.DataDeValidadeDoLote = !string.IsNullOrEmpty(npgsqlDataReader["datavalidadelote"].ToString()) ? DateTime.Parse(npgsqlDataReader["datavalidadelote"].ToString()) : DateTime.MinValue;
                            item.NumeroNota = npgsqlDataReader["numeronota"].ToString().Replace(",", "").Replace(".", "");
                            item.SerieNota = npgsqlDataReader["serienota"].ToString().Replace(",", "").Replace(".", "");
                            item.IntegracaoId = npgsqlDataReader["loteinterno"].ToString().Replace(",", "").Replace(".", "");

                            await _lote.AdicionarAsync(item);

                        }



                    }
                    catch (Exception ex)
                    {

                        throw new Exception(ex.Message);
                    }

                }

                await pgConecction.CloseAsync();
            }
        }

        public async Task SincronizarFaltas()
        {
            using (var pgConecction = new NpgsqlConnection(stringDb))
            {
                var sql = "SELECT * FROM data.faltasencomendas";

                NpgsqlCommand comand = new NpgsqlCommand(sql, pgConecction);
                comand.CommandTimeout = 100000;
                await pgConecction.OpenAsync();

                NpgsqlDataReader npgsqlDataReader = comand.ExecuteReader();

                while (await npgsqlDataReader.ReadAsync())
                {
                    try
                    {

                        var cliente = await _cliente.GetById(x => x.IntegracaoId == npgsqlDataReader["codigocliente"].ToString().Replace(",", "").Replace(".", ""));
                        var vendedor = await _vendedor.GetById(x => x.IntegracaoId == npgsqlDataReader["codigovendedor"].ToString().Replace(",", "").Replace(".", ""));
                        var produto = await _produto.GetById(x => x.IntegracaoId == npgsqlDataReader["codigoproduto"].ToString().Replace(",", "").Replace(".", ""));
                        var grupo = await _genericGrupo.GetById(x => x.IntegracaoId == npgsqlDataReader["codigogrupo"].ToString().Replace(",", "").Replace(".", ""));

                        if (produto is not null && grupo is not null)
                        {

                            var item = new FaltasEncomendas();

                            if (cliente is not null) item.ClienteId = cliente.Id;
                            if (vendedor is not null) item.VendedorId = vendedor.Id;

                            item.ProdutoId = produto.Id;
                            item.GrupoId = grupo.Id;

                            item.Observacao = npgsqlDataReader["obscliente"].ToString();
                            item.PrevisaoDeEntrega = !string.IsNullOrEmpty(npgsqlDataReader["previsaoentrega"].ToString()) ? DateTime.Parse(npgsqlDataReader["previsaoentrega"].ToString()) : null;
                            item.Quantidade = decimal.Parse(npgsqlDataReader["quantidade"].ToString());
                            item.Status = int.Parse(npgsqlDataReader["status"].ToString().Replace(",", "").Replace(".", ""));
                            item.Telefone = npgsqlDataReader["telefone"].ToString();
                            item.Tipo = int.Parse(npgsqlDataReader["tipoencomenda"].ToString().Replace(",", "").Replace(".", ""));
                            item.EmpresaId = Guid.Parse("fe588f0a-dfa9-11ed-b5ea-0242ac120002");
                            item.DataDeCadastro = DateTime.Parse(npgsqlDataReader["dt_creation"].ToString());
                            item.IntegracaoId = npgsqlDataReader["idfaltasencomendas"].ToString().Replace(",", "").Replace(".", "");

                            await _faltas.AdicionarAsync(item);
                        }


                    }
                    catch (Exception ex)
                    {

                        throw new Exception(ex.Message);
                    }

                }

                await pgConecction.CloseAsync();
            }
        }

        public async Task SincronizarVendedor()
        {
            using (var pgConecction = new NpgsqlConnection(stringDb))
            {
                var sql = "select * from data.vendedor";
                NpgsqlCommand comand = new NpgsqlCommand(sql, pgConecction);
                await pgConecction.OpenAsync();
                NpgsqlDataReader npgsqlDataReader = comand.ExecuteReader();
                while (await npgsqlDataReader.ReadAsync())
                {
                    try
                    {
                        var obj = new Vendedor();

                        obj.IntegracaoId = npgsqlDataReader["codigovendedor"].ToString().Replace(",", "");
                        obj.Nome = npgsqlDataReader["nomevendedor"].ToString();
                        obj.Excluido = false;
                        obj.EmpresaId = Guid.Parse("fe588f0a-dfa9-11ed-b5ea-0242ac120002");
                        await _vendedor.AdicionarAsync(obj);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        public async Task SincronizarMovimento()
        {
            using (var pgConecction = new NpgsqlConnection(stringDb))
            {
                var sql = "SELECT * FROM data.movimentoproduto";

                NpgsqlCommand comand = new NpgsqlCommand(sql, pgConecction);
                comand.CommandTimeout = 100000;
                await pgConecction.OpenAsync();

                NpgsqlDataReader npgsqlDataReader = comand.ExecuteReader();

                while (await npgsqlDataReader.ReadAsync())
                {
                    try
                    {

                        var item = new MovimentoProduto();

                        var produto = await _produto.GetById(x => x.IntegracaoId == npgsqlDataReader["codigoproduto"].ToString().Replace(",", "").Replace(".", ""));
                        var grupo = await _genericGrupo.GetById(x => x.IntegracaoId == npgsqlDataReader["codigogrupo"].ToString().Replace(",", "").Replace(".", ""));
                        if (produto is not null)
                        {
                            if(grupo is not null) item.GrupoId = grupo.Id;
                            item.Saldo = decimal.Parse(npgsqlDataReader["saldoproduto"].ToString());
                            item.ProdutoId = produto.Id;
                            item.QuantidadeMovimentada = decimal.Parse(npgsqlDataReader["quantidademovimentada"].ToString());
                            item.EmpresaId = Guid.Parse("fe588f0a-dfa9-11ed-b5ea-0242ac120002");
                            item.DataDeCadastro = DateTime.Parse(npgsqlDataReader["dt_creation"].ToString());
                            item.IntegracaoId = npgsqlDataReader["codigomovimentoproduto"].ToString().Replace(",", "").Replace(".", "");

                            await _movimento.AdicionarAsync(item);

                        }



                    }
                    catch (Exception ex)
                    {

                        throw new Exception(ex.Message);
                    }

                }

                await pgConecction.CloseAsync();
            }
        }

        public async Task SincronizarItensFormuzaVenda()
        {
            using (var pgConecction = new NpgsqlConnection(stringDb))
            {
                var sql = "SELECT * FROM data.itemformulavenda";

                NpgsqlCommand comand = new NpgsqlCommand(sql, pgConecction);
                comand.CommandTimeout = 100000;
                await pgConecction.OpenAsync();

                NpgsqlDataReader npgsqlDataReader = comand.ExecuteReader();

                while (await npgsqlDataReader.ReadAsync())
                {
                    try
                    {

                        var item = new ItensFormulaVenda();

                        var produto = await _produto.GetById(x => x.IntegracaoId == npgsqlDataReader["codigoproduto"].ToString().Replace(",", "").Replace(".", ""));
                        var venda = await _venda.GetById(x => x.IntegracaoId == npgsqlDataReader["numerovenda"].ToString().Replace(",", "").Replace(".", ""));
                        
                        if (produto is not null)
                        {
                            if (venda is not null) item.VendaId = venda.Id;

                            item.ProdutoId = produto.Id;
                            item.EmpresaId = Guid.Parse("fe588f0a-dfa9-11ed-b5ea-0242ac120002");
                            item.DataDeCadastro = DateTime.Parse(npgsqlDataReader["dt_creation"].ToString());
                            item.IntegracaoId = npgsqlDataReader["numeroformula"].ToString().Replace(",", "").Replace(".", "") +
                                npgsqlDataReader["numerovenda"].ToString().Replace(",", "").Replace(".", "");

                            item.ValorCusto = !string.IsNullOrEmpty(npgsqlDataReader["valorcustoitemformula"].ToString()) ? decimal.Parse(npgsqlDataReader["valorcustoitemformula"].ToString()) : 0;
                            item.ValorUnitario = !string.IsNullOrEmpty(npgsqlDataReader["valorunitarioitem"].ToString()) ? decimal.Parse(npgsqlDataReader["valorunitarioitem"].ToString()) : 0;
                            item.ValorTotal = !string.IsNullOrEmpty(npgsqlDataReader["valortotalitem"].ToString()) ? decimal.Parse(npgsqlDataReader["valortotalitem"].ToString()) : 0;
                            
                            await _itensFormula.AdicionarAsync(item);

                        }



                    }
                    catch (Exception ex)
                    {

                        throw new Exception(ex.Message);
                    }

                }

                await pgConecction.CloseAsync();
            }
        }
    }
}
