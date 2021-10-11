using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Models;
using UniAlumni.DataTier.Repositories.Interface;

namespace UniAlumni.DataTier.Repositories.NewsRepo
{
    public interface INewsRepository : IBaseRepository<News>
    {
    }
}
