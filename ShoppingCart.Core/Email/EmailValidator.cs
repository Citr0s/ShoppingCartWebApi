using System.Net.Mail;

namespace ShoppingCart.Core.Email
{
    public static class EmailValidator
    {
        public static bool IsValid(string email)
        {
            try
            {
                var emailAddress = new MailAddress(email);
                return emailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}