using ASPDotNetCoreWebAPI.Models;

namespace ASPDotNetCoreWebAPI.Service
{
    public class BusinessLogic: IBusinessLogic
    {
        public bool ValidateNewUser(User user)
        {
            if (user == null)
                return false;
            else
            {
                if (user.Password.Length > 7)
                {
                    return true;
                }
                else
                {
                    throw new ArgumentException("Email password must be at least 8 characters long.");
                }
            }
        }

        public bool Validate()
        {
            return true;
        }

        public BusinessLogic() { }

    }
}
