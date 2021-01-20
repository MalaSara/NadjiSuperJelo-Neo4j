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
        public Double ocenaJela { get; set; }
        public Autor autor { get; set; }

        public String nacinPripreme { get; set; }

        public Kategorija pripadaKategoriji(Kategorija kategorija)
        {
            return kategorija;
        }
        
        //
        public List<Sastojak> Sastojci { get; set; }

    }
}
