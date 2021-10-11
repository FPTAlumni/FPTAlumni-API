using System.Collections.Generic;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.ViewModels.Event;

namespace UniAlumni.Business.Services.EventService
{
    /// <summary>
    /// Interface for service layer of Event in Business module.
    /// </summary>
    public interface IEventSvc
    {
        /// <summary>
        /// Get list of all Event.
        /// </summary>
        /// <param name="paginationModel">An object contains paging criteria</param>
        /// <param name="searchEventModel">An object contains search and filter criteria</param>
        /// <returns>List of Event.</returns>
        Task<IList<GetEventDetail>> GetEventPage(PagingParam<EventEnum.EventSortCriteria> paginationModel,
            SearchEventModel searchEventModel);
        
        /// <summary>
        /// Get detail information of a Event.
        /// </summary>
        /// <param name="id">Id of Event.</param>
        /// <returns>A Event Detail.</returns>>
        public Task<GetEventDetail> GetEventById(int id);
        
        /// <summary>
        /// Create Event.
        /// </summary>
        /// <param name="requestBody">Model create Class request of Event.</param>
        /// <returns>A Event detail.</returns>>
        public Task<GetEventDetail> CreateEventAsync(CreateEventRequestBody requestBody);

        /// <summary>
        /// Update Event.
        /// </summary>
        /// <param name="requestBody">Model Update Class request of Event.</param>
        /// <returns>A Event Detail.</returns>>
        public Task<GetEventDetail> UpdateEventAsync(UpdateEventRequestBody requestBody);
        
        /// <summary>
        /// Delete Event - Change Status to Inactive
        /// </summary>
        /// <param name="id">ID of Event</param>
        /// <returns></returns>
        public Task DeleteEventAsync(int id);
        
        /// <summary>
        /// Get total of Event
        /// </summary>
        /// <returns>Total of Event</returns>
        public Task<int> GetTotal();
    }
}