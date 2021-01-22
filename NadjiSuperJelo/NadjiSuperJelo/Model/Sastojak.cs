using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadjiSuperJelo.Model
{
    public class Sastojak
    {
        public String idSastojak { get; set; }
        public String naziv { get; set; }

        public int rateHranljivosti { get; set; }
        public List<Jelo> jelo { get; set; }
    }
}
