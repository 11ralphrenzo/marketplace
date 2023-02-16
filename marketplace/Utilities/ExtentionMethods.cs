using System.Text.RegularExpressions;

namespace marketplace.Utilities
{
    public static class ExtentionMethods
    {
        public static bool IsValidEmail(this string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidPassword(this string password)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            return hasNumber.IsMatch(password) &&
                hasUpperChar.IsMatch(password) && 
                hasMinimum8Chars.IsMatch(password);
        }
    }
}
