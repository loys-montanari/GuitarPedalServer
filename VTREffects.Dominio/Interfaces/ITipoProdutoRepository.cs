﻿
using VtrEffects.Dominio.Modelo;

namespace VtrEffects.Dominio.Interfaces
{
    public interface ITipoProdutoRepository : IGenericRepository<TipoProduto>
    {
        Task<List<int>> GetAllIds();
    }
}
