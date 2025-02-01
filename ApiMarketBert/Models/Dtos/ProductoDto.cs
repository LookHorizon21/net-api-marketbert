using System.ComponentModel.DataAnnotations.Schema;

namespace ApiMarketBert.Models.Dtos
{
    public class ProductoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string RutaImagen { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }

        public int categoriaId { get; set; }
    }
}
