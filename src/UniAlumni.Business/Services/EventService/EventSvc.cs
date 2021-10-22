using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.Exception;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.Repositories.AlumniRepo;
using UniAlumni.DataTier.Repositories.EventRegistrationRepo;
using UniAlumni.DataTier.Repositories.EventRepo;
using UniAlumni.DataTier.Utility.Paging;
using UniAlumni.DataTier.ViewModels.Event;

namespace UniAlumni.Business.Services.EventService
{
    public class EventSvc : IEventSvc
    {
        private readonly IEventRepository _eventRepository;
        private readonly IEventRegistrationRepository _eventRegistrationRepository;
        private readonly IAlumniRepository _alumniRepository;
        private readonly IMapper _mapper;

        public EventSvc(IEventRepository eventRepository, 
            IMapper mapper, 
            IAlumniRepository alumniRepository,
            IEventRegistrationRepository eventRegistrationRepository)
        {
            _eventRegistrationRepository = eventRegistrationRepository;
            _alumniRepository = alumniRepository;
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<IList<GetEventDetail>> GetEventPage(PagingParam<EventEnum.EventSortCriteria> paginationModel,
            SearchEventModel searchEventModel, int? alumniId)
        {
            
            
            IQueryable<Event> queryEvent = _eventRepository.Table.
                    Include(e=>e.Group);

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
            
            // Apply Status 
            queryEvent = queryEvent.Where(c => c.Status != (byte?) EventEnum.EventStatus.Delete);

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
            List<int> idEventJoined = null;
            if (alumniId != null)
            {
                IQueryable<EventRegistration> eventRegistrations =
                    _eventRegistrationRepository.Get(er => er.AlumniId == alumniId && 
                                                           er.Status == (byte?) EventRegistrationEnum.EventRegistrationStatus.Joined);
                idEventJoined = eventRegistrations.Select(er => er.EventId).ToList();
            }

            List<GetEventDetail> eventDetailList = eventDetail.ToList();
            if (alumniId != null && idEventJoined != null)
            {
                foreach (var eventD in eventDetailList)
                {
                    eventD.InEvent = idEventJoined.Contains(eventD.Id);
                }
            }
            return eventDetailList;
        }

        public async Task<GetEventDetail> GetEventById(int id)
        {
            Event eventt = await _eventRepository.GetByIdAsync(id);
            if (eventt == null) return null;
            GetEventDetail eventtDetail = _mapper.Map<GetEventDetail>(eventt);
            return eventtDetail;
        }

        public async Task<GetEventDetail> CreateEventAsync(CreateEventRequestBody requestBody)
        {
            Event eventt = _mapper.Map<Event>(requestBody);
            eventt.CreatedDate = DateTime.Now;
            eventt.Status = (byte?) EventEnum.EventStatus.NotStart;
            await _eventRepository.InsertAsync(eventt);
            await _eventRepository.SaveChangesAsync();

            GetEventDetail eventtDetail = _mapper.Map<GetEventDetail>(eventt);
            return eventtDetail;
        }

        public async Task<GetEventDetail> UpdateEventAsync(UpdateEventRequestBody requestBody)
        {
            Event eventt = await _eventRepository.GetFirstOrDefaultAsync(evt => evt.Id == requestBody.Id);
            if (eventt == null) throw new MyHttpException(StatusCodes.Status404NotFound, "Event not found");
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
            if (eventt == null) throw new MyHttpException(StatusCodes.Status404NotFound, "Event not found");

            eventt.Status = (byte?) EventEnum.EventStatus.Delete;
            await _eventRepository.SaveChangesAsync();
        }

        public async Task<int> GetTotal()
        {
            return await _eventRepository.GetAll().CountAsync();
        }
    }
}