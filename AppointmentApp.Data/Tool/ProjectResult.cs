using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppointmentApp.Data.Tool
{
    public class ProjectResult<T> where T : class, new()
    {


        public string Message { get; set; }

        public bool IsSuccess { get; set; }

        public T Data { get; set; }

        public int ReturnId { get; set; }

        public ProjectResult()
        {
            Message = "";
            IsSuccess = true;
        }
    }
}