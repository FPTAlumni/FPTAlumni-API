

namespace UniAlumni.DataTier.Common.Enum
{
    public class GroupEnum
    {
        public enum GroupStatus
        {
            Inactive,
            Active
        }
        public enum GroupSortCriteria
        {
            /// <summary>
            /// Fullname attr
            /// </summary>
            GroupName,

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
