using Coffee.Entities;
using Coffee.Model;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.Interfaces
{
    public interface ICrudlBillDrinkService : IGetAllService, ICreateService
    {
        public Task<List<BilldrinkItemModel>> GetAllBillDrink();
        public Task<BilldrinkItemModel> GetBillDrink(Guid id);
        public Task CreateBillDrink(Guid billId, int drinkId, float coast);
        public Task<IQueryable<BilldrinkItemModel>> GetAllBillDrinkWithCondition(Guid id);
        public Task GetSum(Guid id);


    }
}
