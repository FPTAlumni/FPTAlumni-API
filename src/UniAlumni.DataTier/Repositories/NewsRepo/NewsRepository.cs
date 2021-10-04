using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Models;

namespace UniAlumni.DataTier.Repositories.NewsRepo
{
    public class NewsRepository : BaseRepository <News>, INewsRepository
    {
        public NewsRepository(DbContext context) : base(context)
        {
        }
    }
}
