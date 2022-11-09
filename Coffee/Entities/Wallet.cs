using System;
using System.Collections.Generic;

namespace Coffee.Entities
{
    public partial class Wallet
    {
        public int Id { get; set; }
        public float? Money { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
    }
}
