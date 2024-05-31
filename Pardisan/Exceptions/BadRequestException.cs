using System;

namespace Pardisan.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string message) : base(message)
        {
        }
        public BadRequestException() : base("خطا در ثبت اطلاعات")
        {
        }
    }
}
