namespace VtrEffects.Dominio.Modelo { 
    public class NotificacaoUsuario
    {
        public int Id { get; set; }

        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }

        public int IdNotificacao { get; set; }
        public Notificacao notificacao { get; set; }
        public bool MsgLida { get; set; }
    }
}
