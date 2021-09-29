namespace UniAlumni.DataTier.Common.Enum
{
    public class AlumniEnum
    {
        public enum AlumniStatus
        {
            /// <summary>
            /// Status for first login
            /// </summary>
            Unregistered = (byte)0,
            
            /// <summary>
            /// Status for waiting Admin accept
            /// </summary>
            Pending = (byte)1,
            
            /// <summary>
            /// Status for Alumni
            /// </summary>
            Active = (byte)2,
            
            /// <summary>
            /// Status for block alumni
            /// </summary>
            Deactive = (byte)3,
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