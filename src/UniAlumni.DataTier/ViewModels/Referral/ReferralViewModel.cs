using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.ViewModels.Alumni;
using UniAlumni.DataTier.ViewModels.Major;
using UniAlumni.DataTier.ViewModels.University;
using UniAlumni.DataTier.ViewModels.UniversityMajor;
using UniAlumni.DataTier.ViewModels.Voucher;

namespace UniAlumni.DataTier.ViewModels.Referral
{
    public class ReferralViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string VoucherCode { get; set; }
        public DateTime? CreatedDate { get; set; }
        public byte? Status { get; set; }
        public string StringStatus { get; set; }
        public BaseAlumniModel Nominator { get; set; }
        public BaseVoucherModel Voucher { get; set; }
        public BaseMajorModel Major { get; set; }
        public BaseUniversityModel University { get; set; }
        public string ParentName { get; set; }
        public string ParentPhone { get; set; }
        public string HighSchoolName { get; set; }
    }
}
