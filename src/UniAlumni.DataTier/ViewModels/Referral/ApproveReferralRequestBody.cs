using System.ComponentModel.DataAnnotations;
using UniAlumni.DataTier.Common.Enum;

namespace UniAlumni.DataTier.ViewModels.Referral
{
    public class ApproveReferralRequestBody
    {
        [Required]
        public int Id { get; set; }

        public ReferralEnum.ReferralStatus Status { get; set; }
    }
}