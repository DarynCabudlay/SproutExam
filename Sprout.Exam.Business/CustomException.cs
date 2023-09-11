using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business
{
    [Serializable]
    public class CustomException : Exception
    {
        public int StatusCode { get; set; }
        public object Details { get; set; }
        public CustomException(string message, int statuscode)
            : base(message)
        {
            StatusCode = statuscode;
        }
    }
}
