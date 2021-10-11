using System.ComponentModel;

namespace UniAlumni.DataTier.ViewModels.Class
{
    public class SearchClassModel
    {
        [DefaultValue("")]
        public string ClassOf { get; set; } = "";
    }
}