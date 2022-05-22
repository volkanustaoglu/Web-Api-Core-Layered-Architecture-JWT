
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentApp.Data.Tool
{
    public static class WebLogger
    {
        public static readonly Logger DataError = LogManager.GetLogger("DataError");
        public static readonly Logger BusinessError = LogManager.GetLogger("BusinessError");
        public static readonly Logger ApiError = LogManager.GetLogger("ApiError");
    }
}
