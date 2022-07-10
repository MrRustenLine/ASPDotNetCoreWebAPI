using ASPDotNetCoreWebAPI.Models;

namespace ASPDotNetCoreWebAPI.Service
{
    public interface IBusinessLogic
    {
        bool ValidateNewUser(User user);
        bool ValidatePassword(string password);
    }
}
