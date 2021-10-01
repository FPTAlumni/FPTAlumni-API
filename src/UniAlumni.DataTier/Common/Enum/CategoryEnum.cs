namespace UniAlumni.DataTier.Common.Enum
{
    public class CategoryEnum
    {
        public enum CategoryStatus
        {
            /// <summary>
            /// Status for deleted category
            /// </summary>
            Inactive,
            
            /// <summary>
            /// Status when create category
            /// </summary>
            Active
        }
        
        public enum CategorySortCriteria
        {
            Categoryname
        }
    }
}