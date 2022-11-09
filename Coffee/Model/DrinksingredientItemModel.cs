using System.ComponentModel.DataAnnotations;

namespace Coffee.Entities
{
    public partial class DrinksingredientItemModel
    {
        public int? DrinkId { get; set; }
        public int? IngredientsId { get; set; }
        public float AmountInOneDrink { get; set; }
    }
}
