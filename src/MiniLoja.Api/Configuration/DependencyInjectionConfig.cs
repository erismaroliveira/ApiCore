using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MiniLoja.Api.Extensions;
using MiniLoja.Business.Interfaces;
using MiniLoja.Business.Notificacoes;
using MiniLoja.Business.Services;
using MiniLoja.Data.Context;
using MiniLoja.Data.Repository;

namespace MiniLoja.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MeuDbContext>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IProdutoService, ProdutoService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            return services;
        }
    }
}
