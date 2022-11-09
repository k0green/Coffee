using Coffee.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.Interfaces
{
    public interface ICrudlDrinkService : ICreateService, IDeleteService, IUpdateService, IGetAllService
    {
        public Task<List<Drink>> GetAllDrink();
        public Task<Drink> GetDrink(int id);
        public Task CreateDrink(Drink drink);
        public Task<Drink> UpdateDrink(int? id);
        public Task UpdateDrink(Drink drink);
        public Task DeleteDrink(int? id);
        public Task<Drink> ConfirmDeleteDrink(int? id);

    }
}
