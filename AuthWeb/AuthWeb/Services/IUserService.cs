using AuthWeb.Data;
namespace AuthWeb.Services
{
    public interface IUserService
    {
        ApplicationUser GetUserByUserName(string email);
    }
}
