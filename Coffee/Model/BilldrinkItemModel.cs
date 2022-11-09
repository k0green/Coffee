using System.ComponentModel.DataAnnotations;

namespace Coffee.Model
{
    public partial class BilldrinkItemModel
    {
        public Guid BillId { get; set; }
        public int DrinkId { get; set; }
        [Key]
        public float Coast { get; set; }

    }
}
