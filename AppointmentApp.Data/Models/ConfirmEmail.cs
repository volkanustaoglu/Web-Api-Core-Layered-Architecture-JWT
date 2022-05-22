using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentApp.Data.Models
{
    public class ConfirmEmail
    {
        public int? UserId { get; set; }
        public string? ConfirmToken { get; set; }
    }
}
