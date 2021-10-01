using System;
using System.Collections.Generic;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    public partial class TagNews
    {
        public int NewsId { get; set; }
        public int TagId { get; set; }

        public virtual News News { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
