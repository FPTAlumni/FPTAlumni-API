using UniAlumni.DataTier.Common.Enum;

namespace UniAlumni.DataTier.ViewModels.Major
{
    public class SearchMajorModel
    {
        public string Name { get; set; } = "";

        public int? ClassId { get; set; } = null;

        public MajorEnum.MajorStatus? Status { get; set; } = null;
    }
}
