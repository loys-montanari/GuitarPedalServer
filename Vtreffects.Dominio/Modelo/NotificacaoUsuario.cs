namespace VtrEffects.Dominio.Modelo { 
    public class NotificacaoUsuario
    {
        public int id { get; set; }

        public int idUsuario { get; set; }
        public Usuario usuario { get; set; }

        public int idNotificacao { get; set; }
        public Notificacao notificacao { get; set; }
        public bool msgLida { get; set; }
    }
}
