using System;
using System.Collections.Generic;

namespace ProjetoMySQL.Models
{
    public partial class Candidato
    {
        public Candidato()
        {
            Votacaos = new HashSet<Votacao>();
        }

        public int Id { get; set; }
        public int Numero { get; set; }
        public string Nome { get; set; } = null!;
        public string Partido { get; set; } = null!;

        public virtual ICollection<Votacao> Votacaos { get; set; }
    }
}
