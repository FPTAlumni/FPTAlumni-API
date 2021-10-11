using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.Repositories.AlumniRepo;
using UniAlumni.DataTier.Repositories.EventRepo;
using UniAlumni.DataTier.Utility.Paging;
using UniAlumni.DataTier.ViewModels.Event;

namespace UniAlumni.Business.Services.EventService
{
    public class EventSvc : IEventSvc
    {
        private readonly IEventRepository _eventRepository;
        private readonly IAlumniRepository _alumniRepository;
        private readonly IMapper _mapper;

        public EventSvc(IEventRepository eventRepository, IMapper mapper, IAlumniRepository alumniRepository)
        {
            _alumniRepository = alumniRepository;
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<IList<GetEventDetail>> GetEventPage(PagingParam<EventEnum.EventSortCriteria> paginationModel,
            SearchEventModel searchEventModel)
        {
            IQueryable<Event> queryEvent = _eventRepository.Table;

            if (searchEventModel.EventName != null)
               queryEvent = queryEvent.Where(e=> e.EventName.Contains(searchEventModel.EventName));
               
            if (searchEventModel.EventContent != null)
                queryEvent = queryEvent.Where(e=> e.EventContent.Contains(searchEventModel.EventContent));
            
            if (searchEventModel.Location != null)
                queryEvent = queryEvent.Where(e=> e.Location.Contains(searchEventModel.Location));
            
            if (searchEventModel.RegistrationStartDate != null)
                queryEvent = queryEvent.Where(e=> e.RegistrationStartDate ==  searchEventModel.RegistrationStartDate);
            
            if (searchEventModel.RegistrationEndDate != null)
                queryEvent = queryEvent.Where(e=> e.RegistrationEndDate == searchEventModel.RegistrationEndDate);
            

            if (searchEventModel.StartDate != null)
                queryEvent = queryEvent.Where(c => c.StartDate == searchEventModel.StartDate);
            if (searchEventModel.EndDate != null)
                queryEvent = queryEvent.Where(c => c.EndDate == searchEventModel.EndDate);
          
            if (searchEventModel.Status != null)
                queryEvent = queryEvent.Where(c => c.Status == (byte?) searchEventModel.Status);
            if(searchEventModel.GroupId != null)
                queryEvent = queryEvent.Where(c => c.GroupId == searchEventModel.GroupId);

            if (searchEventModel.AlumniId != null)
            {
                IQueryable<Alumnus> queryAlumni = _alumniRepository.Get(alu => alu.Id == searchEventModel.AlumniId)
                    .Include(a=> a.AlumniGroups);
                Alumnus alumnus = await queryAlumni.FirstOrDefaultAsync();
                List<int> groupJoinsId = alumnus.AlumniGroups.Select(ag=> ag.GroupId).ToList();

                queryEvent = queryEvent.Where(e => groupJoinsId.Contains((int) e.GroupId));
            }
            // Apply sort
            if(paginationModel.SortKey.ToString().Trim().Length > 0)
                queryEvent =
                    queryEvent.GetWithSorting(paginationModel.SortKey.ToString(), paginationModel.SortOrder);

            // Apply Paging
            queryEvent = queryEvent.GetWithPaging(paginationModel.Page, paginationModel.PageSize).AsQueryable();

            IQueryable<GetEventDetail> eventDetail = _mapper.ProjectTo<GetEventDetail>(queryEvent);
            return eventDetail.ToList();
        }

        public async Task<GetEventDetail> GetEventById(int id)
        {
            Event eventt = await _eventRepository.GetByIdAsync(id);
            GetEventDetail eventtDetail = _mapper.Map<GetEventDetail>(eventt);
            return eventtDetail;
        }

        public async Task<GetEventDetail> CreateEventAsync(CreateEventRequestBody requestBody)
        {
            Event eventt = _mapper.Map<Event>(requestBody);
            eventt.CreatedDate = DateTime.Now;
            await _eventRepository.InsertAsync(eventt);
            await _eventRepository.SaveChangesAsync();

            GetEventDetail eventtDetail = _mapper.Map<GetEventDetail>(eventt);
            return eventtDetail;
        }

        public async Task<GetEventDetail> UpdateEventAsync(UpdateEventRequestBody requestBody)
        {
            Event eventt = await _eventRepository.GetFirstOrDefaultAsync(evt => evt.Id == requestBody.Id);
            eventt = _mapper.Map(requestBody, eventt);
            eventt.UpdatedDate = DateTime.Now;
            _eventRepository.Update(eventt);
            await _eventRepository.SaveChangesAsync();
            GetEventDetail eventtDetail = _mapper.Map<GetEventDetail>(eventt);
            return eventtDetail;
        }

        public async Task DeleteEventAsync(int id)
        {
            Event eventt = await _eventRepository.GetFirstOrDefaultAsync(evt => evt.Id == id);
            eventt.Status = (byte?) EventEnum.EventStatus.Delete;
            await _eventRepository.SaveChangesAsync();
        }

        public async Task<int> GetTotal()
        {
            return await _eventRepository.GetAll().CountAsync();
        }
    }
}