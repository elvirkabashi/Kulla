using System.ComponentModel.DataAnnotations;

namespace KULLA.Models
{
    public class Fatura
    {
        [Key]
        public int NrFatures { get; set; }
        public string Blersi { get; set; }
        public string Destinacjoni { get; set; }
        public double Totali { get; set; }
        public DateTime DataKrijimit { get; set; } = DateTime.Now;
        public string Shitesi { get; set; }
        public ApplicationUser User { get; set; }
    }
}
