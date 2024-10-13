using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace Task_Autofficina.Models
{
    [Table("Veicolo")]
    public class Veicolo
    {
        public int VeicoloID { get; set; }
        public string Codice { get; set; } = null!;
        public string Targa { get; set; } = null!;
        public string Modello { get; set; } = null!;
        public string Marca { get; set; } = null!;
        public DateOnly? AnnoImmatricolazione { get; set; } = null!;

        [Column(TypeName = "decimal(8, 2)")]
        public decimal? PrezzoIntervento { get; set; }
        public string StatoIntervento { get; set; } = null!;
        public DateOnly? DataIngresso { get; set; } = null!;
        public DateOnly? DataUscita { get; set; } = null!;
        public int ClienteRif { get; set; }

        //public  Cliente ClienteRifNavigator { get; set; } = null!; //Non ho capito perchè lo ha usato in ferramenta

    }
}
