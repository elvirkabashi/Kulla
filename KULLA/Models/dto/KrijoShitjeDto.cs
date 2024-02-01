namespace KULLA.Models.dto
{
    public class KrijoShitjeDto
    {
        public int ProduktiId { get; set; }
        public int Sasia { get; set; }
        public string? Blersi { get; set; }
        public string? Destinacjoni { get; set; }
        public List<Shitja>? Shitja { get; set; }
    }
}
