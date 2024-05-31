using Microsoft.AspNetCore.Hosting;
using Pardisan.Data;
using Pardisan.Interface;
using Pardisan.Interfaces;
using Pardisan.Models;
using Pardisan.ViewModels.DataTable;
using Pardisan.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Pardisan.ViewModels.Aparat;

namespace Pardisan.Services
{
    public class AparatRepository : IAparatRepository

    {
        private readonly ApplicationDbContext _context;
        private readonly IUploaderService _uploaderService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AparatRepository(ApplicationDbContext context, IUploaderService uploaderService, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _uploaderService = uploaderService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Response<string>> Add(UpsertAparatVM input)
        {
            var estate = new Aparat()
            {
                Title = input.Title,
                CreatedAt = DateTime.Now,
                AparatLink = input.AparatLink,
                Code = input.Code,
            };

            //estate.Image = await FileManager.Images.Upload(PublicHelper.FilePath.EstateImagePath, input.Image);
            //estate.Image = await _uploaderService.SimpleUpload(input.Image, "/Img/Estate/");


            await _context.Aparats.AddAsync(estate);
            await _context.SaveChangesAsync();

            return new Response<string>(200);
        }
        public async Task<DatatableResponse> GetDatatable(AparatDatatableInput input)
        {
            var data = _context.Aparats.Select(d => new AparatDatatableResult()
            {
                Id = d.Id,
                Title = d.Title,
                Code = d.Code,
                IsActive = d.IsActive,
            }).OrderByDescending(d => d.Id).AsQueryable();

            if (input.Deleted == true)
            {
                data = data.Where(w => w.IsActive == false);
            }
            else
            {
                data = data.Where(w => w.IsActive == true);
            }

            if (!string.IsNullOrEmpty(input.Search.Value))
            {
                data = data.Where(w =>
                    string.IsNullOrEmpty(w.Title) || w.Title.Contains(input.Search.Value)
                );
            }
            DatatableResponse response = new DatatableResponse
            {
                ITotalDisplayRecords = data.Count(),
                ITotalRecords = _context.Aparats.Count(),
                AaData = await data.Skip(input.Start).Take(input.Length).ToListAsync(),
                SEcho = 0
            };
            return response;
        }
        public async Task<Response<string>> Active(int id)
        {
            var find = await _context.Aparats.FirstOrDefaultAsync(d => d.Id == id);

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
            var find = await _context.Aparats.FirstOrDefaultAsync(d => d.Id == id);

            if (find == null)
            {
                return new Response<string>(404);
            }

            find.IsActive = false;
            await _context.SaveChangesAsync();
            return new Response<string>(true, "غیرفعال شد");
        }

        public async Task<Response<UpsertAparatVM>> GetById(int id)
        {
            try
            {
                var data = await _context.Aparats.FirstOrDefaultAsync(d => d.Id == id);
                if (data == null)
                {
                    return new Response<UpsertAparatVM>(404);
                }
                return new Response<UpsertAparatVM>(CreateViewModelFromBlog(data));
            }
            catch (Exception)
            {
                return new Response<UpsertAparatVM>("درخواست شما با مشکل مواجه شده است لطفا دوباره تلاش کنید");
            }
        }
        private UpsertAparatVM CreateViewModelFromBlog(Aparat estate)
        {

            var viewModel = new UpsertAparatVM()
            {
                Id = estate.Id,
                Title = estate.Title,
                Code = estate.Code,
                AparatLink = estate.AparatLink,

            };




            return viewModel;
        }

        public async Task<Response<string>> Update(UpsertAparatVM input)
        {
            var data = await _context.Aparats.FirstOrDefaultAsync(d => d.Id == input.Id);
            if (data == null)
            {
                return new Response<string>(404);
            }
            data.Title = input.Title;
            data.Code = input.Code;
            data.AparatLink = input.AparatLink;
            data.UpdatedAt = DateTime.Now;




            await _context.SaveChangesAsync();

            return new Response<string>(200);

        }

        public async Task<Response<List<AparatVM>>> GetAll()
        {

            var find = await _context.Aparats.Where(d => d.IsActive == true).Select(d => new AparatVM()
            {
                Id = d.Id,
                Title = d.Title,
                CreatedAt = d.CreatedAt,
                AparatLink = d.AparatLink,
                Code = d.Code,


            }).OrderBy(x => x.Code).ToListAsync();

            if (find == null)
            {
                return new Response<List<AparatVM>>(404);
            }
            return new Response<List<AparatVM>>(find);
        }



    }
}
