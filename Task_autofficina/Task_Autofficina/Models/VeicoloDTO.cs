using System.Data.SqlTypes;

namespace Task_Autofficina.Models
{
    public class VeicoloDTO
    {
        public string? Cod { get; set; }
        public string? Tar { get; set; }
        public string? Mod { get; set; }
        public string? Mar { get; set; }
        public DateOnly? AnnoImm { get; set; }
        public decimal? Prez { get; set; }
        public string? StatoInt { get; set; }
        public DateOnly? DataIng { get; set; }
        public DateOnly? DataUsc { get; set; }
        public ClienteDTO? Clie { get; set; }


    }
}
