namespace VtrEffects.Dominio.Modelo
{
    public class Usuario
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public string? foto { get; set; }
        public string? bio { get; set; }

        public string? instrumentoPrincipal { get; set; }
    }
}
