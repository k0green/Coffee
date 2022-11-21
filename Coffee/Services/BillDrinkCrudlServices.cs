using Coffee.Entities;
using Coffee.Interfaces;
using Coffee.Model;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Services
{
    public class BillDrinkCrudlServices : ICrudlBillDrinkService
    {

        private readonly IDbContext _dbContext;
        private readonly ICrudlBillsService _crudlBillsService;


        public BillDrinkCrudlServices(IDbContext dbContext, ICrudlBillsService crudlBillsService)
        {
            _dbContext = dbContext;
            _crudlBillsService = crudlBillsService;
        }

        public async Task<List<BilldrinkItemModel>> GetAllBillDrink()
        {
            var allBillDrink = await _dbContext.Billdrinks.Select(b=>new BilldrinkItemModel
            {
                BillId = b.BillId,
                DrinkId = b.DrinkId,
                Coast = b.Coast,
            }).ToListAsync();
            return allBillDrink;
        }
        public async Task<BilldrinkItemModel> GetBillDrink(Guid id)
        {
            BilldrinkItemModel? billDrink = await _dbContext.Billdrinks.Select(b => new BilldrinkItemModel
            {
                BillId = b.BillId,
                DrinkId = b.DrinkId,
                Coast = b.Coast,
            }).FirstOrDefaultAsync(p => p.BillId == id);
            return billDrink;
        }

        public List<Billdrink> GetAllBillDrinkWithCondition(Guid id)
        {
            var allBillDrinkWithCondition = _dbContext.Billdrinks.Where(b => b.BillId == id).ToList();
            return allBillDrinkWithCondition;
        }

        public async Task GetSum(Guid id)
        {
            float? sum = 0;
            var billdrink = GetAllBillDrinkWithCondition(id);
            foreach (var coasts in billdrink)
            {
                sum += coasts.Coast;
            }
            var bills = await _crudlBillsService.GetBill(id);
            bills.Sum = sum;
            await _crudlBillsService.UpdateBill(bills);

        }

        public async Task CreateBillDrink(Guid billId, int drinkId, float coast)
        {
            Billdrink billdrink = new Billdrink();
            billdrink.BillId = billId;
            billdrink.DrinkId = drinkId;
            billdrink.Coast = coast;
            await _dbContext.Billdrinks.AddAsync(billdrink);
            _dbContext.SaveChanges();
        }
    }
}
