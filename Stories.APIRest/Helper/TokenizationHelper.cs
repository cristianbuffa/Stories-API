using System.Text;

namespace Stories.API.Helper
{
    public static class TokenizationHelper
    {
        public static string CreateToken(string username, string password)
        {
             byte[] bytes = Encoding.Default.GetBytes(username + ":" + password);
             return Convert.ToBase64String(bytes);
        }
    }
}
