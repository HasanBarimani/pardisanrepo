using Pardisan.Data;
using Pardisan.Interfaces;
using Pardisan.ViewModels;
using Pardisan.ViewModels.DataTable;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Pardisan.ViewModels.Page;
using Pardisan.Models;

namespace Pardisan
{
    public class PagesRepository : IPageRepository
    {
        private readonly ApplicationDbContext _context;


        public PagesRepository(ApplicationDbContext context)
        {
            _context = context;

        }

        #region page
        public async Task<Response<PageIndexViewModel>> GetAllPages()
        {
            var findPages = await _context.Pages.Where(d => d.IsActive == true).Select(d => new PageVM()
            {
                Id = d.Id,
                Title = d.Title,
                Content = d.Content,
                Url = d.Url,
                IsActive = d.IsActive,
                CreatedAt = d.CreatedAt
            }).ToListAsync();

            if (findPages == null)
            {
                return new Response<PageIndexViewModel>(404);
            }

            var model = new PageIndexViewModel()
            {
                Pages = findPages
            };

            return new Response<PageIndexViewModel>(model);
        }

        public async Task<DatatableResponse> GetPagesDatatable(PagesDatatableInput input)
        {
            var PageObj = _context.Pages.Select(d => new PagesDatatableResult()
            {
                Id = d.Id,
                Title = d.Title,
                Content = d.Content,
                Url = d.Url,
                IsActive = d.IsActive
            }).OrderByDescending(d => d.Id).AsQueryable();

            if (input.Deleted == true)
            {
                PageObj = PageObj.Where(w => w.IsActive == false);
            }
            else
            {
                PageObj = PageObj.Where(w => w.IsActive == true);
            }

            if (!string.IsNullOrEmpty(input.Search.Value))
            {
                PageObj = PageObj.Where(w =>
                    string.IsNullOrEmpty(w.Title) || w.Title.Contains(input.Search.Value)
                );
            }
            DatatableResponse response = new DatatableResponse
            {
                ITotalDisplayRecords = PageObj.Count(),
                ITotalRecords = _context.Pages.Count(),
                AaData = await PageObj.Skip(input.Start).Take(input.Length).ToListAsync(),
                SEcho = 0
            };
            return response;
        }

        public async Task<Response<string>> AddPages(UpsertPageVM input)
        {
            var pageModel = new Page()
            {
                Title = input.Title,
                Url = input.Url,
                Content = input.Content,

                CreatedAt = DateTime.Now,
            };

            //newsModel.Image = await FileManager.Images.Upload(PublicHelper.FilePath.NewsImagePath, input.Image);


            await _context.Pages.AddAsync(pageModel);
            await _context.SaveChangesAsync();
            return new Response<string>(200);
        }

        public async Task<Response<string>> UpdatePages(UpsertPageVM input)
        {
            var newsModel = await _context.Pages.FirstOrDefaultAsync(d => d.Id == input.Id);
            if (newsModel == null)
            {
                return new Response<string>(404);
            }
            newsModel.Title = input.Title;
            newsModel.Url = input.Url;
            newsModel.Content = input.Content;

            newsModel.UpdatedAt = DateTime.Now;


            await _context.SaveChangesAsync();
            return new Response<string>(200);

        }

        public async Task<Response<PageVM>> GetPagesById(int pageId)
        {
            try
            {
                var pageObj = await _context.Pages.FirstOrDefaultAsync(d => d.Id == pageId);
                if (pageObj == null)
                {
                    return new Response<PageVM>(404);
                }
                return new Response<PageVM>(CreateViewModelFromNews(pageObj));
            }
            catch (Exception)
            {
                return new Response<PageVM>("درخواست شما با مشکل مواجه شده است لطفا دوباره تلاش کنید");
            }
        }

        public async Task<Response<string>> ActivePages(int pageId)
        {
            var findPages = await _context.Pages.FirstOrDefaultAsync(d => d.Id == pageId);

            if (findPages == null)
            {
                return new Response<string>(404);
            }

            findPages.IsActive = true;
            await _context.SaveChangesAsync();
            return new Response<string>(true, "فعال شد");
        }

        public async Task<Response<string>> DisablePages(int newsId)
        {
            var findPage = await _context.Pages.FirstOrDefaultAsync(d => d.Id == newsId);

            if (findPage == null)
            {
                return new Response<string>(404);
            }

            findPage.IsActive = false;
            await _context.SaveChangesAsync();
            return new Response<string>(true, "غیرفعال شد");
        }

        private PageVM CreateViewModelFromNews(Page news)
        {
            var viewModel = new PageVM()
            {
                Id = news.Id,
                Title = news.Title,

                Content = news.Content,
                Url = news.Url,
                IsActive = news.IsActive,
                CreatedAt = news.CreatedAt,

            };

            return viewModel;
        }

        public async Task<Page> GetPage(string pagename)
        {
            var page = await _context.Pages.FirstOrDefaultAsync(x => x.Url == pagename && x.IsActive.Value);
            return page;
        }
        #endregion


    }
}
