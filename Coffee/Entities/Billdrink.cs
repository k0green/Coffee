using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Coffee.Entities
{
    public partial class Billdrink
    {
        public Guid BillId { get; set; }
        public int DrinkId { get; set; }
        [Key]
        public float Coast { get; set; }

        public virtual Bill Bill { get; set; } = null!;
        public virtual Drink Drink { get; set; } = null!;
    }
}
