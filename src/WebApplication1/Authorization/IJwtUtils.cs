using WebApplication1.Entities;
using WebApplication1.Models;

namespace WebApplication1.Authorization
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(Customer account);
        public int? ValidateJwtToken(string token);
        public Task<RefreshToken> GenerateRefreshToken(string ipAddress);
    }
}
