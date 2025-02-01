namespace ApiMarketBert.Models.Dtos
{
    public class UsuarioLoginRespuestaDto
    {
        public UsuarioMd Usuario { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
