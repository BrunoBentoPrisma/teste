using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Ms_Compras.Context;
using Ms_Compras.Database.Interfaces;
using Ms_Compras.Database.Repository;
using Ms_Compras.RabbitMq.Consumer.ClienteConsumer;
using Ms_Compras.RabbitMq.Consumer.FornecedorConsumer;
using Ms_Compras.RabbitMq.Consumer.GrupoConsumer;
using Ms_Compras.RabbitMq.Consumer.LaboratorioConsumer;
using Ms_Compras.RabbitMq.Consumer.MovimentoProdutoConsumer;
using Ms_Compras.RabbitMq.Consumer.NotaFiscalDeEntradaConsumer;
using Ms_Compras.RabbitMq.Consumer.OrdemDeProducaoConsumer;
using Ms_Compras.RabbitMq.Consumer.ProdutoConsumer;
using Ms_Compras.RabbitMq.Consumer.VendedorConsumer;
using Ms_Compras.RabbitMq.Producer.Implementacao;
using Ms_Compras.RabbitMq.Producer.Interfaces;
using Ms_Compras.RabbitMq.ProdutoConsumer;
using Ms_Compras.Services.Interfaces;
using Ms_Compras.Services.Service;
using System.Text;

namespace Ms_Compras.Config
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddContext(this IServiceCollection services) 
        {
            services.AddDbContext<MsContext>();

            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<ICompraFornecedorService, CompraFornecedorService>();
            services.AddSingleton<IFiltroCompraTipoVendaService, FiltroCompraTipoVendaService>();
            services.AddSingleton<IFiltroCompraFornecedorService, FiltroCompraFornecedorService>();
            services.AddSingleton<ICompraService, CompraService>();
            services.AddSingleton<IFaltasEncomendasService, FaltasEncomendasService>();
            services.AddSingleton<IItemCompraFornecedorService, ItemCompraFornecedorService>();
            services.AddSingleton<IPermissaoService, PermissaoService>();
            services.AddSingleton<IGerarArquivoCoteFacilService, GerarArquivoCoteFacilService>();
            services.AddSingleton<IGerarArquivosEmbraFarmaService, GerarArquivosEmbraFarmaService>();
            services.AddSingleton<IFiltroCompraTipoEstoqueMaximoService, FiltroCompraTipoEstoqueMaximoService>();
            services.AddSingleton<IFiltroCompraTipoEstoqueMinimoService, FiltroCompraTipoEstoqueMinimoService>();
            services.AddSingleton<IFiltroCompraTipoDemandaService, FiltroCompraTipoDemandaService>();
            services.AddSingleton<IFiltroCompraTipoFaltasEncomendasService, FiltroCompraTipoFaltasEncomendasService>();
            services.AddSingleton<IFiltroCompraTipoConsumoService, FiltroCompraTipoConsumoService>();

            return services;
        }
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddSingleton<ICompraFornecedorRepository, CompraFornecedorRepository>();
            services.AddSingleton<IFaltasEncomendasRepository, FaltasEncomendasRepository>();
            services.AddSingleton<ICompraRepository, CompraRepository>();
            services.AddSingleton<IProdutoRepository, ProdutoRepository>();
            services.AddSingleton<IItemCompraFornecedorRepository, ItemCompraFornecedorRepository>();
            services.AddSingleton<IEmbalagemOrdemDeProducaoRepository, EmbalagemOrdemDeProducaoRepository>();
            services.AddSingleton<IGrupoRepository, GrupoRepository>();
            services.AddSingleton<ILoteRepository, LoteRepository>();
            services.AddSingleton<IMovimentoProdutoRepository, MovimentoProdutoRepository>();
            services.AddSingleton<IOrdemDeProducaoRepository, OrdemDeProducaoRepository>();
            services.AddSingleton<IVendaRepository, VendaRepository>();
            services.AddSingleton<IConfigEstoqueFilialRepository, ConfigEstoqueFilialRepository>();
            services.AddSingleton<INotaFiscalDeEntradaRepository, NotaFiscalDeEntradaRepository>();
            services.AddSingleton<IClienteVendedorRepository, ClienteVendedorRepository>();
            services.AddSingleton<IFiltroComprasPorDemandaRepository, FiltroComprasPorDemandaRepository>();
            services.AddSingleton<IRel_ConsumoProdutoRepository, Rel_ConsumoProdutoRepository>();
            services.AddSingleton<IRel_ConsumoProdutoRepository, Rel_ConsumoProdutoRepository>();

            return services;
        }
        public static IServiceCollection AddProducerRabbitMq(this IServiceCollection services)
        {
            services.AddSingleton<ICompraProducer, CompraProducer>();
            services.AddSingleton<ICompraFornecedorProducer, CompraFornecedorProducer>();
            services.AddSingleton<IFaltasEncomendasProducer, FaltasEncomendasProducer>();
            services.AddSingleton<IEmailEmbraFarmaProducer, EmailEmbraFarmaProducer>();

            return services;
        }
        public static IServiceCollection AddConsumerRabbitMq(this IServiceCollection services)
        {
            services.AddHostedService<FornecedorAdicionarConsumer>();
            services.AddHostedService<FornecedorExcluirConsumer>();
            services.AddHostedService<FornecedorEditConsumer>();

            services.AddHostedService<ClienteAdicionarConsumer>();
            services.AddHostedService<ClienteExcluirConsumer>();
            services.AddHostedService<ClienteEditConsumer>();

            services.AddHostedService<GrupoAdicionarConsumer>();
            services.AddHostedService<GrupoExcluirConsumer>();
            services.AddHostedService<GrupoEditConsumer>();

            services.AddHostedService<LaboratorioAdicionarConsumer>();
            services.AddHostedService<LaboratorioExcluirConsumer>();
            services.AddHostedService<LaboratorioEditConsumer>();

            services.AddHostedService<MovimentaProdutoAdicionarConsumer>();

            services.AddHostedService<NotaFiscalDeEntradaAdicionarConsumer>();
            services.AddHostedService<NotaFiscalDeEntradaExcluirConsumer>();
            services.AddHostedService<NotaFiscalDeEntradaEditConsumer>();

            services.AddHostedService<OrdemDeProducaoAdicionarConsumer>();
            services.AddHostedService<OrdemDeProducaoExcluirConsumer>();
            services.AddHostedService<OrdemDeProducaoEditConsumer>();

            services.AddHostedService<ProdutoAdicionarConsumer>();
            services.AddHostedService<ProdutoExcluirConsumer>();
            services.AddHostedService<ProdutoEditConsumer>();

            services.AddHostedService<VendedorAdicionarConsumer>();
            services.AddHostedService<VendedorExcluirConsumer>();
            services.AddHostedService<VendedorEditConsumer>();

            return services;
        }
        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(
                JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(options =>
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidAudience = configuration["TokenConfiguration:Audience"],
                     ValidIssuer = configuration["TokenConfiguration:Issuer"],
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(
                         Encoding.UTF8.GetBytes(configuration["Jwt:key"]))
                 });

            return services;
        }
        public static IServiceCollection AddCorsCustom(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "Mypolicy",
                                  policy =>
                                  {
                                      policy.WithOrigins("*")
                                          .AllowAnyMethod()
                                          .WithHeaders(
                                              HeaderNames.ContentType,
                                              HeaderNames.Authorization);
                                  });
            });

            return services;
        }
    }
}
