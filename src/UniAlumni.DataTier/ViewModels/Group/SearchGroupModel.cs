using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common.Enum;

namespace UniAlumni.DataTier.ViewModels.Group
{
    public class SearchGroupModel
    {
        [DefaultValue("")]
        public string GroupName { get; set; } = "";

        public int? ParentGroupId { get; set; } = null;

        public int? MajorId { get; set; } = null;

        [DefaultValue(GroupEnum.GroupStatus.Active)]
        public GroupEnum.GroupStatus? Status { get; set; } = GroupEnum.GroupStatus.Active;
    }
}
