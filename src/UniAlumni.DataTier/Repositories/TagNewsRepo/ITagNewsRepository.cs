using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.Repositories.Interface;

namespace UniAlumni.DataTier.Repositories.TagNewsRepo
{
    public interface ITagNewsRepository : IBaseRepository<TagNews>
    {
        public DbSet<TagNews> TagNews { get; }
    }
}
