using System;

namespace UniAlumni.DataTier.ViewModels.AlumniGroup
{
    public class GetAlumniGroupDetail
    {
        public int AlumniId { get; set; }
        public int GroupId { get; set; }
        public DateTime? RegisteredDate { get; set; }
        public byte? Status { get; set; }
    }
}