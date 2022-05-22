using AppointmentApp.Data.Models;
using System.Security.Cryptography;
using System.Text;

namespace AppointmentApp.Business.AdditionalClasses
{
    public class PasswordHash
    {
        public static string GetPasswordHash(User entity)
        {
            var salt = "%-%0%%1881s";
            // Create an MD5 hash of the supplied password using the supplied salt as well.
            string sourceText = salt + entity.Password;
            ASCIIEncoding asciiEnc = new ASCIIEncoding();
            string hash = null;
            byte[] byteSourceText = asciiEnc.GetBytes(sourceText);
            MD5CryptoServiceProvider md5Hash = new MD5CryptoServiceProvider();
            byte[] byteHash = md5Hash.ComputeHash(byteSourceText);
            foreach (byte b in byteHash)
            {
                hash += b.ToString("x2");
            }

            // Return the hashed password
            entity.Password = hash;

            return entity.Password;

        }
    }
}
