using ASPDotNetCoreWebAPI.Models;

namespace ASPDotNetCoreWebAPI.Service
{
    public class BusinessLogic: IBusinessLogic
    {
        public BusinessLogic() { }
        public bool ValidateNewUser(User user)
        {
            try
            {
                if (user == null)
                    return false;
                else
                {
                    try
                    {
                        if (ValidatePassword(user.Password))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch(ArgumentException ae) 
                    {
                        throw;
                    }
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
        public bool ValidatePassword(string password)
        {
            if (password.Length < 8)
            {
                throw new ArgumentException("Email password must be at least 8 characters long.");
            }
            //else if (user.Password.Contains())
            else
            {
                return true;
            }
        }
    }
}
