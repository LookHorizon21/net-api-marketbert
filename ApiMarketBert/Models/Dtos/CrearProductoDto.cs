namespace ApiMarketBert.Models.Dtos
{
    public class CrearProductoDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string RutaImagen { get; set; }

        public int categoriaId { get; set; }
    }
}
