using Pardisan.Models;
using Pardisan.ViewModels;
using Pardisan.ViewModels.DataTable;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserInfo(string userData);
        //Task<DatatableResponse> GetUserDatatable(UserDatatableInput input);
        //Task<Response<ApplicationUserVM>> GetUserById(int Id);
        //Task<Response<string>> AddUser(UpsertApplicationUserVM input);
        //Task<Response<string>> UpdateUser(UpsertApplicationUserVM input);

    }
}
