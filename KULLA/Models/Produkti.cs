using System.ComponentModel.DataAnnotations;

namespace KULLA.Models
{
    public class Produkti
    {
        [Key]
        public int ProduktiId { get; set; }
        public string Emri { get; set; }
        public double Cmimi { get; set; }
        public string Category { get; set; }
        public virtual ICollection<Shitja>? Shitjet { get; set; }
    }
}
