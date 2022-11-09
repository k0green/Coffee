using Coffee.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.Interfaces
{
    public interface ICrudlIngredientOfDrinkService : ICreateService, IDeleteService, IUpdateService, IGetAllService
    {
        public Task<List<DrinksingredientItemModel>> GetAllDrinkingredient();
        public Task CreateDrinksingredient(DrinksingredientItemModel drinkIngredientItemModel);
        public Task<DrinksingredientItemModel> UpdateDrinkingredient(int? idd, int? idi);
        public Task UpdateDrinksingredient(DrinksingredientItemModel drinkIngredientItemModel);
        public Task DeleteDrinkingredient(int? idd, int? idi);
        public Task<DrinksingredientItemModel> ConfirmDeleteDrinkingredient(int? idd, int? idi);
        public Task DecreaseIngredientAmount(int id);
        public Task<List<DrinksingredientItemModel>> GetAllDrinksIngredientWithCondition(int id);



    }
}
