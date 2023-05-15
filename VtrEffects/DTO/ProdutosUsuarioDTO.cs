namespace VtrEffects.DTO
{
    public class ProdutosUsuarioDTO
    {
        public int usuarioId { get; set; }
        public List<ProdutoDTO> produtos { get; set; } = new List<ProdutoDTO>();
    }
}
