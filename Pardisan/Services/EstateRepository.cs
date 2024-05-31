using Microsoft.EntityFrameworkCore;
using Pardisan.Data;
using Pardisan.Extensions;
using Pardisan.Interfaces;
using Pardisan.Models;
using Pardisan.Models.Blog;
using Pardisan.ViewModels;
using Pardisan.ViewModels.Blog;
using Pardisan.ViewModels.DataTable;
using Pardisan.ViewModels.Estate;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DNTPersianUtils.Core;
using Pardisan.Interface;
using Pardisan.Migrations;
using Pardisan.ViewModels.API.Property;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Security.Policy;
using Microsoft.AspNetCore.Components.Forms;
using Pardisan.Extention;
using static Pardisan.Extensions.PublicHelper;

namespace Pardisan.Services
{
    public class EstateRepository : IEstateRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUploaderService _uploaderService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EstateRepository(ApplicationDbContext context, IUploaderService uploaderService, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _uploaderService = uploaderService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Response<string>> Add(UpsertEstateVM input)
        {
            var estate = new Estate()
            {
                Title = input.Title,
                Description = input.Description,
                EstateMeterage = input.EstateMeterage,
                FloorCount = input.FloorCount,
                UnitInFlorCount = input.UnitInFlorCount,
                TotalUnits = input.TotalUnits,
                Province = input.Province,
                UsageType = input.UsageType,
                Status = input.Status,
                CreatedAt = DateTime.Now,
                AparatLink = input.AparatLink,
                Code = input.Code,
                Lat = input.Lat,
                Long = input.Long,
                AbNama = input.AbNama,
                Alachigh = input.Alachigh,
                Camera = input.Camera,
                Fibr = input.Fibr,
                Flower = input.Flower,
                QRCode = input.QRCode,
                Security = input.Security,
                Shomineh = input.Shomineh,



            };

            PersianCalendar pc = new PersianCalendar();
            DateTime dateStart = new DateTime(input.StartDate.Year, input.StartDate.Month, input.StartDate.Day, pc);
            DateTime dateComplete = new DateTime(input.ProjectCompletionDate.Year, input.ProjectCompletionDate.Month, input.ProjectCompletionDate.Day, pc);
            //estate.Image = await _uploaderService.SimpleUpload(input.Image, "/Img/Estate/");
            var host = _webHostEnvironment.WebRootPath;
            var path = FilePath.EstateImagePath;
            var pathWaterMark = FilePath.WaterMarkImagePath;
            var pathShow = FilePath.EstateImagePathForShow;
            estate.Image = pathShow + FileUploader.UploadImage(input.Image, Path.Combine(host, path), Path.Combine(host, pathWaterMark), true, compression: 30).result;

            //estate.Image = await FileManager.Images.Upload(PublicHelper.FilePath.EstateImagePath, input.Image);
            //estate.Image = await _uploaderService.SimpleUpload(input.Image, "/Img/Estate/");
            estate.StartDate = dateStart;
            estate.ProjectCompletionDate = dateComplete;

            await _context.Estates.AddAsync(estate);
            await _context.SaveChangesAsync();
            if (input.Files != null)
            {
                foreach (var i in input.Files)
                {
                    var image = new Pardisan.Models.EstateImage
                    {
                        Date = DateTime.Now,
                        EstateId = estate.Id,
                        Url = pathShow + FileUploader.UploadImage(i, Path.Combine(host, path), Path.Combine(host, pathWaterMark), true, compression: 35).result

                    };

                    //await _uploaderService.SimpleUpload(i, "/Img/Property/")


                    _context.EstateImages.Add(image);
                    _context.SaveChanges();
                }
            }
            return new Response<string>(200);
        }
        public async Task<DatatableResponse> GetDatatable(EstateDatatableInput input)
        {
            var data = _context.Estates.Select(d => new EstateDatatableResult()
            {
                Id = d.Id,
                Title = d.Title,
                FloorCount = d.FloorCount,
                UsageType = d.UsageType,
                Province = d.Province,
                Image = d.Image,
                Code = d.Code,
                IsActive = d.IsActive,
                Status = d.Status
            }).OrderByDescending(d => d.Code).AsQueryable();

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
                ITotalRecords = _context.Blog.Count(),
                AaData = await data.Skip(input.Start).Take(input.Length).ToListAsync(),
                SEcho = 0
            };
            return response;
        }
        public async Task<Response<string>> Active(int id)
        {
            var find = await _context.Estates.FirstOrDefaultAsync(d => d.Id == id);

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
            var find = await _context.Estates.FirstOrDefaultAsync(d => d.Id == id);

            if (find == null)
            {
                return new Response<string>(404);
            }

            find.IsActive = false;
            await _context.SaveChangesAsync();
            return new Response<string>(true, "غیرفعال شد");
        }

        public async Task<Response<UpsertEstateVM>> GetById(int id)
        {
            try
            {
                var data = await _context.Estates.Include(x => x.Images).FirstOrDefaultAsync(d => d.Id == id);
                if (data == null)
                {
                    return new Response<UpsertEstateVM>(404);
                }
                return new Response<UpsertEstateVM>(CreateViewModelFromBlog(data));
            }
            catch (Exception)
            {
                return new Response<UpsertEstateVM>("درخواست شما با مشکل مواجه شده است لطفا دوباره تلاش کنید");
            }
        }
        private UpsertEstateVM CreateViewModelFromBlog(Estate estate)
        {

            var viewModel = new UpsertEstateVM()
            {
                Id = estate.Id,
                Title = estate.Title,
                Code = estate.Code,
                Description = estate.Description,
                ImageShow = estate.Image,
                EstateMeterage = estate.EstateMeterage,
                FloorCount = estate.FloorCount,
                UnitInFlorCount = estate.UnitInFlorCount,
                TotalUnits = estate.TotalUnits,
                Province = estate.Province,
                ProjectCompletionDate = estate.ProjectCompletionDate,
                CompletionDateForShow = estate.ProjectCompletionDate.ToShortPersianDateString(),
                UsageType = estate.UsageType,
                Status = estate.Status,
                StartDate = estate.StartDate,
                StartDateForShow = estate.StartDate.ToShortPersianDateString(),
                AparatLink = estate.AparatLink,
                Long = estate.Long,
                Lat = estate.Lat,
                AbNama = estate.AbNama,
                Alachigh = estate.Alachigh,
                Camera = estate.Camera,
                Fibr = estate.Fibr,
                Flower = estate.Flower,
                QRCode = estate.QRCode,
                Security = estate.Security,
                Shomineh = estate.Shomineh,


            };

            var listVM = new EstateForShowVM()
            {
                PropertyId = estate.Id,
                Images = _context.EstateImages.Where(x => x.EstateId == estate.Id).ToList(),

            };
            var img = new EstateImage()
            {
                EstateId = estate.Id,
                Url = viewModel.ImageShow,
                Id = estate.Id,


            };
            listVM.Images.Add(img);

            viewModel.GalleryIameges = listVM;


            return viewModel;
        }

        public async Task<Response<string>> Update(UpsertEstateVM input)
        {
            var data = await _context.Estates.Include(x => x.Images).FirstOrDefaultAsync(d => d.Id == input.Id);
            if (data == null)
            {
                return new Response<string>(404);
            }
            data.Title = input.Title;
            data.Code = input.Code;
            data.Description = input.Description;
            data.EstateMeterage = input.EstateMeterage;
            data.FloorCount = input.FloorCount;
            data.UnitInFlorCount = input.UnitInFlorCount;
            data.TotalUnits = input.TotalUnits;
            data.Province = input.Province;
            data.ProjectCompletionDate = input.ProjectCompletionDate;
            data.UsageType = input.UsageType;
            data.AparatLink = input.AparatLink;
            data.Status = input.Status;
            data.UpdatedAt = DateTime.Now;
            data.Lat = input.Lat;
            data.Long = input.Long;
            data.AbNama = input.AbNama;
            data.Alachigh = input.Alachigh;
            data.Camera = input.Camera;
            data.Fibr = input.Fibr;
            data.Flower = input.Flower;
            data.QRCode = input.QRCode;
            data.Security = input.Security;
            data.Shomineh = input.Shomineh;



            PersianCalendar pc = new PersianCalendar();
            DateTime dateStart = new DateTime(input.StartDate.Year, input.StartDate.Month, input.StartDate.Day, pc);
            DateTime dateComplete = new DateTime(input.ProjectCompletionDate.Year, input.ProjectCompletionDate.Month, input.ProjectCompletionDate.Day, pc);

            data.StartDate = dateStart;
            data.ProjectCompletionDate = dateComplete;


            var host = _webHostEnvironment.WebRootPath;
            var path = FilePath.EstateImagePath;
            var pathWaterMark = FilePath.WaterMarkImagePath;
            var pathShow = FilePath.EstateImagePathForShow;
            if (input.Image != null && input.Image.Length > 0)
            {
                if (!string.IsNullOrWhiteSpace(data.Image))
                {
                    FileUploader.DeleteFile(Path.Combine(host, data.Image?.Substring(1)));
                }

                data.Image = pathShow + FileUploader.UploadImage(input.Image, Path.Combine(host, path), Path.Combine(host, pathWaterMark), true, compression: 30).result;

                //data.Image = await FileManager.Images.Update(data.Image, PublicHelper.FilePath.EstateImagePath, input.Image);
                //data.Image = await _uploaderService.SimpleUpload(input.Image, "/Img/Estate/");
            }

            await _context.SaveChangesAsync();
            if (input.Files != null)
            {
                foreach (var i in input.Files)
                {
                    var image = new Pardisan.Models.EstateImage
                    {
                        Date = DateTime.Now,
                        EstateId = data.Id,
                        Url = pathShow + FileUploader.UploadImage(i, Path.Combine(host, path), Path.Combine(host, pathWaterMark), true, compression: 35).result
                    };
                    _context.EstateImages.Add(image);
                    _context.SaveChanges();
                }
            }
            return new Response<string>(200);

        }

        public async Task<Response<List<EstateVM>>> GetAll()
        {

            var find = await _context.Estates.Where(d => d.IsActive == true).Select(d => new EstateVM()
            {
                Id = d.Id,
                Title = d.Title,
                Image = d.Image,
                Address = d.Province,
                Status = d.Status,
                CreatedAt = d.CreatedAt,
                Code = d.Code,


            }).OrderByDescending(x => x.Code).ToListAsync();

            if (find == null)
            {
                return new Response<List<EstateVM>>(404);
            }
            return new Response<List<EstateVM>>(find);
        }
        public async Task<DatatableResponse> GetGallery(EstateImagesDatatableInput input)
        {
            var data = _context.EstateImages.Where(x => x.EstateId == input.Id).AsQueryable();
            DatatableResponse response = new DatatableResponse
            {
                ITotalDisplayRecords = data.Count(),
                ITotalRecords = _context.EstateImages.Where(x => x.EstateId == input.Id).Count(),
                AaData = await data.Skip(input.Start).Take(input.Length).ToListAsync(),
                SEcho = 0
            };
            return response;
        }
        public async Task<Response<string>> DeleteImage(int id)
        {
            var find = await _context.EstateImages.FirstOrDefaultAsync(d => d.Id == id);

            if (find == null)
            {
                return new Response<string>(404);
            }



            _context.EstateImages.Remove(find);
            await _context.SaveChangesAsync();
            return new Response<string>(true, "با موفقیت حذف شد");
        }
        public async Task<Response<string>> InsertImages(EstateImageVM input)
        {
            foreach (var i in input.Item)
            {
                var image = new Pardisan.Models.EstateImage
                {
                    Date = DateTime.Now,
                    EstateId = input.PropertyId,
                    Url = await _uploaderService.SimpleUpload(i, "/Img/Property/")
                };
                _context.EstateImages.Add(image);
                _context.SaveChanges();
            }
            return new Response<string>(200);



        }

    }
}
