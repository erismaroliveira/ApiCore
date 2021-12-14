using MiniLoja.Business.Interfaces;
using MiniLoja.Business.Models;
using MiniLoja.Data.Context;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MiniLoja.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(MeuDbContext context) : base(context) { }

        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
        {
            return await Db.Enderecos.AsNoTracking()
                .FirstOrDefaultAsync(f => f.FornecedorId == fornecedorId);
        }
    }
}