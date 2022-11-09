using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Coffee.Entities
{
    public partial class Drink
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public float Volume { get; set; }
        public TimeOnly PreparingTime { get; set; }
        public float Coast { get; set; }
    }
}
