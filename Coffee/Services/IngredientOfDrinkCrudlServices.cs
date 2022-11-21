using Coffee.Entities;
using Coffee.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Services
{
    public class IngredientOfDrinkCrudlServices : ICrudlIngredientOfDrinkService
    {

        private readonly IDbContext _dbContext;
        private readonly ICrudlIngredientService _crudlIngredientService;

        public IngredientOfDrinkCrudlServices(IDbContext dbContext, ICrudlIngredientService crudlIngredient)
        {
            _dbContext = dbContext;
            _crudlIngredientService = crudlIngredient;
        }

        public async Task<DrinksingredientItemModel> ConfirmDeleteDrinkingredient(int? idd, int? idi)
        {
            DrinksingredientItemModel? ingredientOfdrinks = await _dbContext.Drinksingredients.Select(c => new DrinksingredientItemModel
            {
                DrinkId = c.DrinkId,
                IngredientsId = c.IngredientsId,
                AmountInOneDrink = c.AmountInOneDrink
            }).FirstOrDefaultAsync(p => p.DrinkId == idd && p.IngredientsId == idi);
            return ingredientOfdrinks;
        }

        public async Task CreateDrinksingredient(DrinksingredientItemModel drinkIngredientItemModel)
        {
            var drinkIngredient = new Drinksingredient
            {
                DrinkId = drinkIngredientItemModel.DrinkId,
                IngredientsId = drinkIngredientItemModel.IngredientsId,
                AmountInOneDrink = drinkIngredientItemModel.AmountInOneDrink,
            };
            try
            {
                await _dbContext.Drinksingredients.AddAsync(drinkIngredient);
                _dbContext.SaveChanges();
            }
            catch
            {
                _dbContext.Drinksingredients.Remove(drinkIngredient);
                throw;
            }
        }

        public async Task DecreaseIngredientAmount(int id)
        {

            var drinksingredient = await GetAllDrinksIngredientWithCondition(id);

            foreach (var drink in drinksingredient)
            {
                var ingredient = _dbContext.Ingredients.FirstOrDefault(i => i.Id == drink.IngredientsId);
                ingredient.Amount -= drink.AmountInOneDrink;
                _dbContext.SaveChanges();
            }
            _dbContext.SaveChanges();
        }

        public async Task DeleteDrinkingredient(int? idd, int? idi)
        {
            Drinksingredient? drinksIngredient = await _dbContext.Drinksingredients.FirstOrDefaultAsync(p => p.DrinkId == idd && p.IngredientsId == idi);
            if (drinksIngredient != null)
            {
                _dbContext.Drinksingredients.Remove(drinksIngredient);
                _dbContext.SaveChanges();
            }
        }

        public async Task<List<Drinksingredient>> GetAllDrinksIngredientWithCondition(int id)
        {
            var drinksingredient = await _dbContext.Drinksingredients.Where(b => b.DrinkId == id).ToListAsync();
            return drinksingredient;
        }

        public async Task<List<DrinksingredientItemModel>> GetAllDrinkingredient()
        {
            var allIndredientOfDrink = await _dbContext.Drinksingredients.Select(c=>new DrinksingredientItemModel 
            { DrinkId = c.DrinkId,
              IngredientsId = c.IngredientsId, 
              AmountInOneDrink = c.AmountInOneDrink
            }).ToListAsync();
            return allIndredientOfDrink;
        }

        public async Task<DrinksingredientItemModel> UpdateDrinkingredient(int? idd, int? idi)
        {
            DrinksingredientItemModel? indredientOfDrink = await _dbContext.Drinksingredients.Select(c => new DrinksingredientItemModel
            {
                DrinkId = c.DrinkId,
                IngredientsId = c.IngredientsId,
                AmountInOneDrink = c.AmountInOneDrink
            }).FirstOrDefaultAsync(p => p.DrinkId == idd && p.IngredientsId == idi);
            return indredientOfDrink ?? throw new Exception();
        }

        public async Task UpdateDrinksingredient(DrinksingredientItemModel drinkIngredientItemModel)
        {
            var drinkIngredient = new Drinksingredient
            {
                DrinkId = drinkIngredientItemModel.DrinkId,
                IngredientsId = drinkIngredientItemModel.IngredientsId,
                AmountInOneDrink = drinkIngredientItemModel.AmountInOneDrink,
            };
            try
            {
                _dbContext.Drinksingredients.Update(drinkIngredient);
                _dbContext.SaveChanges();
            }
            catch
            {
                _dbContext.Drinksingredients.Remove(drinkIngredient);
                throw;
            }
        }
    }
}
