using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadjiSuperJelo.Model
{
    public class Autor
    {
        public String idAutor { get; set; }
        public List<Jelo> jelaCijiJeAutor { get; set; }
        public Double srednjaOcenaAutora { get; set; }
        public String ime { get; set; }
        public String prezime { get; set; }
        public int brojJela { get; set; }

        public int brojOcena { get; set; }
    }
}
