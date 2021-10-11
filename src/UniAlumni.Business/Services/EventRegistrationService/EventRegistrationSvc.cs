using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.Repositories.AlumniRepo;
using UniAlumni.DataTier.Repositories.EventRegistrationRepo;
using UniAlumni.DataTier.Repositories.EventRepo;

namespace UniAlumni.Business.Services.EventRegistrationService
{
    public class EventRegistrationSvc : IEventRegistrationSvc
    {
        private readonly IAlumniRepository _alumniRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IEventRegistrationRepository _eventRegistrationRepository;

        public EventRegistrationSvc(IAlumniRepository alumniRepository, IEventRepository eventRepository, IEventRegistrationRepository eventRegistrationRepository)
        {
            _alumniRepository = alumniRepository;
            _eventRepository = eventRepository;
            _eventRegistrationRepository = eventRegistrationRepository;
        }

        public async Task JoinEvent(int alumniId, int eventId)
        {
            IQueryable<EventRegistration> queryEventRegistration = _eventRegistrationRepository.Table.Where(er
                => er.AlumniId == alumniId && er.EventId == eventId);
            EventRegistration eventRegistration = await queryEventRegistration.FirstOrDefaultAsync();
            if (eventRegistration == null)
            {
                IQueryable<Alumnus> queryAlumni = _alumniRepository.Table.Where(alu => alu.Id == alumniId);
                Alumnus alumnus = await queryAlumni.FirstOrDefaultAsync();
                if (alumnus == null || alumnus.Status != (byte?) AlumniEnum.AlumniStatus.Active)
                {
                    throw new Exception("Alumni not exist or not active");
                }

                IQueryable<Event> queryEvent = _eventRepository.Table.Where(e => e.Id == eventId);
                Event eventDetail = await queryEvent.FirstOrDefaultAsync();
                if (eventDetail == null || eventDetail.Status != (byte?) EventEnum.EventStatus.Delete)
                {
                    throw new Exception("EventNotFound");
                }

                EventRegistration newEventRegistration = new EventRegistration() {
                    AlumniId = alumniId, 
                    EventId = eventId,
                    Status = (byte?) EventRegistrationEnum.EventRegistrationStatus.Pending};

                await _eventRegistrationRepository.InsertAsync(newEventRegistration);
                await _eventRegistrationRepository.SaveChangesAsync();
            }
            
        }

        public async Task LeaveEvent(int alumniId, int eventId)
        {
            IQueryable<EventRegistration> queryEventRegistration = _eventRegistrationRepository.Table.Where(er
                => er.AlumniId == alumniId && er.EventId == eventId);
            EventRegistration eventRegistration = await queryEventRegistration.FirstOrDefaultAsync();
            _eventRegistrationRepository.Delete(eventRegistration);
            await _eventRegistrationRepository.SaveChangesAsync();
        }
    }
}