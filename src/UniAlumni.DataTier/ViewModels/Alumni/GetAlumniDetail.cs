﻿using System;
using Newtonsoft.Json;
using UniAlumni.DataTier.Utility;
using UniAlumni.DataTier.ViewModels.Class;
using UniAlumni.DataTier.ViewModels.Company;
using UniAlumni.DataTier.ViewModels.Major;

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
        
        [JsonProperty("dob")]
        [JsonConverter(typeof(DateFormatConverter), "dd/MM/yyyy")]
        public DateTime DoB { get; set; }
        public string Job { get; set; }
        public string AboutMe { get; set; }
        
        [JsonConverter(typeof(DateFormatConverter), "dd/MM/yyyy HH:mm")]
        public DateTime? CreatedDate { get; set; }
        public string Status { get; set; }
        public GetCompanyDetail Company { get; set; }
        public MajorViewModel Major { get; set; }
        
        public GetClassDetail Class { get; set; }
    }
    public class AlumniGroupAlumniModel : BaseAlumniModel
    {
        public string Email { get; set; }
    }
}