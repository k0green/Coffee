namespace Coffee.Entities
{
    public partial class Bill
    {
        public Guid Id { get; set; }
        public float? Sum { get; set; }
        public int? UserId { get; set; }
    }
}
