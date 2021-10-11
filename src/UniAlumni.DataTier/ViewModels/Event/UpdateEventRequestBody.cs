using System.ComponentModel.DataAnnotations;

namespace UniAlumni.DataTier.ViewModels.Event
{
    public class UpdateEventRequestBody : CreateEventRequestBody
    {
        [Required]
        public int Id { get; set;}
    }
}