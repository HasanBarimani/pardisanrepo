using DNTPersianUtils.Core;
using Pardisan.Data;
using Pardisan.Interfaces;
using Pardisan.Models.News;
using Pardisan.ViewModels;
using Pardisan.ViewModels.News;
using Pardisan.ViewModels.DataTable;
using Pardisan.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pardisan.Interface;

namespace Pardisan
{
    public class NewsRepository : INewsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUploaderService _uploaderService;

        public NewsRepository(ApplicationDbContext context, IUploaderService uploaderService)
        {
            _context = context;
            _uploaderService = uploaderService;
        }

        #region News
        public async Task<Response<NewsIndexViewModel>> GetAllNews()
        {
            var findNews = await _context.News.Where(d => d.IsActive == true).Select(d => new NewsVM()
            {
                Id = d.Id,
                Title = d.Title,
                Describtion = d.Describtion,
                Content = d.Content,
                Image =  d.Image,
                Tags = d.Tags,
                CategoryTitle = d.Category.Title,
                IsActive = d.IsActive,
                CreatedAt = d.CreatedAt
            }).ToListAsync();

            if (findNews == null)
            {
                return new Response<NewsIndexViewModel>(404);
            }

            var model = new NewsIndexViewModel()
            {
                News = findNews
            };

            return new Response<NewsIndexViewModel>(model);
        }

        public async Task<DatatableResponse> GetNewsDatatable(NewsDatatableInput input)
        {
            var newsObj = _context.News.Select(d => new NewsDatatableResult()
            {
                Id = d.Id,
                Title = d.Title,
                Describtion = d.Describtion,
                Image = d.Image,
                IsActive = d.IsActive
            }).OrderByDescending(d => d.Id).AsQueryable();

            if (input.Deleted == true)
            {
                newsObj = newsObj.Where(w => w.IsActive == false);
            }
            else
            {
                newsObj = newsObj.Where(w => w.IsActive == true);
            }

            if (!string.IsNullOrEmpty(input.Search.Value))
            {
                newsObj = newsObj.Where(w =>
                    string.IsNullOrEmpty(w.Title) || w.Title.Contains(input.Search.Value)
                );
            }
            DatatableResponse response = new DatatableResponse
            {
                ITotalDisplayRecords = newsObj.Count(),
                ITotalRecords = _context.News.Count(),
                AaData = await newsObj.Skip(input.Start).Take(input.Length).ToListAsync(),
                SEcho = 0
            };
            return response;
        }

        public async Task<Response<string>> AddNews(UpsertNewsVM input)
        {
            var newsModel = new News()
            {
                Title = input.Title,
                Describtion = input.Describtion,
                Content = input.Content,
                Tags = input.Tags,
                CategoryId = input.CategoryId,
                CreatedAt = DateTime.Now,
            };

            //newsModel.Image = await FileManager.Images.Upload(PublicHelper.FilePath.NewsImagePath, input.Image);
            newsModel.Image = await _uploaderService.SimpleUpload(input.Image, "/Img/News/");

            await _context.News.AddAsync(newsModel);
            await _context.SaveChangesAsync();
            return new Response<string>(200);
        }

        public async Task<Response<string>> UpdateNews(UpsertNewsVM input)
        {
            var newsModel = await _context.News.FirstOrDefaultAsync(d => d.Id == input.Id);
            if (newsModel == null)
            {
                return new Response<string>(404);
            }
            newsModel.Title = input.Title;
            newsModel.Describtion = input.Describtion;
            newsModel.Content = input.Content;
            newsModel.Tags = input.Tags;
            newsModel.CategoryId = input.CategoryId;
            newsModel.UpdatedAt = DateTime.Now;
            if (input.Image != null && input.Image.Length > 0)
            {
                //newsModel.Image = await FileManager.Images.Update(newsModel.Image, PublicHelper.FilePath.NewsImagePath, input.Image);
                newsModel.Image = await _uploaderService.SimpleUpload(input.Image, "/Img/News/");
            }

            await _context.SaveChangesAsync();
            return new Response<string>(200);

        }

        public async Task<Response<NewsVM>> GetNewsById(int newsId)
        {
            try
            {
                var newsObj = await _context.News.FirstOrDefaultAsync(d => d.Id == newsId);
                if (newsObj == null)
                {
                    return new Response<NewsVM>(404);
                }
                return new Response<NewsVM>(CreateViewModelFromNews(newsObj));
            }
            catch (Exception)
            {
                return new Response<NewsVM>("درخواست شما با مشکل مواجه شده است لطفا دوباره تلاش کنید");
            }
        }

        public async Task<Response<string>> ActiveNews(int newsId)
        {
            var findNews = await _context.News.FirstOrDefaultAsync(d => d.Id == newsId);

            if (findNews == null)
            {
                return new Response<string>(404);
            }

            findNews.IsActive = true;
            await _context.SaveChangesAsync();
            return new Response<string>(true, "فعال شد");
        }

        public async Task<Response<string>> DisableNews(int newsId)
        {
            var findNews = await _context.News.FirstOrDefaultAsync(d => d.Id == newsId);

            if (findNews == null)
            {
                return new Response<string>(404);
            }

            findNews.IsActive = false;
            await _context.SaveChangesAsync();
            return new Response<string>(true, "غیرفعال شد");
        }

        private NewsVM CreateViewModelFromNews(News news)
        {
            var viewModel = new NewsVM()
            {
                Id = news.Id,
                Title = news.Title,
                Describtion = news.Describtion,
                Content = news.Content,
                Image = news.Image,
                Tags = news.Tags,
                IsActive = news.IsActive,
                CreatedAt = news.CreatedAt,
                CategoryId = news.CategoryId,
            };

            return viewModel;
        }
        #endregion

        #region Category
        public async Task<DatatableResponse> GetNewsCategoryDatatable(NewsCategoryDatatableInput input)
        {
            var findCategory = _context.NewsCategories.Select(d => new NewsCategoryDatatableResult()
            {
                Id = d.Id,
                Title = d.Title,
                IsActive = d.IsActive,
                CreatedAt = d.CreatedAt.ToPersianDateTextify(true),
            });
            if (input.Deleted == true)
            {
                findCategory = findCategory.Where(w => w.IsActive == false);
            }
            else
            {
                findCategory = findCategory.Where(w => w.IsActive == true);
            }
            if (!string.IsNullOrEmpty(input.Search.Value))
            {
                findCategory = findCategory.Where(w =>
                    string.IsNullOrEmpty(w.Title) || w.Title.Contains(input.Search.Value)
                );
            }
            DatatableResponse response = new DatatableResponse
            {
                ITotalDisplayRecords = findCategory.Count(),
                ITotalRecords = _context.NewsCategories.Count(),
                AaData = await findCategory.Skip(input.Start).Take(input.Length).ToListAsync(),
                SEcho = 0
            };
            return response;
        }

        public async Task<Response<NewsCategoryIndexViewModel>> GetAllNewsCategory()
        {
            var category = await _context.NewsCategories.Where(d => d.IsActive == true).Select(d => new NewsCategoryVM()
            {
                Id = d.Id,
                Title = d.Title,
                IsActive = d.IsActive

            }).ToListAsync();

            if (category == null)
            {
                return new Response<NewsCategoryIndexViewModel>(404);
            }
            var model = new NewsCategoryIndexViewModel()
            {
                NewsCategory = category
            };

            return new Response<NewsCategoryIndexViewModel>(model);
        }

        public async Task<IEnumerable<SelectListItem>> CategoryListForDropdown()
        {
            var categoryList = await _context.NewsCategories.Where(n => (bool)n.IsActive).Select(i => new NewsVM()
            {
                Id = i.Id,
                Title = i.Title,
            }).ToListAsync();

            return categoryList.Select(i => new SelectListItem()
            {
                Text = i.Title,
                Value = i.Id.ToString()
            }).ToList();
        }

        public async Task<Response<string>> AddNewsCategory(UpsertNewsCategoryVM input)
        {
            var newsModel = new NewsCategory()
            {
                Title = input.Title,
                CreatedAt = DateTime.Now
            };

            await _context.NewsCategories.AddAsync(newsModel);
            await _context.SaveChangesAsync();
            return new Response<string>(200);
        }

        public async Task<Response<string>> UpdateNewsCategory(UpsertNewsCategoryVM input)
        {
            var category = await _context.NewsCategories.FirstOrDefaultAsync(d => d.Id == input.Id);
            if (category == null)
            {
                return new Response<string>(404);
            }
            category.Title = input.Title;
            category.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return new Response<string>(200);
        }

        public async Task<Response<NewsCategoryVM>> GetNewsCategoryById(int categoryId)
        {
            try
            {
                var categoryObj = await _context.NewsCategories.FirstOrDefaultAsync(d => d.Id == categoryId);
                if (categoryObj == null)
                {
                    return new Response<NewsCategoryVM>(404);
                }
                return new Response<NewsCategoryVM>(CreateViewModelFromNewsCategory(categoryObj));
            }
            catch (Exception)
            {
                return new Response<NewsCategoryVM>("درخواست شما با مشکل مواجه شده است لطفا دوباره تلاش کنید");
            }
        }

        public async Task<Response<string>> ActiveNewsCategory(int categoryId)
        {
            var category = await _context.NewsCategories.FirstOrDefaultAsync(d => d.Id == categoryId);
            if (category == null)
            {
                return new Response<string>(404);
            }

            category.IsActive = true;
            await _context.SaveChangesAsync();
            return new Response<string>(true, "فعال شد");
        }

        public async Task<Response<string>> DisableNewsCategory(int categoryId)
        {
            var category = await _context.NewsCategories.FirstOrDefaultAsync(d => d.Id == categoryId);
            if (category == null)
            {
                return new Response<string>(404);
            }

            category.IsActive = false;
            await _context.SaveChangesAsync();
            return new Response<string>(true, "غیرفعال شد");
        }

        private NewsCategoryVM CreateViewModelFromNewsCategory(NewsCategory category)
        {
            var viewModel = new NewsCategoryVM()
            {
                Id = category.Id,
                Title = category.Title,
                IsActive = category.IsActive,
                CreatedAt = category.CreatedAt
            };

            return viewModel;
        }

        #endregion
    }
}
