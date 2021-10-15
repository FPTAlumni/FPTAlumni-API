using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.ViewModels.Category;
using UniAlumni.DataTier.ViewModels.Group;
using UniAlumni.DataTier.ViewModels.Tag;

namespace UniAlumni.DataTier.ViewModels.News
{
    public class BaseNewsModel { 

    }
    public class NewsViewModel : BaseNewsModel
    {
        public string Banner { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public byte? Status { get; set; }
        public BaseGroupModel Group { get; set; }
        public BaseCategoryModel Category { get; set; }
        
    }
    public class NewsDetailModel : NewsViewModel
    {
        public ICollection<TagViewModel> Tags { get; set; }
    }
}
