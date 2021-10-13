using System;
using System.ComponentModel;
using Newtonsoft.Json;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Utility;

namespace UniAlumni.DataTier.ViewModels.Event
{
    public class SearchEventModel
    {
        [DefaultValue("")]
        public string EventName { get; set; } = "";

        [DefaultValue("")]
        public string EventContent { get; set; } = "";

        [DefaultValue("")]
        public string Location { get; set; } = "";

        public DateTime? RegistrationStartDate { get; set; } = null;

        public DateTime? RegistrationEndDate { get; set; } = null;

        public DateTime? StartDate { get; set; } = null;

        public DateTime? EndDate { get; set; } = null;

        public int? GroupId { get; set; } = null;
        
        public int? AlumniId { get; set; } = null;

        public EventEnum.EventStatus? Status { get; set; } = null;
    }
}