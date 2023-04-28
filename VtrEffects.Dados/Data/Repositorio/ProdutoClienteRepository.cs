using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffectsDados.Data.Context;

namespace VtrEffectsDados.Data.Repositorio
{
    public class ProdutoClienteRepository : GenericRepository<ProdutoCliente>, IProdutoClienteRepository
    {
        private readonly ContextVTR context;
        public ProdutoClienteRepository(ContextVTR contextoBI) : base(contextoBI)
        {
            this.context = contextoBI;
        }

        public async Task<bool> VerificarCadastro(int produtoId)
        {
            var produtoCliente = context.ProdutoCliente.Where(p => p.produtoid == produtoId && p.ativo == true).FirstOrDefault();

            //Produto apto para cadastro
            if (produtoCliente == null)
                return true;

            return false;
        }

        public async Task<IList<ProdutoCliente>> GetAllByUsuario(int usuarioId)
        {
            var produtoClienteList = context.ProdutoCliente.Where(p => p.clienteid == usuarioId && p.ativo == true).ToList();
            return produtoClienteList;
        }
    }
}
