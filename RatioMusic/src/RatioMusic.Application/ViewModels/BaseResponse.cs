using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatioMusic.Application.ViewModels
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Message = string.Empty;
            Successded = true;
            Failed = false;
            Error = null;
        }
        public bool Successded { get; set; }
        public bool Failed { get; set; }
        public string[] Error { get; set; }
        public string Message { get; set; }
    }
}
