using System.Collections.Generic;

namespace Pardisan.ViewModels
{
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }
        public Response(bool succeeded, string message)
        {
            Succeeded = succeeded;
            Message = message;
        }

        public Response(int statusCode, string message = null,List<string> errors = null)
        {
            switch (statusCode)
            {
                case 200:
                    Message = "درخواست با موفقیت ارسال شد";
                    Succeeded = true;
                    break;
                case 201:
                    Message = "در خواست شما با موفقیت ثبت شده است";
                    Succeeded = false;
                    break;
                case 400:
                    Message = string.IsNullOrWhiteSpace(message) ? "آدرس در خواستی به سرور معتبر نمی باشد" : message;
                    Succeeded = false;
                    Errors = errors;
                    break;
                case 401:
                    Message = "آدرس در خواست شده نیاز به ارائه نام کاربری و کلمه عبور میباشد";
                    Succeeded = false;
                    break;
                case 403:
                    Message = "شما اجازه دسترسی به این بخش از سایت را ندارید";
                    Succeeded = false;
                    break;
                case 404:
                    Message = "منبع درخواستی پیدا نشد";
                    Succeeded = false;
                    break;
                default:

                    break;
            }
        }

        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }
}
