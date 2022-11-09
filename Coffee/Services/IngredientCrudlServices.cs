using Coffee.Entities;
using Coffee.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Services
{
    public class IngredientCrudlServices : ICrudlIngredientService
    {

        private readonly IDbContext _dbContext;

        public IngredientCrudlServices(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Ingredient> ConfirmDeleteIngredient(int? id)
        {
            return await GetIngredient(id);
        }

        public async Task CreateIngredient(Ingredient ingredient)
        {
            await _dbContext.Ingredients.AddAsync(ingredient);
            _dbContext.SaveChanges();
        }

        public async Task DeleteIngredient(int? id)
        {
            Ingredient? ingredient =await GetIngredient(id); ;
            if (ingredient != null)
            {
                _dbContext.Ingredients.Remove(ingredient);
                _dbContext.SaveChanges();
            }
        }

        public async Task<List<Ingredient>> GetAllIngredient()
        {
            var allIngredients = await _dbContext.Ingredients.ToListAsync();
            return allIngredients;
        }

        public async Task<Ingredient> UpdateIngredient(int? id)
        {
            return await GetIngredient(id);
        }

        public async Task UpdateIngredient(Ingredient ingredient)
        {
            _dbContext.Ingredients.Update(ingredient);
            _dbContext.SaveChanges();
        }

        public async Task<Ingredient> GetIngredient(int? id)
        {
            Ingredient? ingredient =await _dbContext.Ingredients.FirstOrDefaultAsync(p => p.Id == id);
            return ingredient ?? throw new Exception();
        }
    }
}
