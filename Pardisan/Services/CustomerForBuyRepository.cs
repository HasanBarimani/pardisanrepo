using Pardisan.Data;
using Pardisan.Interface;
using Pardisan.Models;
using Pardisan.ViewModels.API.CustomerForSale;
using Pardisan.ViewModels;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System;
using Pardisan.Interfaces;
using Pardisan.ViewModels.API.CustomerForBuy;
using Microsoft.EntityFrameworkCore;
using DNTPersianUtils.Core;
using System.Linq;

namespace Pardisan.Services
{
    public class CustomerForBuyRepository : ICustomerForBuyRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerForBuyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<string>> Add(EditCustomerForBuyVM input)
        {
            var customerForBuy = new CustomerForBuy()
            {
                Name = input.Name,
                PhoneNumber = input.PhoneNumber,
                Budget = input.Budget,
                HowToKnow = input.HowToKnow,
                FinalOpinion = input.FinalOpinion,
                FirstRecord = input.FirstRecord,
                SecondRecord = input.SecondRecord,
                CreatedAt = DateTime.Now,
                Address = input.Address,
                SaleManagerOpinion = input.SaleManagerOpinion,


            };

            PersianCalendar pc = new PersianCalendar();
            DateTime dateStart = new DateTime(input.Date.Year, input.Date.Month, input.Date.Day, pc);


            //estate.Image = await FileManager.Images.Upload(PublicHelper.FilePath.EstateImagePath, input.Image);

            customerForBuy.Date = dateStart;


            await _context.CustomerForBuys.AddAsync(customerForBuy);
            await _context.SaveChangesAsync();
            return new Response<string>(200);
        }
        public async Task<Response<string>> Active(int id)
        {
            var find = await _context.CustomerForBuys.FirstOrDefaultAsync(d => d.Id == id);

            if (find == null)
            {
                return new Response<string>(404);
            }

            find.IsActive = true;
            await _context.SaveChangesAsync();
            return new Response<string>(true, "فعال شد");
        }

        public async Task<Response<string>> Disable(int id)
        {
            var find = await _context.CustomerForBuys.FirstOrDefaultAsync(d => d.Id == id);

            if (find == null)
            {
                return new Response<string>(404);
            }

            find.IsActive = false;
            await _context.SaveChangesAsync();
            return new Response<string>(true, "غیرفعال شد");
        }

        public async Task<Response<EditCustomerForBuyVM>> GetById(int id)
        {
            try
            {
                var data = await _context.CustomerForBuys.FirstOrDefaultAsync(d => d.Id == id);
                if (data == null)
                {
                    return new Response<EditCustomerForBuyVM>(404);
                }
                return new Response<EditCustomerForBuyVM>(CreateViewModelFromBlog(data));
            }
            catch (Exception)
            {
                return new Response<EditCustomerForBuyVM>("درخواست شما با مشکل مواجه شده است لطفا دوباره تلاش کنید");
            }
        }
        private EditCustomerForBuyVM CreateViewModelFromBlog(CustomerForBuy input)
        {
            var viewModel = new EditCustomerForBuyVM()
            {
                Id = input.Id,
                Name = input.Name,
                PhoneNumber = input.PhoneNumber,
                Budget = input.Budget,
                FinalOpinion = input.FinalOpinion,
                FirstRecord = input.FirstRecord,
                HowToKnow = input.HowToKnow,
                SaleManagerOpinion = input.SaleManagerOpinion,
                Address = input.Address,
                SecondRecord = input.SecondRecord,

                DateForShow = input.Date.ToShortPersianDateString(),

            };

            return viewModel;
        }

        public async Task<Response<string>> Update(EditCustomerForBuyVM input)
        {
            var data = await _context.CustomerForBuys.FirstOrDefaultAsync(d => d.Id == input.Id);
            if (data == null)
            {
                return new Response<string>(404);
            }
            data.Name = input.Name;
            data.PhoneNumber = input.PhoneNumber;
            data.Budget = input.Budget;
            data.Address = input.Address;
            data.SaleManagerOpinion = input.SaleManagerOpinion;
            data.FirstRecord = input.FirstRecord;
            data.SecondRecord = input.SecondRecord;
            data.HowToKnow = input.HowToKnow;
            data.Date = input.Date;
            data.FinalOpinion = input.FinalOpinion;
            data.UpdatedAt = DateTime.Now;



            PersianCalendar pc = new PersianCalendar();
            DateTime dateStart = new DateTime(input.Date.Year, input.Date.Month, input.Date.Day, pc);

            data.Date = dateStart;




            await _context.SaveChangesAsync();
            return new Response<string>(200);

        }

        public async Task<Response<List<CustomerForBuyVM>>> GetAll()
        {
            var find = await _context.CustomerForBuys.Where(d => d.IsActive == true).Select(d => new CustomerForBuyVM()
            {   
                Id= d.Id,
                Name = d.Name,
                PhoneNumber = d.PhoneNumber,
                Date = d.Date,
                Budget = d.Budget,
                Address = d.Address,
                FinalOpinion = d.FinalOpinion,
                FirstRecord = d.FirstRecord,
                HowToKnow = d.HowToKnow,
                SaleManagerOpinion = d.SaleManagerOpinion,
                SecondRecord = d.SecondRecord,



            }).ToListAsync();

            if (find == null)
            {
                return new Response<List<CustomerForBuyVM>>(404);
            }
            return new Response<List<CustomerForBuyVM>>(find);
        }
        public async Task<object> Detail(int id)
        {
            var owner = await _context.CustomerForBuys
                .Where(x => x.IsActive.Value && x.Id == id).Select(x => new
                {
                    x.Id,
                    x.Budget,
                    x.PhoneNumber,
                    x.Name,
                    x.FinalOpinion,
                    Date = x.Date.ToShortPersianDateString(false),
                    x.FirstRecord,
                    x.SecondRecord,
                    x.SaleManagerOpinion,
                    x.Address,
                    x.HowToKnow,
                    CreatedAt = x.CreatedAt.ToPersianDateTextify(false)

                }).FirstOrDefaultAsync();

            return owner;
        }
        public async Task<bool> DoesItExist(int id)
        {
            var result = await _context.CustomerForBuys.AnyAsync(x => x.IsActive.Value && x.Id == id);
            return result;
        }
    }
}
