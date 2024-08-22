using CharityHub.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace CharityHub.Business.Services.TokenRepository
{
    public interface ITokenRepository
    {
        string CreateJWTToken(User user, List<string> roles);
    }
}
