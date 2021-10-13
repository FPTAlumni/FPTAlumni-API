using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniAlumni.DataTier.Common.Exception
{
    public class MyHttpException : System.Exception
    {
        public int errorCode { get; }
        public MyHttpException(int _errorCode, string message) : base(message)
        {
            errorCode = _errorCode;
        }
    }
}
