using System;

namespace ShoppingCart.Core.Email
{
    public static class EmailValidator
    {
        public static bool IsValid(string email)
        {
            try
            {
                var emailAddress = new System.Net.Mail.MailAddress(email);
                return emailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}