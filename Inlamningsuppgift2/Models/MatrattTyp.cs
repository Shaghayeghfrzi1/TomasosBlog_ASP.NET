using System;
using System.Collections.Generic;

#nullable disable

namespace Inlamningsuppgift2.Models
{
    public partial class MatrattTyp
    {
        public MatrattTyp()
        {
            Matratts = new HashSet<Matratt>();
        }

        public int MatrattTyp1 { get; set; }
        public string Beskrivning { get; set; }

        public virtual ICollection<Matratt> Matratts { get; set; }
    }
}
