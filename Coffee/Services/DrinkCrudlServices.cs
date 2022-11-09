using Coffee.Entities;
using Coffee.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Services
{
    public class DrinkCrudlServices : ICrudlDrinkService
    {

        private readonly IDbContext _dbContext;

        public DrinkCrudlServices(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Drink> ConfirmDeleteDrink(int? id)
        {
            Drink? drinks = await _dbContext.Drinks.FirstOrDefaultAsync(p => p.Id == id);
                return drinks ?? throw new Exception();
        }

        public async Task CreateDrink(Drink drink)
        {
            await _dbContext.Drinks.AddAsync(drink);
            _dbContext.SaveChanges();
        }

        public async Task DeleteDrink(int? id)
        {
            Drink? drinks = await _dbContext.Drinks.FirstOrDefaultAsync(p => p.Id == id);
            if (drinks != null)
            {
                _dbContext.Drinks.Remove(drinks);
                _dbContext.SaveChanges();
            }
        }

        public async Task<List<Drink>> GetAllDrink()
        {
            var allDrink = await _dbContext.Drinks.ToListAsync();
            return allDrink;
        }

        public async Task<Drink> GetDrink(int id)
        {
            return await _dbContext.Drinks.FirstOrDefaultAsync(d => d.Id == id) ?? throw new Exception();
        }

        public async Task<Drink> UpdateDrink(int? id)
        {
            Drink? drinks = await _dbContext.Drinks.FirstOrDefaultAsync(p => p.Id == id);
                return drinks;
        }

        public async Task UpdateDrink(Drink drink)
        {
            _dbContext.Drinks.Update(drink);
            _dbContext.SaveChanges();
        }
    }
}
