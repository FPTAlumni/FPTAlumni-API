﻿using System.ComponentModel.DataAnnotations;

namespace UniAlumni.DataTier.ViewModels.Class
{
    public class UpdateClassRequestBody 
    {
        [Required]
        public int Id { get; set;}
        public string ClassOf { get; set; }
        public int? StartYear { get; set; }
    }
}