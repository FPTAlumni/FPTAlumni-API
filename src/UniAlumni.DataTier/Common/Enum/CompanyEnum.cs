namespace UniAlumni.DataTier.Common.Enum
{
    public class CompanyEnum
    {
        public enum CompanyStatus
        {
            /// <summary>
            /// Status of company when inactive
            /// </summary>
            Inactive,
            
            /// <summary>
            /// Status of company available
            /// </summary>
            Active
        }

        public enum CompanySortCriteria
        {
            /// <summary>
            /// CompanyName attr
            /// </summary>
            CompanyName,
            
            /// <summary>
            /// Location Attr
            /// </summary>
            Location,
            
            /// <summary>
            /// Business Attr
            /// </summary>
            Business
        }
    }
}