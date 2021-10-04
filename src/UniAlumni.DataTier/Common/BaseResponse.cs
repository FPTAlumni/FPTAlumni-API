using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniAlumni.DataTier.Common
{
    public class BaseResponse<T>
    {
        public int Code { get; set; }
        public string Msg { get; set; }
        public T Data { get; set; }
    }
    public class ModelsResponse<T>
    {
        public int Code { get; set; }
        public string Msg { get; set; }
        public PagingMetadata Metadata { get; set; }
        public List<T> Data { get; set; }
    }
    public class PagingMetadata
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public int Total { get; set; }
    }
}
