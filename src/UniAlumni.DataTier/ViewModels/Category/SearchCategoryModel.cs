using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace UniAlumni.DataTier.ViewModels.Category
{
    public class SearchCategoryModel
    {
        [DefaultValue("")] 
        [FromQuery(Name = "category-name")]
        public string CategoryName { get; set; } = "";

        [DefaultValue("")] public string Description { get; set; } = "";
    }
}