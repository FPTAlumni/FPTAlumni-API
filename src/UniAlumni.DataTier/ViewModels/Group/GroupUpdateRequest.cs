using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniAlumni.DataTier.ViewModels.Group
{
    public class GroupUpdateRequest : GroupCreateRequest
    {
        public int Id { get; set; }
        
        public byte? Status { get; set; }
    }
}
