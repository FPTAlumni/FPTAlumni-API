using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common.Enum;

namespace UniAlumni.DataTier.ViewModels.News
{
    public class SearchNewsModel
    {
        public string Title { get; set; } = "";
        public int? GroupId { get; set; } = null;
        public int? CategoryId { get; set; } = null;
        public int? TagId { get; set; } = null;

        [DefaultValue(NewsEnum.NewsStatus.Active)]
        public NewsEnum.NewsStatus? Status { get; set; } = null;
    }
}
