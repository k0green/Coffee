using Coffee.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.Interfaces
{
    public interface ICrudlBillsService : IGetAllService, IUpdateService
    {
        public Task<List<Bill>> GetAllBill();
        public Task<Bill> GetBill(Guid id);
        public Task UpdateBill(Bill bill);
        public Task CreateFirstBill(Guid billId, int userId);

    }
}
