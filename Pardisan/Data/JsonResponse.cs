using Pardisan.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Data
{
    public class JsonResponse
    {
        public JsonResponse(StatusCode statusCode, string message, List<string> errors, object data)
        {
            this.Status = statusCode;
            this.Message = message;
            this.Errors = errors;
            this.Data = data;
        }
        public JsonResponse(object data)
        {
            this.Status = StatusCode.OK;
            this.Message = "با موفقیت انجام شد";
            this.Errors = new List<string>();
            this.Data = data;
        }
        public StatusCode Status { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public object Data { get; set; }
    }

    public enum StatusCode
    {
        [Display(Name = "OK")]
        OK = 200,

        [Display(Name = "NonAuthoritativeInformation")]
        NonAuthoritativeInformation = 203,

        [Display(Name = "BadRequest")]
        BadRequest = 400,

        [Display(Name = "UnAuthorized")]
        UnAuthorized = 401,

        [Display(Name = "PaymentRequired")]
        PaymentRequired = 401,

        [Display(Name = "Forbidden")]
        Forbidden = 403,

        [Display(Name = "NotFound")]
        NotFound = 404,

        [Display(Name = "MethodNotAllowed")]
        MethodNotAllowed = 405,

        [Display(Name = "LockOnChangePassword")]
        LockOnChangePassword = 471,

        [Display(Name = "InternalServerError")]
        InternalServerError = 500,
    }
}
