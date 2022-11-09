using Coffee.Entities;
using Coffee.Model;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.Interfaces
{
    public interface ICrudlUserService : ICreateService, IDeleteService, IUpdateService, IGetAllService
    {
        public Task<List<UserItemModel>> GetAllUser();
        public Task<UserItemModel> GetUser(int id);
        public Task<UserItemModel> GetUserByLogin(string login);
        public Task CreateUser(string name, string login, string password, int roleId);

    }
}
