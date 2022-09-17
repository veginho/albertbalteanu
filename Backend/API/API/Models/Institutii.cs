using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Institutii
    {
        public Institutii()
        {
            Rezolvaris = new HashSet<Rezolvari>();
            Subiectes = new HashSet<Subiecte>();
        }

        public int Id { get; set; }
        public string Nume { get; set; }
        public string Adresa { get; set; }
        public string Imagine { get; set; }
        public string Descriere { get; set; }
        public string Categorie { get; set; }

        public virtual ICollection<Rezolvari> Rezolvaris { get; set; }
        public virtual ICollection<Subiecte> Subiectes { get; set; }
    }
}
