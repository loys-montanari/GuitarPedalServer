namespace VtrEffects.Dominio.Modelo
{
    public class Usuario
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public string? Foto { get; set; }
        public string? Bio { get; set; }

        public string? InstrumentoPrincipal { get; set; }
    }
}
