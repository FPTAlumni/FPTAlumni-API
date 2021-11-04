using System;

namespace UniAlumni.DataTier.ViewModels.Event
{
    public class GetEventDetail : CreateEventRequestBody
    {
        public int Id { get; set; }
        
        public bool? InEvent { get; set; }

        public string GroupName { get; set; }
        
        public DateTime? CreatedDate { get; set; }
    }
}