using Microsoft.OpenApi.Models;
using Ms_Compras.Config;
using Ms_Compras.MigadorDeDados;
using Ms_Compras.Services.Interfaces;
using Ms_Compras.Services.Service;
using Ms_Compras.SincronizadosDeDados;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "api", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Header de autoriza��o JWT usando o esquema Bearer.\r\n\r\nInforme 'Bearer'[espa�o] e o seu token.\r\n\r\nExamplo: \'Bearer 12345abcdef\'",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
       {
          new OpenApiSecurityScheme
          {
             Reference = new OpenApiReference
             {
                 Type = ReferenceType.SecurityScheme,
                 Id = "Bearer"
             }
          },
          new string[] {}
       }
    });
});

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddContext();
builder.Services.AddRepository();
builder.Services.AddServices();
builder.Services.AddProducerRabbitMq();
builder.Services.AddConsumerRabbitMq();
builder.Services.AddAuth(builder.Configuration);
builder.Services.AddCorsCustom();

builder.Services.AddHttpClient<IPermissaoService, PermissaoService>(s => s.BaseAddress = new Uri(builder.Configuration["ServiceUrls:Permissao"]));

builder.Services.AddSingleton<ISincronizador, Sincronizador>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.UseCors("Mypolicy");

app.MapControllers();

app.Run();
