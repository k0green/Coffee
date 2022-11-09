using Coffee.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.Interfaces
{
    public interface ICrudlWalletService : ICreateService, IDeleteService, IGetAllService
    {
        public Task<List<WalletItemModel>> GetAllWallet();
        public Task<WalletItemModel> GetOneWallet(int? id);
        public Task<Wallet> GetWalletByUserId(int? id);
        public Task UpdateWallet(int id);
        public Task CreateWallet(int id);
        public Task DeleteWallet(int? id);
        public Task<WalletItemModel> ConfirmDeleteWallet(int? id);
        public Task WithdrawMoney(Wallet wallet, Drink drink);

    }
}
