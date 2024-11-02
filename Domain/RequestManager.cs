using ydai5.TechnicalServices;

namespace ydai5.Domain
{
    public class RequestManager
    {
        public List<Category> DisplayCategoryList()
        {
            Categories CategoryManager = new();

            List<Category> CategoryList = CategoryManager.GetCategoryList();

            return CategoryList;
        }
    }
}
