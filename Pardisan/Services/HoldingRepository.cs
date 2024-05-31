using DNTPersianUtils.Core;
using Pardisan.Data;
using Pardisan.Interfaces;
using Pardisan.Models.Blog;
using Pardisan.ViewModels;
using Pardisan.ViewModels.Blog;
using Pardisan.ViewModels.DataTable;
using Pardisan.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pardisan.Interface;
using Pardisan.ViewModels.Holding;
using Pardisan.Models;
using Pardisan.ViewModels.Estate;
using Microsoft.AspNetCore.Hosting;
using static Pardisan.Extensions.PublicHelper;
using System.IO;
using Microsoft.Extensions.Hosting;

namespace Pardisan
{
    public class HoldingRepository : IHoldingRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUploaderService _uploaderService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HoldingRepository(ApplicationDbContext context, IUploaderService uploaderService, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _uploaderService = uploaderService;
            _webHostEnvironment = webHostEnvironment;
        }


        public async Task<Response<HoldingIndexViewModel>> GetAllList()
        {
            var findBlog = await _context.Holdings.Where(d => d.IsActive == true).Select(d => new HoldingVM()
            {
                Id = d.Id,
                Title = d.Title,
                Image = d.Image,
                IsActive = d.IsActive,
                CreatedAt = d.CreatedAt,

            }).ToListAsync();

            if (findBlog == null)
            {
                return new Response<HoldingIndexViewModel>(404);
            }

            var model = new HoldingIndexViewModel()
            {
                Holding = findBlog
            };

            return new Response<HoldingIndexViewModel>(model);
        }

        public async Task<DatatableResponse> GetDatatable(HoldingDatatableInput input)
        {
            var BlogObj = _context.Holdings.Select(d => new BlogDatatableResult()
            {
                Id = d.Id,
                Title = d.Title,
                Image = d.Image,
                IsActive = d.IsActive
            }).OrderByDescending(d => d.Id).AsQueryable();

            if (input.Deleted == true)
            {
                BlogObj = BlogObj.Where(w => w.IsActive == false);
            }
            else
            {
                BlogObj = BlogObj.Where(w => w.IsActive == true);
            }

            if (!string.IsNullOrEmpty(input.Search.Value))
            {
                BlogObj = BlogObj.Where(w =>
                    string.IsNullOrEmpty(w.Title) || w.Title.Contains(input.Search.Value)
                );
            }
            DatatableResponse response = new DatatableResponse
            {
                ITotalDisplayRecords = BlogObj.Count(),
                ITotalRecords = _context.Holdings.Count(),
                AaData = await BlogObj.Skip(input.Start).Take(input.Length).ToListAsync(),
                SEcho = 0
            };
            return response;
        }

        public async Task<Response<string>> Add(UpsertHoldingVM input)
        {
            var BlogModel = new Holding()
            {
                Title = input.Title,
                CreatedAt = DateTime.Now,
                AparatLink = input.AparatLink,
                History = input.History,
                UpdatedAt = DateTime.Now,
                Lat = input.Lat,
                Long = input.Long,

            };

            //add image with wartemark and commperssion
            var host = _webHostEnvironment.WebRootPath;
            var path = FilePath.HodlingImagePath;
            var pathWaterMark = FilePath.WaterMarkImagePath;
            var pathShow = FilePath.HodlingImagePathForShow;
            BlogModel.Image = pathShow + FileUploader.UploadImage(input.Image, Path.Combine(host, path), Path.Combine(host, pathWaterMark), false, compression: 30).result;


            await _context.Holdings.AddAsync(BlogModel);
            await _context.SaveChangesAsync();
            if (input.Files != null)
            {
                foreach (var i in input.Files)
                {
                    var image = new Pardisan.Models.HoldingImage
                    {
                        Date = DateTime.Now,
                        HoldingId = BlogModel.Id,
                        Url = pathShow + FileUploader.UploadImage(i, Path.Combine(host, path), Path.Combine(host, pathWaterMark), false, compression: 35).result

                    };
                    _context.HoldingImages.Add(image);
                    _context.SaveChanges();
                }
            }
            return new Response<string>(200);
        }

        public async Task<Response<string>> Update(UpsertHoldingVM input)
        {
            var BlogModel = await _context.Holdings.FirstOrDefaultAsync(d => d.Id == input.Id);
            if (BlogModel == null)
            {
                return new Response<string>(404);
            }
            BlogModel.Title = input.Title;
            BlogModel.UpdatedAt = DateTime.Now;
            BlogModel.AparatLink = input.AparatLink;
            BlogModel.History = input.History;
            BlogModel.Lat = input.Lat;
            BlogModel.Long = input.Long;
            var host = _webHostEnvironment.WebRootPath;
            var path = FilePath.HodlingImagePath;
            var pathWaterMark = FilePath.WaterMarkImagePath;
            var pathShow = FilePath.HodlingImagePathForShow;
            if (input.Image != null && input.Image.Length > 0)
            {
               
                BlogModel.Image = pathShow + FileUploader.UploadImage(input.Image, Path.Combine(host, path), Path.Combine(host, pathWaterMark), false, compression: 30).result;

            }

            await _context.SaveChangesAsync();
            if (input.Files != null)
            {
                foreach (var i in input.Files)
                {
                    var image = new Pardisan.Models.HoldingImage
                    {
                        Date = DateTime.Now,
                        HoldingId = BlogModel.Id,
                        Url = pathShow + FileUploader.UploadImage(i, Path.Combine(host, path), Path.Combine(host, pathWaterMark), false, compression: 35).result

                    };
                    _context.HoldingImages.Add(image);
                    _context.SaveChanges();
                }
            }
            return new Response<string>(200);

        }

        public async Task<Response<UpsertHoldingVM>> GetById(int BlogId)
        {
            try
            {
                var BlogObj = await _context.Holdings.FirstOrDefaultAsync(d => d.Id == BlogId);
                if (BlogObj == null)
                {
                    return new Response<UpsertHoldingVM>(404);
                }
                return new Response<UpsertHoldingVM>(CreateViewModelFrom(BlogObj));
            }
            catch (Exception)
            {
                return new Response<UpsertHoldingVM>("درخواست شما با مشکل مواجه شده است لطفا دوباره تلاش کنید");
            }
        }

        public async Task<Response<string>> Active(int BlogId)
        {
            var findBlog = await _context.Holdings.FirstOrDefaultAsync(d => d.Id == BlogId);

            if (findBlog == null)
            {
                return new Response<string>(404);
            }

            findBlog.IsActive = true;
            await _context.SaveChangesAsync();
            return new Response<string>(true, "فعال شد");
        }

        public async Task<Response<string>> Disable(int BlogId)
        {
            var findBlog = await _context.Holdings.FirstOrDefaultAsync(d => d.Id == BlogId);

            if (findBlog == null)
            {
                return new Response<string>(404);
            }

            findBlog.IsActive = false;
            await _context.SaveChangesAsync();
            return new Response<string>(true, "غیرفعال شد");
        }

        private UpsertHoldingVM CreateViewModelFrom(Holding Blog)
        {
            var viewModel = new UpsertHoldingVM()
            {
                Id = Blog.Id,
                Title = Blog.Title,
                AparatLink = Blog.AparatLink,
                History = Blog.History,
                UpdatedAt = Blog.UpdatedAt,
                ImageForShow = Blog.Image,
                IsActive = Blog.IsActive,
                CreatedAt = Blog.CreatedAt,
                Lat = Blog.Lat,
                Long = Blog.Long,

            };
            var listVM = new HoldingForShowVM()
            {
                PropertyId = viewModel.Id,
                Images = _context.HoldingImages.Where(x => x.HoldingId == viewModel.Id).ToList(),

            };
            viewModel.GalleryIameges = listVM;
            return viewModel;
        }

        public async Task<Response<List<HoldingVM>>> GetAll()
        {

            var find = await _context.Holdings.Where(d => d.IsActive == true).Select(d => new HoldingVM()
            {
                Id = d.Id,
                Title = d.Title,
                Image = d.Image,
                CreatedAt = d.CreatedAt,


            }).ToListAsync();

            if (find == null)
            {
                return new Response<List<HoldingVM>>(404);
            }
            return new Response<List<HoldingVM>>(find);
        }

        public async Task<DatatableResponse> GetGallery(HoldingImagesDatatableInput input)
        {
            var data = _context.HoldingImages.Where(x => x.HoldingId == input.Id).AsQueryable();
            DatatableResponse response = new DatatableResponse
            {
                ITotalDisplayRecords = data.Count(),
                ITotalRecords = _context.HoldingImages.Where(x => x.HoldingId == input.Id).Count(),
                AaData = await data.Skip(input.Start).Take(input.Length).ToListAsync(),
                SEcho = 0
            };
            return response;
        }
        public async Task<Response<string>> DeleteImage(int id)
        {
            var find = await _context.HoldingImages.FirstOrDefaultAsync(d => d.Id == id);

            if (find == null)
            {
                return new Response<string>(404);
            }



            _context.HoldingImages.Remove(find);
            await _context.SaveChangesAsync();
            return new Response<string>(true, "با موفقیت حذف شد");
        }
        public async Task<Response<string>> InsertImages(HoldingImageVm input)
        {
            foreach (var i in input.Item)
            {
                var image = new Pardisan.Models.EstateImage
                {
                    Date = DateTime.Now,
                    EstateId = input.HoldingId,
                    Url = await _uploaderService.SimpleUpload(i, "/Img/Property/")
                };
                _context.EstateImages.Add(image);
                _context.SaveChanges();
            }
            return new Response<string>(200);



        }

    }
}
