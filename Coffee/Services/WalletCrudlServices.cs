using Coffee.Entities;
using Coffee.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Services
{
    public class WalletCrudlServices : ICrudlWalletService
    {

        private readonly IDbContext _dbContext;
        public WalletCrudlServices(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<WalletItemModel> ConfirmDeleteWallet(int? id)
        {
            WalletItemModel? wallets = await _dbContext.Wallets.Select(w => new WalletItemModel
            {
                Id = w.Id,
                Money = w.Money,
                UserId = w.UserId,
            }).FirstOrDefaultAsync(p => p.Id == id);
            return wallets ?? throw new Exception();
        }

        public async Task UpdateWallet(int id)
        {
            Wallet? wallets =await _dbContext.Wallets.FirstOrDefaultAsync(p => p.UserId == id);
            var rnd = new Random();
            wallets.Money = rnd.Next(10, 25);
            _dbContext.Wallets.Update(wallets);
            _dbContext.SaveChanges();
        }

        public async Task CreateWallet(int id)
        {
            Wallet wallet = new Wallet();
            wallet.UserId = id;
            var rnd = new Random();
            wallet.Money = rnd.Next(10, 25);
            await _dbContext.Wallets.AddAsync(wallet);
            _dbContext.SaveChanges();
        }

        public async Task DeleteWallet(int? id)
        {
            Wallet? wallets = await _dbContext.Wallets.FirstOrDefaultAsync(p => p.Id == id);
            if (wallets != null)
            {
                _dbContext.Wallets.Remove(wallets);
                _dbContext.SaveChanges();
            }
        }

        public async Task<List<WalletItemModel>> GetAllWallet()
        {
            var allWallet = await _dbContext.Wallets.Select(w=>new WalletItemModel
            {
                Id = w.Id,
                Money = w.Money,
                UserId = w.UserId,
            }).ToListAsync();
            return allWallet;
        }

        public async Task<WalletItemModel> GetOneWallet(int? id)
        {
            WalletItemModel? wallets = await _dbContext.Wallets.Select(w => new WalletItemModel
            {
                Id = w.Id,
                Money = w.Money,
                UserId = w.UserId,
            }).FirstOrDefaultAsync(p => p.Id == id);
            return wallets ?? throw new Exception();
        }

        public async Task<Wallet> GetWalletByUserId(int? id)
        {
            Wallet wallets = await _dbContext.Wallets.FirstOrDefaultAsync(d => d.UserId == id);
            return wallets;
        }

        public async Task WithdrawMoney(Wallet wallet, Drink drink)
        {
            wallet.Money -= drink.Coast;
            _dbContext.SaveChanges();
        }
    }
}
