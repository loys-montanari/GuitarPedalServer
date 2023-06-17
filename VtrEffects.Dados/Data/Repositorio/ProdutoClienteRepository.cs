using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VtrEffects.Dominio.DTO.Generic;
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

        public async Task<ProdutoCliente> GetBySerial(string serial)
        {
            var produto = produtoRepository.GetBySerial(serial).Result;
            if (produto == null)
                return null;

            var produtoCliente = context.ProdutoCliente.Where(p => p.produtoid == produto.id && p.ativo == true).FirstOrDefault();

            return produtoCliente;
        }
        public async Task<IList<ProdutoCliente>> GetAllByUsuario(int usuarioId)
        {
            var produtoClienteList = context.ProdutoCliente.Include(p => p.produto).ThenInclude(prod => prod.tipoProduto).Where(p => p.usuarioid == usuarioId && p.ativo == true).ToList();
            //var produtoClienteList = context.ProdutoCliente.Where(p => p.usuarioid == usuarioId && p.ativo == true).ToList();
            return produtoClienteList;
        }

        public async Task<IList<Generic3<string, int, DateTime?>>> GetAllSerialTipoProdutoByUsuario(int usuarioId)
        {
            var produtoClienteList = context.ProdutoCliente.Include(p => p.produto).ThenInclude(prod => prod.tipoProduto).Where(p => p.usuarioid == usuarioId && p.ativo == true).Select(prodcliente => new Generic3<string, int, DateTime?> { prop1 = prodcliente.produto.serial, prop2 = prodcliente.produto.tipoProduto.id, prop3 = prodcliente.dataCompra }).ToList();
            return produtoClienteList;
        }

        public async Task<string?> GetPrazoGarantia(string serial)
        {
            var produto = produtoRepository.GetBySerial(serial).Result;
            if (produto == null)
                return null;

            var produtoClienteList = context.ProdutoCliente.Where(p => p.produtoid == produto.id).ToList();
            var owner = produtoClienteList.Where(p => p.ativo == true).FirstOrDefault();
            if (owner == null)
                return null;

            if (owner.primeiroComprador == true) //Caso a pessoa que está com o produto seja o primeiro comprador, a garantia será vitalícia.
                return "Vitalícia";

            var prazo = produtoClienteList.Min(p => p.dataCompra).Value; //Caso a pessoa que está com o produto NÃO seja o primeiro comprador, a garantia será a data de aquisição do primeiro comprador + 1 ano.
            return prazo.AddYears(1).ToShortDateString();
        }

        public static DateTime GetRandomDate()
        {
            Random generator = new Random();

            DateTime start = new DateTime(2023, 05, 01);
            int limite = (DateTime.Today - start).Days;
            return start.AddDays(generator.Next(limite));
        }
    }
}
