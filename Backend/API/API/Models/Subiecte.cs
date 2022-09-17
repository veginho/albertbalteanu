using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Subiecte
    {
        public int Id { get; set; }
        public int IdInstitutie { get; set; }
        public int An { get; set; }
        public byte[] Subiect { get; set; }
        public string Nume { get; set; }
        public string FileType { get; set; }

        public virtual Institutii IdInstitutieNavigation { get; set; }
    }
}
