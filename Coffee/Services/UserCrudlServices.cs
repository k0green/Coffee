using Coffee.Entities;
using Coffee.Interfaces;
using Coffee.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Coffee.Services
{
    public class UserCrudlServices : ICrudlUserService
    {

        private readonly IDbContext _dbContext;

        public UserCrudlServices(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<UserItemModel>> GetAllUser()
        {
            var allUser = await _dbContext.Users.Select(u => new UserItemModel
            {
                Id = u.Id,
                Name = u.Name,
                Login = u.Login,
                Password = u.Password,
                RoleId = u.RoleId,
            }).ToListAsync();
            return allUser;
        }

        public async Task<UserItemModel> GetUser(int id)
        {
            var user = await _dbContext.Users.Select(u => new UserItemModel
            {
                Id = u.Id,
                Name = u.Name,
                Login = u.Login,
                Password = u.Password,
                RoleId = u.RoleId,
            }).FirstOrDefaultAsync(d => d.Id == id);
            return user ?? throw new Exception();
        }

        public async Task<UserItemModel> GetUserByLogin(string login)
        {

            var user = await _dbContext.Users.Select(u => new UserItemModel
            {
                Id = u.Id,
                Name = u.Name,
                Login = u.Login,
                Password = u.Password,
                RoleId = u.RoleId,
            }).FirstOrDefaultAsync(u => u.Login == login);
            return user ?? throw new Exception();
        }

        public async Task CreateUser(string name, string login, string password, int roleId)
        {
            await _dbContext.Users.AddAsync(new User { Name = name, Login = login, Password = IRegistrationService.HashPassword(password), RoleId = roleId });
            _dbContext.SaveChanges();
        }
    }
}
