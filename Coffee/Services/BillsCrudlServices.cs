using Coffee.Entities;
using Coffee.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Services
{
    public class BillsCrudlServices : ICrudlBillsService
    {

        private readonly IDbContext _dbContext;

        public BillsCrudlServices(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Bill>> GetAllBill()
        {
            var allBill = await _dbContext.Bills.ToListAsync();
            return allBill;
        }

        public async Task<Bill> GetBill(Guid id)
        {
            Bill? bill = await _dbContext.Bills.FirstOrDefaultAsync(p => p.Id == id);
            return bill ?? throw new Exception();
        }

        public async Task UpdateBill(Bill bill)
        {
            _dbContext.Bills.Update(bill);
            _dbContext.SaveChanges();
        }

        public async Task CreateFirstBill(Guid billId, int userId)
        {
            Bill bill = new Bill();
            bill.Id = billId;
            bill.Sum = 0;
            bill.UserId = userId;
            await _dbContext.Bills.AddAsync(bill);
            _dbContext.SaveChanges();
        }
    }
}
