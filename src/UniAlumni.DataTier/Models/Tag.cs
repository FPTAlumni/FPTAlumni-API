using System;
using System.Collections.Generic;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    public partial class Tag
    {
        public Tag()
        {
            TagNews = new HashSet<TagNews>();
        }

        public int Id { get; set; }
        public string Tagname { get; set; }
        public byte? Status { get; set; }

        public virtual ICollection<TagNews> TagNews { get; set; }
    }
}
