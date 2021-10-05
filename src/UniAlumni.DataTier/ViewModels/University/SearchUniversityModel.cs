using System.ComponentModel;

namespace UniAlumni.DataTier.ViewModels.University
{
    public class SearchUniversityModel
    {
        [DefaultValue("")]
        public string Name { get; set; } = "";
    }
}