using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.ViewModels.Tag;

namespace UniAlumni.DataTier.ViewModels.News
{
    public class NewsUpdateRequest
    {
        public int Id { get; set; }
        public string Banner { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? GroupId { get; set; }
        public int? CategoryId { get; set; }
        public int? Status { get; set; }
        public ICollection<string> TagNames { get; set; }
    }
}
