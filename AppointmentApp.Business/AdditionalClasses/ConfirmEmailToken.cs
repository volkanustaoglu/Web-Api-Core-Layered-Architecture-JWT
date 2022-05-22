using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppointmentApp.Business.AdditionalClasses
{
    public class ConfirmEmailToken
    {
        private static Random random = new Random();

        public static string RandomStringConfirmEmail()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789123!";
            return new string(Enumerable.Repeat(chars, chars.Length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
