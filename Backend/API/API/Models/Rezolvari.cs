using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Rezolvari
    {
        public int Id { get; set; }
        public int IdInstitutie { get; set; }
        public int An { get; set; }
        public byte[] Rezolvare { get; set; }
        public string Nume { get; set; }
        public string FileType { get; set; }

        public virtual Institutii IdInstitutieNavigation { get; set; }
    }
}
