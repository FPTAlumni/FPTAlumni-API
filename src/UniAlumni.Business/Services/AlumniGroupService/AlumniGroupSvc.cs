using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniAlumni.DataTier.Common.Enum;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.Repositories.AlumniGroupRepo;
using UniAlumni.DataTier.Repositories.AlumniRepo;
using UniAlumni.DataTier.Repositories.GroupRepo;

namespace UniAlumni.Business.Services.AlumniGroupService
{
    public class AlumniGroupSvc : IAlumniGroupSvc
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IAlumniRepository _alumniRepository;
        private readonly IAlumniGroupRepository _alumniGroupRepository;

        public AlumniGroupSvc(IGroupRepository groupRepository, IAlumniRepository alumniRepository,
            IAlumniGroupRepository alumniGroupRepository)
        {
            _groupRepository = groupRepository;
            _alumniRepository = alumniRepository;
            _alumniGroupRepository = alumniGroupRepository;
        }

        public async Task JoinGroup(int alumniId, int groupId)
        {
            IQueryable<AlumniGroup> queryAlumniGroup =
                _alumniGroupRepository.Table.Where(ag => ag.AlumniId == alumniId && ag.GroupId == groupId);
            AlumniGroup alumniGroup = await queryAlumniGroup.FirstOrDefaultAsync();
            if (alumniGroup == null)
            {
                IQueryable<Group> queryGroup = _groupRepository.Table.Where(g => g.Id == groupId);
                Group group = await queryGroup.FirstOrDefaultAsync();
                if (group == null || group.Status == (byte?) GroupEnum.GroupStatus.Inactive)
                {
                    throw new Exception("GroupNotFound");
                }

                IQueryable<Alumnus> queryAlumni = _alumniRepository.Table.Where(alu => alu.Id == alumniId);
                Alumnus alumnus = await queryAlumni.FirstOrDefaultAsync();
                if (alumnus == null || alumnus.Status != (byte?) AlumniEnum.AlumniStatus.Active)
                {
                    throw new Exception("Alumni not exist or not active");
                }

                AlumniGroup newAlumniGroup = new AlumniGroup()
                {
                    AlumniId = alumniId, GroupId = groupId, Status = (byte?) AlumniGroupEnum.AlumniGroupStatus.Inactive
                };
                await _alumniGroupRepository.InsertAsync(newAlumniGroup);
                await _alumniGroupRepository.SaveChangesAsync();
            }
        }

        public async Task LeaveGroup(int alumniId, int groupId)
        {
            IQueryable<AlumniGroup> queryAlumniGroup =
                _alumniGroupRepository.Table.Where(ag => ag.AlumniId == alumniId && ag.GroupId == groupId);
            AlumniGroup alumniGroup = await queryAlumniGroup.FirstOrDefaultAsync();
            _alumniGroupRepository.Delete(alumniGroup);
            await _alumniGroupRepository.SaveChangesAsync();
        }
    }
}