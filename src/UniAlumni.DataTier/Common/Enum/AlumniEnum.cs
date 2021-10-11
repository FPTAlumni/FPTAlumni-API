namespace UniAlumni.DataTier.Common.Enum
{
    public class AlumniEnum
    {
        public enum AlumniStatus
        {
            /// <summary>
            /// Status for reject login
            /// </summary>
            Reject = 0,
            
            /// <summary>
            /// Status for waiting Admin accept
            /// </summary>
            Pending = 1,
            
            /// <summary>
            /// Status for Alumni
            /// </summary>
            Active = 2,
            
            /// <summary>
            /// Status for block alumni
            /// </summary>
            Deactive = 3,
        }
        
        
        public enum AlumniSortCriteria
        {   
            /// <summary>
            /// Fullname attr
            /// </summary>
            FullName,
            
            /// <summary>
            /// CreateDate attr
            /// </summary>
            CreatedDate,
            
            /// <summary>
            /// Status attr
            /// </summary>
            Status
        }
    }
}