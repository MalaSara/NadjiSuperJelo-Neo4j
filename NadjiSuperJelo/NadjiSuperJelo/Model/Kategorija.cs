using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadjiSuperJelo.Model
{
    public class Kategorija
    {
        public String idKategorija { get; set; }
        public String naziv { get; set; }
        public List<Jelo> jela { get; set; }
    }
}
