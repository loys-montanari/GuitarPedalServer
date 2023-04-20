﻿using VtrEffects.Dominio.Interfaces;
using VtrEffects.Dominio.Modelo;
using VtrEffectsDados.Data.Context;

namespace VtrEffectsDados.Data.Repositorio
{
    public class ComentarioRepository : GenericRepository<Comentario>, IComentarioRepository
    {
        public ComentarioRepository(ContextVTR contextoBI) : base(contextoBI)
        {

        }
    }
}
