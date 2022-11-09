using Coffee.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.Interfaces
{
    public interface ICrudlIngredientService : ICreateService, IDeleteService, IUpdateService, IGetAllService
    {
        public Task<List<Ingredient>> GetAllIngredient();
        public Task CreateIngredient(Ingredient ingredient);
        public Task<Ingredient> UpdateIngredient(int? id);
        public Task UpdateIngredient(Ingredient ingredient);
        public Task DeleteIngredient(int? id);
        public Task<Ingredient> ConfirmDeleteIngredient(int? id);
        public Task<Ingredient> GetIngredient(int? id);

    }
}
