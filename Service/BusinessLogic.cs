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
                        if (ValidatePassword(user.Password) && ValidateDisplayName(user.DisplayName) && ValidateEmail(user.Email))
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
            bool hasUpper = password.Any(char.IsUpper);
            bool hasNumber = password.Any(char.IsNumber);
            bool lengthCorrect = (password.Length > 7);
            if (lengthCorrect && hasUpper && hasNumber)
            {
                return true;
            }
            else
            {
                throw new ArgumentException("Email password must be at least 8 characters long with at least 1 upper case and 1 numeric.");
            }
        }

        public bool ValidateDisplayName(string displayName)
        {
            if (!string.IsNullOrEmpty(displayName))
            {
                return true;
            }
            else
            {
                throw new ArgumentException("Display name cannot be blank.");
            }
        }

        public bool ValidateEmail(string email)
        {
            if (email.Length > 2)
            {
                return true;
            }
            else
            {
                throw new ArgumentException("Invalid email address.");
            }
        }
    }
}
