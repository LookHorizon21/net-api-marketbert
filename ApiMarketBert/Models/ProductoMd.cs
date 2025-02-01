using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiMarketBert.Models
{
    public class ProductoMd
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string RutaImagen { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }

        //Relación con Categoria
        public int categoriaId { get; set; }
        [ForeignKey("categoriaId")]
        public CategoriaMd Categoria { get; set; }
    }
}
