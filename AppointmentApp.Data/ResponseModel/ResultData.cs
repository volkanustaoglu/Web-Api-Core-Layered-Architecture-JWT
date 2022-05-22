using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentApp.Data.ResponseModel
{
    public class ResultData
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public object Data { get; set; }

        public ResultData(bool isSuccess, string message)
        {
            Message = message;
            IsSuccess = isSuccess;
        }
        public static ResultData Get(bool isSuccess, string message, object data)
        {
            ResultData newResult = new ResultData(isSuccess, message);
            newResult.Data = data;
            var res =newResult;
            return res;
        }


    }
}
