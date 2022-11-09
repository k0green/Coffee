using Coffee.Entities;
using Coffee.Interfaces;
using System.Security.Cryptography;

namespace Coffee.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IDbContext _dbContext;

        public RegistrationService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
