using Coffee.Entities;
using Coffee.Model;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.Interfaces
{
    public interface ICrudlRoleService : IGetAllService
    {
        public Task<List<RoleItemModel>> GetAllRole();
        public Task<RoleItemModel> GetRole(int? id);

    }
}
