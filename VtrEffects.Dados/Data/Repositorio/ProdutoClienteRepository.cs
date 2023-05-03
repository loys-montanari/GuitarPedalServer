using Microsoft.EntityFrameworkCore;
using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffectsDados.Data.Context;

namespace VtrEffectsDados.Data.Repositorio
{
    public class ProdutoClienteRepository : GenericRepository<ProdutoCliente>, IProdutoClienteRepository
    {
        private readonly ContextVTR context;
        private IProdutoRepository produtoRepository;

        public ProdutoClienteRepository(ContextVTR contextoBI, IProdutoRepository produtoRepository) : base(contextoBI)
        {
            this.context = contextoBI;
            this.produtoRepository = produtoRepository;
        }

        public async Task<bool> VerificarCadastro(string serial)
        {
            var produto = produtoRepository.GetBySerial(serial).Result;
            if (produto == null)
                return false;

            var produtoCliente = context.ProdutoCliente.Where(p => p.produtoid == produto.id && p.ativo == true).FirstOrDefault();
            //Produto apto para cadastro
            if (produtoCliente == null)
                return true;

            return false;
        }

        public async Task<IList<ProdutoCliente>> GetAllByUsuario(int usuarioId)
        {
            //var produtoClienteList = context.ProdutoCliente.Include(p => p.produto.id).Include(p => p.produto.tipoProduto.id).Where(p => p.usuarioid == usuarioId && p.ativo == true).ToList();
            var produtoClienteList = context.ProdutoCliente.Where(p => p.usuarioid == usuarioId && p.ativo == true).ToList();
            return produtoClienteList;
        }
    }
}
