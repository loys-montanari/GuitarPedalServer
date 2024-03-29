﻿using System.Security.Cryptography.X509Certificates;
using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffectsDados.Data.Context;

namespace VtrEffectsDados.Data.Repositorio
{
    public class TipoProdutoImagemRepository : GenericRepository<TipoProdutoImagem>, ITipoProdutoImagemRepository
    {
        private readonly ContextVTR context;
        public TipoProdutoImagemRepository(ContextVTR contextoBI) : base(contextoBI)
        {
            context = contextoBI;
        }

        public async Task<IList<TipoProdutoImagem>> GetAllByTipoProduto(int tipoProdutoId)
        {
            return context.TipoProdutoImagem.Where(t => t.tipoProdutoId == tipoProdutoId).OrderBy(t => t.ordem).ToList();
        }

        public async Task<TipoProdutoImagem?> GetByTipoProdutoAndTipoImagem(int tipoProdutoId, int tipoImagem)
        {
            return context.TipoProdutoImagem.Where(t => t.tipoProdutoId == tipoProdutoId && t.ordem == tipoImagem).FirstOrDefault();
        }
    }
}
