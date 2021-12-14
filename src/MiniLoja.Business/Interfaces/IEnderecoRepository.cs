using System;
using System.Threading.Tasks;
using MiniLoja.Business.Models;

namespace MiniLoja.Business.Interfaces
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {
        Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId);
    }
}