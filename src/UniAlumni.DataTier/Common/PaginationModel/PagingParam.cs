using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using UniAlumni.DataTier.Utility.Paging;

namespace UniAlumni.DataTier.Common.PaginationModel
{
    public class PagingParam<TKey> where TKey : System.Enum
    {
        private int _page = PagingConstants.DefaultPage;

        /// <summary>
        /// Gets or sets current page number.
        /// </summary>
        public int Page
        {
            get => _page;
            set => _page = (value);
        }
        
        /// <summary>
        /// Gets or sets size of current page.
        /// </summary>
        public int PageSize { get; set; } = PagingConstants.DefaultPageSize;

      
        [Description("Parameter use for sorting result. Value: {propertyName}")]
        public TKey SortKey { get; set; }
        
        /// <summary>
        /// Gets or sets ordering criteria.
        /// </summary>
        [EnumDataType(typeof(PagingConstant.OrderCriteria))]
        [JsonConverter(typeof(PagingConstant.OrderCriteria))]
        public PagingConstant.OrderCriteria SortOrder { get; set; }
    }
}