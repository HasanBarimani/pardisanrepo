using Microsoft.EntityFrameworkCore;
using Pardisan.Models;
using Pardisan.ViewModels;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System;
using Pardisan.Data;
using Pardisan.ViewModels.API.CustomerForm;
using DNTPersianUtils.Core;
using System.Linq;
using Pardisan.Interfaces;

namespace Pardisan.Services
{
    public class CustomerFormRepository:ICustomerFormRepository
    {
        private readonly ApplicationDbContext _context;
        public CustomerFormRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<string>> Add(EditCustomerFormVM input)
        {
            var customerForm = new CustomerForm()
            {
                Name = input.Name,
                PhoneNumer = input.PhoneNumer,
                CallSubject = input.CallSubject,
                Content = input.Content,
                Date = input.Date,
                Transactions = input.Transactions,
                HowToKnow = input.HowToKnow,
                CreatedAt = DateTime.Now,



            };

            PersianCalendar pc = new PersianCalendar();
            DateTime dateStart = new DateTime(input.Date.Year, input.Date.Month, input.Date.Day, pc);


            //estate.Image = await FileManager.Images.Upload(PublicHelper.FilePath.EstateImagePath, input.Image);

            customerForm.Date = dateStart;


            await _context.CustomerForms.AddAsync(customerForm);
            await _context.SaveChangesAsync();
            return new Response<string>(200);
        }
        public async Task<Response<string>> Active(int id)
        {
            var find = await _context.CustomerForms.FirstOrDefaultAsync(d => d.Id == id);

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
            var find = await _context.CustomerForms.FirstOrDefaultAsync(d => d.Id == id);

            if (find == null)
            {
                return new Response<string>(404);
            }

            find.IsActive = false;
            await _context.SaveChangesAsync();
            return new Response<string>(true, "غیرفعال شد");
        }

        public async Task<Response<EditCustomerFormVM>> GetById(int id)
        {
            try
            {
                var data = await _context.CustomerForms.FirstOrDefaultAsync(d => d.Id == id);
                if (data == null)
                {
                    return new Response<EditCustomerFormVM>(404);
                }
                return new Response<EditCustomerFormVM>(CreateViewModelFromBlog(data));
            }
            catch (Exception)
            {
                return new Response<EditCustomerFormVM>("درخواست شما با مشکل مواجه شده است لطفا دوباره تلاش کنید");
            }
        }
        private EditCustomerFormVM CreateViewModelFromBlog(CustomerForm input)
        {
            var viewModel = new EditCustomerFormVM()
            {
                Id = input.Id,
                Name = input.Name,
                PhoneNumer = input.PhoneNumer,
                HowToKnow = input.HowToKnow,
                CallSubject = input.CallSubject,
                Transactions = input.Transactions,
                Content = input.Content,
                DateForShow = input.Date.ToShortPersianDateString(),

            };

            return viewModel;
        }

        public async Task<Response<string>> Update(EditCustomerFormVM input)
        {
            var data = await _context.CustomerForms.FirstOrDefaultAsync(d => d.Id == input.Id);
            if (data == null)
            {
                return new Response<string>(404);
            }
            data.Name = input.Name;
            data.PhoneNumer = input.PhoneNumer;
            data.HowToKnow = input.HowToKnow;
            data.CallSubject = input.CallSubject;
            data.Content = input.Content;
            data.Date = input.Date;
            data.Transactions = input.Transactions;
            data.UpdatedAt = DateTime.Now;



            PersianCalendar pc = new PersianCalendar();
            DateTime dateStart = new DateTime(input.Date.Year, input.Date.Month, input.Date.Day, pc);
            data.Date = dateStart;




            await _context.SaveChangesAsync();
            return new Response<string>(200);

        }

        public async Task<Response<List<CustomerFormVM>>> GetAll()
        {
            var find = await _context.CustomerForms.Where(d => d.IsActive == true).Select(d => new CustomerFormVM()
            {
                Id= d.Id,
                Name = d.Name,
                Transactions = d.Transactions,
                CallSubject = d.CallSubject,
                Content = d.Content,
                PhoneNumer = d.PhoneNumer,
                Date = d.Date,
                HowToKnow = d.HowToKnow
            }).ToListAsync();

            if (find == null)
            {
                return new Response<List<CustomerFormVM>>(404);
            }
            return new Response<List<CustomerFormVM>>(find);
        }
        public async Task<object> Detail(int id)
        {
            var owner = await _context.CustomerForms
                .Where(x => x.IsActive.Value && x.Id == id).Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.PhoneNumer,
                    x.CallSubject,
                    x.Transactions,
                    Date = x.Date.ToShortPersianDateString(false),
                    x.Content,
                    x.HowToKnow,
                    CreatedAt = x.CreatedAt.ToPersianDateTextify(false)

                }).FirstOrDefaultAsync();

            return owner;
        }
        public async Task<bool> DoesItExist(int id)
        {
            var result = await _context.CustomerForms.AnyAsync(x => x.IsActive.Value && x.Id == id);
            return result;
        }
    }
}
