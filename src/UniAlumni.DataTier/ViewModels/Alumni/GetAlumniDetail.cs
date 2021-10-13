using System;

namespace UniAlumni.DataTier.ViewModels.Alumni
{
    public class BaseAlumniModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }
    public class GetAlumniDetail: BaseAlumniModel
    {
        
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Uid { get; set; }
        public DateTime DoB { get; set; }
        public string Job { get; set; }
        public string AboutMe { get; set; }
        public DateTime? CreatedDate { get; set; }
        public byte? Status { get; set; }
        public int? UniversityId { get; set; }
        public int? CompanyId { get; set; }
    }
    public class AlumniGroupAlumniModel : BaseAlumniModel
    {
        public string Email { get; set; }
    }
}