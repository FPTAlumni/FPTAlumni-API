namespace UniAlumni.DataTier.ViewModels.University
{
    public class BaseUniversityModel 
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
   public class UniversityViewModel : BaseUniversityModel
    {
        public string Address { get; set; }
        public string Logo { get; set; }
    }
}
