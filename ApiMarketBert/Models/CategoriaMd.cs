using System.ComponentModel.DataAnnotations;

namespace ApiMarketBert.Models
{
    public class CategoriaMd
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public int Orden { get; set; }
    }
}
