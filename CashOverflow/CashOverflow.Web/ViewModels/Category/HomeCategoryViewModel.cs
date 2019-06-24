using CashOverflow.Models.Enum;

namespace CashOverflow.Web.ViewModels.Category
{
    public class HomeCategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CategoryType Type { get; set; }

        public string ImagePath { get; set; }
    }
}
