using Coffee.Entities;
using Coffee.Interfaces;
using Coffee.Model;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Services
{
    public class RoleCrudlServices : ICrudlRoleService
    {

        private readonly IDbContext _dbContext;

        public RoleCrudlServices(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<RoleItemModel>> GetAllRole()
        {
            var allRole = await _dbContext.Roles.Select(r=> new RoleItemModel
            {
                Id = r.Id,
                Name = r.Name,
            }).ToListAsync();
            return allRole;
        }

        public async Task<RoleItemModel> GetRole(int? id)
        {
            RoleItemModel? role = await _dbContext.Roles.Select(r => new RoleItemModel
            {
                Id = r.Id,
                Name = r.Name,
            }).FirstOrDefaultAsync(d => d.Id == id);
            return role ?? throw new Exception();
        }
    }
}
