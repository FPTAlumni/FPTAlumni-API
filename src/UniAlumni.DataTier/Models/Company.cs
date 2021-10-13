using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace UniAlumni.DataTier.Models
{
    [Table("Company")]
    public partial class Company
    {
        public Company()
        {
            Alumni = new HashSet<Alumnus>();
            Recruitments = new HashSet<Recruitment>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(70)]
        public string CompanyName { get; set; }
        [Required]
        [StringLength(150)]
        public string Location { get; set; }
        [Required]
        [StringLength(100)]
        public string Business { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public int? CreateBy { get; set; }
        public byte? Status { get; set; }
        [Column(TypeName = "text")]
        public string Image { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [InverseProperty(nameof(Alumnus.Company))]
        public virtual ICollection<Alumnus> Alumni { get; set; }
        [InverseProperty(nameof(Recruitment.Company))]
        public virtual ICollection<Recruitment> Recruitments { get; set; }
    }
}
