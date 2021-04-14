﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Inlamningsuppgift2.Models
{
    public partial class Produkt
    {
        public Produkt()
        {
            MatrattProdukts = new HashSet<MatrattProdukt>();
        }

        public int ProduktId { get; set; }
        public string ProduktNamn { get; set; }

        public virtual ICollection<MatrattProdukt> MatrattProdukts { get; set; }
    }
}
