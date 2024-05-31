using Pardisan.Data;
using Pardisan.Interfaces;
using Pardisan.Models;
using Pardisan.ViewModels.Users;
using Pardisan.ViewModels;
using Pardisan.ViewModels.DataTable;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Services
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<DatatableResponse> GetUserDatatable(UserDatatableInput input)
        //{
        //    var userObj = _context.Users.Select(d => new UserDatatableResult()
        //    {
        //        Id = d.Id,
        //        FirstName = d.FirstName,
        //        LastName = d.LastName,
        //        Email = d.Email,
        //        UserName = d.UserName,
        //    }).OrderByDescending(d => d.Id).AsQueryable();


        //    if (!string.IsNullOrEmpty(input.Search.Value))
        //    {
        //        userObj = userObj.Where(w =>
        //            string.IsNullOrEmpty(w.UserName) || w.UserName.Contains(input.Search.Value)
        //        );
        //    }
        //    DatatableResponse response = new DatatableResponse
        //    {
        //        ITotalDisplayRecords = userObj.Count(),
        //        ITotalRecords = _context.News.Count(),
        //        AaData = await userObj.Skip(input.Start).Take(input.Length).ToListAsync(),
        //        SEcho = 0
        //    };
        //    return response;
        //}

        public async Task<ApplicationUser> GetUserInfo(string userData)
        {
            var user = await _context.Users.FirstOrDefaultAsync(d => d.UserName == userData);
            return user;
        }

        //public async Task<Response<ApplicationUserVM>> GetUserById(int Id)
        //{
        //    try
        //    {
        //        var userObj = await _context.Users.FirstOrDefaultAsync(d => d.Id == Id.ToString());
        //        if (userObj == null)
        //        {
        //            return new Response<ApplicationUserVM>(404);
        //        }
        //        return new Response<ApplicationUserVM>(CreateViewModelFromUser(userObj));
        //    }
        //    catch (Exception)
        //    {
        //        return new Response<ApplicationUserVM>("درخواست شما با مشکل مواجه شده است لطفا دوباره تلاش کنید");
        //    }
        //}

        //public async Task<Response<string>> AddUser(UpsertApplicationUserVM input)
        //{
        //    var userModel = new ApplicationUser()
        //    {
        //        FirstName = input.FirstName,
        //        LastName = input.LastName,
        //        UserName = input.UserName,
        //        Email = input.Email,

        //    };
        //    await _context.Users.AddAsync(userModel);
        //    await _context.SaveChangesAsync();
        //    return new Response<string>(200);
        //}

        //public async Task<Response<string>> UpdateUser(UpsertApplicationUserVM input)
        //{
        //    var newsModel = await _context.Users.FirstOrDefaultAsync(d => d.Id == input.Id);
        //    if (newsModel == null)
        //    {
        //        return new Response<string>(404);
        //    }
        //    newsModel.FirstName = input.FirstName;
        //    newsModel.LastName = input.LastName;
        //    newsModel.UserName = input.UserName;
        //    newsModel.Email = input.Email;

        //    await _context.SaveChangesAsync();
        //    return new Response<string>(200);

        //}

        //private ApplicationUserVM CreateViewModelFromUser(ApplicationUser user)
        //{
        //    var viewModel = new ApplicationUserVM()
        //    {
        //        Id = user.Id,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        UserName = user.UserName,
        //        Email = user.UserName
        //    };

        //    return viewModel;
        //}

    }
}
