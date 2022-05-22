using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentApp.Data.Models
{
    public class UpdatePasswordModel
    {
        public string Token { get; set; }
        public int UserId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
