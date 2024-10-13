using System.ComponentModel.DataAnnotations.Schema;

namespace Task_Autofficina.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        public int ClienteID { get; set; }
        public string Codice { get; set; } = null!;
        public string Nome { get; set; } = null!;
        public string Cognome { get; set; } = null!;
        public string? Indirizzo { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public List<Veicolo> Veicolo { get; set;} = new List<Veicolo>();


    }
}
