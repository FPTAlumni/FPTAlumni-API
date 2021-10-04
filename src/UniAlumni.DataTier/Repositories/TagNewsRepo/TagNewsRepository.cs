using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Models;

namespace UniAlumni.DataTier.Repositories.TagNewsRepo
{
    public class TagNewsRepository : BaseRepository<TagNews>, ITagNewsRepository
    {
        public TagNewsRepository(DbContext context) : base(context)
        {
        }

        public DbSet<TagNews> TagNews => this.CurrentContext.Set<TagNews>();
    }
}
