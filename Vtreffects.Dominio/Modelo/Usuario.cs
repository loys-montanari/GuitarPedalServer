namespace VtrEffects.Dominio.Modelo
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Bio { get; set; }

        public string? InstrumentoPrincipal { get; set; }
        public string? Foto { get; set; }
    }
}
