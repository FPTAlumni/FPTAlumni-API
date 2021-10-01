using System.ComponentModel;

namespace UniAlumni.DataTier.ViewModels.Category
{
    public class SearchCategoryModel
    {
        [DefaultValue("")] public string Categoryname { get; set; } = "";

        [DefaultValue("")] public string Description { get; set; } = "";
    }
}