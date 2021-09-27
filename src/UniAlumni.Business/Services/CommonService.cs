using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniAlumni.DataTier.Common.PaginationModel;
using UniAlumni.DataTier.Models;

namespace UniAlumni.Business.Services
{
    public static class CommonService
    {
        public static IQueryable<T> PagingIQueryable<T>(this IQueryable<T> source, PaginationModel paginationModel)
        {
            return source.Skip((paginationModel.pageNumber - 1) * paginationModel.pageSize)
                .Take(paginationModel.pageSize);
        }
    }
}
