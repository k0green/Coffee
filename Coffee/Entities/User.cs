namespace Coffee.Entities
{
    public class User
    {
        public User()
        {
            Wallets = new HashSet<Wallet>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public int? RoleId { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<Wallet> Wallets { get; set; }
    }
}
