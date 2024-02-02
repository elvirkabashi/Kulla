using System.ComponentModel.DataAnnotations;

namespace KULLA.Models.dto
{
    public class KrijoShitjeDto
    {
        [RequiredIfNotEmpty(ErrorMessage = "Duhet te selektoni nje produkt!")]
        public int ProduktiId { get; set; }
        [SasiaNotZero(ErrorMessage = "Sasia nuk mund te ket vler zero!")]
        public int Sasia { get; set; }
        public string? Blersi { get; set; }
        public string? Destinacjoni { get; set; }
        public List<Shitja>? Shitja { get; set; }
    }
}
