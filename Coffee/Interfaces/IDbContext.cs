using Coffee.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Interfaces
{
    public interface IDbContext
    {
        DbSet<Bill> Bills { get; set; }
        DbSet<Billdrink> Billdrinks { get; set; }
        DbSet<Drink> Drinks { get; set; }
        DbSet<Drinksingredient> Drinksingredients { get; set; }
        DbSet<Ingredient> Ingredients { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Wallet> Wallets { get; set; }

        int SaveChanges();
    }
}
