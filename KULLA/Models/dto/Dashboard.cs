namespace KULLA.Models.dto
{
    public class Dashboard
    {
        public double ShitjaTotale { get; set; }
        public double ShitjaBesnikit { get; set; }
        public double ShitjaShkelezenit { get; set; }
        public List<Fatura> Faturat { get; set; }
    }
}
