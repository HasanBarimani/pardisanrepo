using Pardisan.Data;
using Pardisan.Interface;
using Pardisan.Interfaces;
using Pardisan.Models;
using Pardisan.ViewModels.DataTable;
using Pardisan.ViewModels.Estate;
using Pardisan.ViewModels;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DNTPersianUtils.Core;
using Pardisan.ViewModels.API.CustomerForSale;

namespace Pardisan.Services
{
    public class CustomerForSaleRepository : ICustomerForSaleRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerForSaleRepository(ApplicationDbContext context, IUploaderService uploaderService)
        {
            _context = context;
        }

        public async Task<Response<string>> Add(UpsertCustomerForSaleVM input)
        {
            var customerForSale = new CustomerForSale()
            {
                Name = input.Name,
                PhoneNumber = input.PhoneNumber,
                Passage = input.Passage,
                LandBar = input.LandBar,
                Usgae = input.Usgae,
                Demand = input.Demand,
                DocumentType = input.DocumentType,
                HowToKnow = input.HowToKnow,
                FinalOpinion = input.FinalOpinion,
                FirstRecord = input.FirstRecord,
                PropertyAddress = input.PropertyAddress,
                SalesManagerOpinion = input.SalesManagerOpinion,
                Meterage = input.Meterage,

                SecondRecord = input.SecondRecord,
                CreatedAt = DateTime.Now,
                Providers = input.Providers,

            };

            PersianCalendar pc = new PersianCalendar();
            DateTime dateStart = new DateTime(input.Date.Year, input.Date.Month, input.Date.Day, pc);


            //estate.Image = await FileManager.Images.Upload(PublicHelper.FilePath.EstateImagePath, input.Image);

            customerForSale.Date = dateStart;


            await _context.CustomerForSales.AddAsync(customerForSale);
            await _context.SaveChangesAsync();
            return new Response<string>(200);
        }
        public async Task<Response<string>> Active(int id)
        {
            var find = await _context.CustomerForSales.FirstOrDefaultAsync(d => d.Id == id);

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
            var find = await _context.CustomerForSales.FirstOrDefaultAsync(d => d.Id == id);

            if (find == null)
            {
                return new Response<string>(404);
            }

            find.IsActive = false;
            await _context.SaveChangesAsync();
            return new Response<string>(true, "غیرفعال شد");
        }

        public async Task<Response<UpsertCustomerForSaleVM>> GetById(int id)
        {
            try
            {
                var data = await _context.CustomerForSales.FirstOrDefaultAsync(d => d.Id == id);
                if (data == null)
                {
                    return new Response<UpsertCustomerForSaleVM>(404);
                }
                return new Response<UpsertCustomerForSaleVM>(CreateViewModelFromBlog(data));
            }
            catch (Exception)
            {
                return new Response<UpsertCustomerForSaleVM>("درخواست شما با مشکل مواجه شده است لطفا دوباره تلاش کنید");
            }
        }
        private UpsertCustomerForSaleVM CreateViewModelFromBlog(CustomerForSale input)
        {
            var viewModel = new UpsertCustomerForSaleVM()
            {
                Id = input.Id,
                Name = input.Name,
                PhoneNumber = input.PhoneNumber,
                Passage = input.Passage,
                Demand = input.Demand,
                DocumentType = input.DocumentType,
                FinalOpinion = input.FinalOpinion,
                FirstRecord = input.FirstRecord,
                HowToKnow = input.HowToKnow,
                LandBar = input.LandBar,
                Meterage = input.Meterage,
                PropertyAddress = input.PropertyAddress,
                Providers = input.Providers,
                SalesManagerOpinion = input.SalesManagerOpinion,
                SecondRecord = input.SecondRecord,
                Usgae = input.Usgae,
                DateForShow = input.Date.ToShortPersianDateString(),

            };

            return viewModel;
        }

        public async Task<Response<string>> Update(UpsertCustomerForSaleVM input)
        {
            var data = await _context.CustomerForSales.FirstOrDefaultAsync(d => d.Id == input.Id);
            if (data == null)
            {
                return new Response<string>(404);
            }
            data.Name = input.Name;
            data.PhoneNumber = input.PhoneNumber;
            data.Meterage = input.Meterage;
            data.LandBar = input.LandBar;
            data.Demand = input.Demand;
            data.FirstRecord = input.FirstRecord;
            data.SecondRecord = input.SecondRecord;
            data.HowToKnow = input.HowToKnow;
            data.PropertyAddress = input.PropertyAddress;
            data.Providers = input.Providers;
            data.Usgae = input.Usgae;
            data.SalesManagerOpinion = input.SalesManagerOpinion;
            data.FinalOpinion = input.FinalOpinion;
            data.Date = input.Date;
            data.UpdatedAt = DateTime.Now;
            data.DocumentType = input.DocumentType;
            data.Providers = input.Providers;


            PersianCalendar pc = new PersianCalendar();
            DateTime dateStart = new DateTime(input.Date.Year, input.Date.Month, input.Date.Day, pc);

            data.Date = dateStart;




            await _context.SaveChangesAsync();
            return new Response<string>(200);

        }

        public async Task<Response<List<CustomerForSaleVM>>> GetAll()
        {
            var find = await _context.CustomerForSales.Where(d => d.IsActive == true).Select(d => new CustomerForSaleVM()
            {
                Id= d.Id,
                Name = d.Name,
                PhoneNumber = d.PhoneNumber,
                Date = d.Date,
                Demand = d.Demand,
                DocumentType = d.DocumentType,
                FinalOpinion = d.FinalOpinion,
                FirstRecord = d.FirstRecord,
                HowToKnow = d.HowToKnow,
                LandBar = d.LandBar,
                Meterage = d.Meterage,
                Passage = d.Passage,
                PropertyAddress = d.PropertyAddress,
                Providers = d.Providers,
                SalesManagerOpinion = d.SalesManagerOpinion,
                SecondRecord = d.SecondRecord,
                Usgae = d.Usgae,

            }).ToListAsync();

            if (find == null)
            {
                return new Response<List<CustomerForSaleVM>>(404);
            }
            return new Response<List<CustomerForSaleVM>>(find);
        }
        public async Task<object> Detail(int id)
        {
            var owner = await _context.CustomerForSales
                .Where(x => x.IsActive.Value && x.Id == id).Select(x => new
                {
                    x.Id,
                    x.Passage,
                    x.PhoneNumber,
                    x.Name,
                    x.Usgae,
                    x.LandBar,
                    x.FinalOpinion,
                    Date = x.Date.ToShortPersianDateString(false),
                    x.FirstRecord,
                    x.SecondRecord,
                    x.DocumentType,
                    x.Demand,
                    x.Meterage,
                    x.PropertyAddress,
                    x.Providers,
                    x.SalesManagerOpinion,
                    x.HowToKnow,
                    CreatedAt = x.CreatedAt.ToPersianDateTextify(false)

                }).FirstOrDefaultAsync();

            return owner;
        }
        public async Task<bool> DoesItExist(int id)
        {
            var result = await _context.CustomerForSales.AnyAsync(x => x.IsActive.Value && x.Id == id);
            return result;
        }
    }
}
