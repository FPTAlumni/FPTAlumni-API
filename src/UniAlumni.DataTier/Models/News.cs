using System;
using System.Collections.Generic;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    public partial class News
    {
        public News()
        {
            TagNews = new HashSet<TagNews>();
        }

        public int Id { get; set; }
        public string Banner { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public byte? Status { get; set; }
        public int? GroupId { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Group Group { get; set; }
        public virtual ICollection<TagNews> TagNews { get; set; }
    }
}
