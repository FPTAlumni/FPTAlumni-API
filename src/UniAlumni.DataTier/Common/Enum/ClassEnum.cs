namespace UniAlumni.DataTier.Common.Enum
{
    public class ClassEnum
    {
        public enum ClassStatus
        {
            /// <summary>
            /// Status for deleted
            /// </summary>
            Inactive = 0,
            
            /// <summary>
            /// Status for active
            /// </summary>
            Active = 1,
        }
        public enum ClassSortCriteria
        {
            ClassOf,
            StartYear
        }
    }
}