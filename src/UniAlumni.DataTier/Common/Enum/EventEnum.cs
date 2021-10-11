namespace UniAlumni.DataTier.Common.Enum
{
    public class EventEnum
    {
        public enum EventStatus
        {
            NotStart = 0,
            
            RegistrationStart = 1,
            
            RegistrationEnd = 2,
            
            InProgress = 3,
            
            End = 4,
            
            Delete = 5
        }
        
        public enum EventSortCriteria
        {
            EventName = 0,
            
            Location = 1,
            
            RegistrationStartDate = 2,
            
            RegistrationEndDate,
            
            StartDate,
            
            EndDate
        }
    }
}