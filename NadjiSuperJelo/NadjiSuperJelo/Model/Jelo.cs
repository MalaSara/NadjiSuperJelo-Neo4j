using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadjiSuperJelo.Model
{
    public class Jelo
    {
        public String idJelo { get; set; }

        public String nazivJela { get; set; }
        public Double ocenaJela { get; set; }
        public Autor autor { get; set; }

        public String nacinPripreme { get; set; }

        public int brojOcenaJela { get; set; }
        public Kategorija kategorija { get; set; }
        public List<Sastojak> Sastojci { get; set; }

    }
}
