namespace VtrEffects.Dominio.Modelo { 
    public class NotificacaoUsuario
    {
        public int id { get; set; }

        public int usuarioid { get; set; }
        public Usuario usuario { get; set; }

        public int notificacaoid { get; set; }
        public Notificacao notificacao { get; set; }
        public bool msgLida { get; set; }
    }
}
