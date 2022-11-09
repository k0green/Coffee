using System;
using System.Collections.Generic;

namespace Coffee.Entities
{
    public partial class Ingredient
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public float? Amount { get; set; }
    }
}
