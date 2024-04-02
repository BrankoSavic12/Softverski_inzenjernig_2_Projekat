using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEntityLib
{
    public class Pitanje
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int PredmetID { get; set; }
        [ForeignKey("PredmetID")]
        public Predmet? Predmet { get; set; }

        public int OblastID { get; set; }
        [ForeignKey("OblastID")]
        public Oblast? Oblast { get; set; }

        public string? NivoTezine { get; set; }

        public string? PitanjeSlika { get; set; }

        public string? OdgovorSlika { get; set; }

        public string? OdgovorTekst { get; set; }


        [NotMapped]
        public string? UserAnswer { get; set; }
    }
}
