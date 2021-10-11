namespace UniAlumni.DataTier.ViewModels.Category
{
    public class BaseCategoryModel 
    {
        public int Id { get; set; }
        
        public string CategoryName { get; set; }
    }
    public class GetCategoryDetail: BaseCategoryModel
    {
        
        public string Description { get; set; }
    }
}