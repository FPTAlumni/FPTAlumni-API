using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common.Enum;

namespace UniAlumni.DataTier.ViewModels.Major
{
    public class SearchMajorModel
    {
        [DefaultValue("")]
        public string Name { get; set; } = "";

        [DefaultValue(MajorEnum.MajorStatus.Active)]
        public MajorEnum.MajorStatus? Status { get; set; } = MajorEnum.MajorStatus.Active;
    }
}
