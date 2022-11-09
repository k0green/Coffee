using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Coffee.Entities
{
    public partial class Drinksingredient
    {
        public int? DrinkId { get; set; }
        public int? IngredientsId { get; set; }
        [Key]
        public float AmountInOneDrink { get; set; }

        public virtual Drink? Drink { get; set; }
        public virtual Ingredient? Ingredients { get; set; }
    }
}
