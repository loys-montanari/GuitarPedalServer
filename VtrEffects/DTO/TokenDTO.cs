namespace VtrEffects.DTO
{
    public class TokenDTO
    {
        public bool Autenticado { get; set; }
        public DateTime Expiracao { get; set; }
        public string Token { get; set; }
        public string Mensagem { get; set; }
    }
}
