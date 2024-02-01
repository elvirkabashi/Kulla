using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KULLA.Models
{
    public class Shitja
    {
        [Key]
        public int Id { get; set; }
        public int ProduktiId { get; set; }
        public virtual Produkti? Produkti { get; set; }
        public int Sasia { get; set; }
        public string? Blersi { get; set; }
        public int NrFatures { get; set; }
    }
}
