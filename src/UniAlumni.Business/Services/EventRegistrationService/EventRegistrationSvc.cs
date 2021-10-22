using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Common.Exception;
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
                    throw new MyHttpException(StatusCodes.Status404NotFound,"Alumni not exist or not active");
                }

                IQueryable<Event> queryEvent = _eventRepository.Table.Where(e => e.Id == eventId);
                Event eventDetail = await queryEvent.FirstOrDefaultAsync();
                if (eventDetail == null || eventDetail.Status != (byte?) EventEnum.EventStatus.RegistrationStart)
                {
                    throw new MyHttpException(StatusCodes.Status404NotFound,"Event not exist or not start register");
                }

                EventRegistration newEventRegistration = new EventRegistration() {
                    AlumniId = alumniId, 
                    EventId = eventId,
                    Status = (byte?) EventRegistrationEnum.EventRegistrationStatus.Joined,
                    RegisteredDate = DateTime.Now
                };

                await _eventRegistrationRepository.InsertAsync(newEventRegistration);
                await _eventRegistrationRepository.SaveChangesAsync();
            }
            else
            {
                throw new MyHttpException(StatusCodes.Status204NoContent,"Event has been registed");

            }
            
        }

        public async Task LeaveEvent(int alumniId, int eventId)
        {
            IQueryable<EventRegistration> queryEventRegistration = _eventRegistrationRepository.Table.Where(er
                => er.AlumniId == alumniId && er.EventId == eventId);
            EventRegistration eventRegistration = await queryEventRegistration.FirstOrDefaultAsync();
            if (eventRegistration == null)
            {
                throw new MyHttpException(StatusCodes.Status404NotFound, "You cannot leave");
            }
            _eventRegistrationRepository.Delete(eventRegistration);
            await _eventRegistrationRepository.SaveChangesAsync();
        }
    }
}