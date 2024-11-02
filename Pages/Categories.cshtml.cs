using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ydai5.Domain;
using Microsoft.Data.SqlClient;
using System.Data;
using ydai5.TechnicalServices;

namespace ydai5.Pages
{
    public class ydai5CategoriesModel : PageModel
    {
        private List<Category> _categoryList = new();
        public List<Category> CategoryList
        {
            get
            {
                return _categoryList;
            }
        }
        public void OnGet()
        {
            RequestManager requestManager = new();

            CategoryList.AddRange(requestManager.DisplayCategoryList());

        }
    }
}
