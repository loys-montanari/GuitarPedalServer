﻿
using VtrEffects.Dominio.Modelo;

namespace VtrEffects.Dominio.Interfaces
{
    public interface IAnexoRepository : IGenericRepository<AnexoPostagem>
    {

        Task<List<AnexoPostagem>> getAllByPost(int idpost);
    }
}
