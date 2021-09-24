using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    [Table("FPTStudentInfo")]
    public partial class FptstudentInfo
    {
        [Key]
        public int Id { get; set; }
        [StringLength(10)]
        public string StudentId { get; set; }
        [StringLength(35)]
        public string EduMail { get; set; }
        [StringLength(50)]
        public string Major { get; set; }
        [StringLength(3)]
        public string Class { get; set; }
    }
}
