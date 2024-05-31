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

namespace Pardisan
{
    public class BlogRepository : IBlogRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUploaderService _uploaderService;
        public BlogRepository(ApplicationDbContext context, IUploaderService uploaderService)
        {
            _context = context;
            _uploaderService = uploaderService;
        }

        #region Blog
        public async Task<Response<BlogIndexViewModel>> GetAllBlog()
        {
            var findBlog = await _context.Blog.Where(d => d.IsActive == true).Select(d => new BlogVM()
            {
                Id = d.Id,
                Title = d.Title,
                Describtion = d.Describtion,
                Content = d.Content,
                Image = d.Image,
                Tags = d.Tags,
                CategoryTitle = d.Category.Title,
                IsActive = d.IsActive,
                CreatedAt = d.CreatedAt
            }).ToListAsync();

            if (findBlog == null)
            {
                return new Response<BlogIndexViewModel>(404);
            }

            var model = new BlogIndexViewModel()
            {
                Blog = findBlog
            };

            return new Response<BlogIndexViewModel>(model);
        }

        public async Task<DatatableResponse> GetBlogDatatable(BlogDatatableInput input)
        {
            var BlogObj = _context.Blog.Select(d => new BlogDatatableResult()
            {
                Id = d.Id,
                Title = d.Title,
                Describtion = d.Describtion,
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
                ITotalRecords = _context.Blog.Count(),
                AaData = await BlogObj.Skip(input.Start).Take(input.Length).ToListAsync(),
                SEcho = 0
            };
            return response;
        }

        public async Task<Response<string>> AddBlog(UpsertBlogVM input)
        {
            var BlogModel = new Blog()
            {
                Title = input.Title,
                Describtion = input.Describtion,
                Content = input.Content,
                Tags = input.Tags,
                CategoryId = input.CategoryId,
                CreatedAt = DateTime.Now,
            };

            //BlogModel.Image = await FileManager.Images.Upload(PublicHelper.FilePath.BlogImagePath, input.Image);
            BlogModel.Image = await _uploaderService.SimpleUpload(input.Image, "/Img/Blog/");

            await _context.Blog.AddAsync(BlogModel);
            await _context.SaveChangesAsync();
            return new Response<string>(200);
        }

        public async Task<Response<string>> UpdateBlog(UpsertBlogVM input)
        {
            var BlogModel = await _context.Blog.FirstOrDefaultAsync(d => d.Id == input.Id);
            if (BlogModel == null)
            {
                return new Response<string>(404);
            }
            BlogModel.Title = input.Title;
            BlogModel.Describtion = input.Describtion;
            BlogModel.Content = input.Content;
            BlogModel.Tags = input.Tags;
            BlogModel.CategoryId = input.CategoryId;
            BlogModel.UpdatedAt = DateTime.Now;
            if (input.Image != null && input.Image.Length > 0)
            {
                //BlogModel.Image = await FileManager.Images.Update(BlogModel.Image, PublicHelper.FilePath.BlogImagePath, input.Image);
                BlogModel.Image = await _uploaderService.SimpleUpload(input.Image, "/Img/Blog/");
            }

            await _context.SaveChangesAsync();
            return new Response<string>(200);

        }

        public async Task<Response<BlogVM>> GetBlogById(int BlogId)
        {
            try
            {
                var BlogObj = await _context.Blog.Include(x=>x.Category).FirstOrDefaultAsync(d => d.Id == BlogId);
                if (BlogObj == null)
                {
                    return new Response<BlogVM>(404);
                }
                return new Response<BlogVM>(CreateViewModelFromBlog(BlogObj));
            }
            catch (Exception)
            {
                return new Response<BlogVM>("درخواست شما با مشکل مواجه شده است لطفا دوباره تلاش کنید");
            }
        }

        public async Task<Response<string>> ActiveBlog(int BlogId)
        {
            var findBlog = await _context.Blog.FirstOrDefaultAsync(d => d.Id == BlogId);

            if (findBlog == null)
            {
                return new Response<string>(404);
            }

            findBlog.IsActive = true;
            await _context.SaveChangesAsync();
            return new Response<string>(true, "فعال شد");
        }

        public async Task<Response<string>> DisableBlog(int BlogId)
        {
            var findBlog = await _context.Blog.FirstOrDefaultAsync(d => d.Id == BlogId);

            if (findBlog == null)
            {
                return new Response<string>(404);
            }

            findBlog.IsActive = false;
            await _context.SaveChangesAsync();
            return new Response<string>(true, "غیرفعال شد");
        }

        private BlogVM CreateViewModelFromBlog(Blog Blog)
        {
            var viewModel = new BlogVM()
            {
                Id = Blog.Id,
                Title = Blog.Title,
                Describtion = Blog.Describtion,
                Content = Blog.Content,
                Image =  Blog.Image,
                Tags = Blog.Tags,
                IsActive = Blog.IsActive,
                CreatedAt = Blog.CreatedAt,
                CategoryId = Blog.CategoryId,
                CategoryTitle = Blog.Category.Title
            };

            return viewModel;
        }
        #endregion

        #region Category
        public async Task<DatatableResponse> GetBlogCategoryDatatable(BlogCategoryDatatableInput input)
        {
            var findCategory = _context.BlogCategories.Select(d => new BlogCategoryDatatableResult()
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
                ITotalRecords = _context.BlogCategories.Count(),
                AaData = await findCategory.Skip(input.Start).Take(input.Length).ToListAsync(),
                SEcho = 0
            };
            return response;
        }

        public async Task<Response<BlogCategoryIndexViewModel>> GetAllBlogCategory()
        {
            var category = await _context.BlogCategories.Where(d => d.IsActive == true).Select(d => new BlogCategoryVM()
            {
                Id = d.Id,
                Title = d.Title,
                IsActive = d.IsActive

            }).ToListAsync();

            if (category == null)
            {
                return new Response<BlogCategoryIndexViewModel>(404);
            }
            var model = new BlogCategoryIndexViewModel()
            {
                BlogCategory = category
            };

            return new Response<BlogCategoryIndexViewModel>(model);
        }

        public async Task<IEnumerable<SelectListItem>> CategoryListForDropdown()
        {
            var categoryList = await _context.BlogCategories.Where(n => (bool)n.IsActive).Select(i => new BlogVM()
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

        public async Task<Response<string>> AddBlogCategory(UpsertBlogCategoryVM input)
        {
            var BlogModel = new BlogCategory()
            {
                Title = input.Title,
                CreatedAt = DateTime.Now
            };

            await _context.BlogCategories.AddAsync(BlogModel);
            await _context.SaveChangesAsync();
            return new Response<string>(200);
        }

        public async Task<Response<string>> UpdateBlogCategory(UpsertBlogCategoryVM input)
        {
            var category = await _context.BlogCategories.FirstOrDefaultAsync(d => d.Id == input.Id);
            if (category == null)
            {
                return new Response<string>(404);
            }
            category.Title = input.Title;
            category.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return new Response<string>(200);
        }

        public async Task<Response<BlogCategoryVM>> GetBlogCategoryById(int categoryId)
        {
            try
            {
                var categoryObj = await _context.BlogCategories.FirstOrDefaultAsync(d => d.Id == categoryId);
                if (categoryObj == null)
                {
                    return new Response<BlogCategoryVM>(404);
                }
                return new Response<BlogCategoryVM>(CreateViewModelFromBlogCategory(categoryObj));
            }
            catch (Exception)
            {
                return new Response<BlogCategoryVM>("درخواست شما با مشکل مواجه شده است لطفا دوباره تلاش کنید");
            }
        }

        public async Task<Response<string>> ActiveBlogCategory(int categoryId)
        {
            var category = await _context.BlogCategories.FirstOrDefaultAsync(d => d.Id == categoryId);
            if (category == null)
            {
                return new Response<string>(404);
            }

            category.IsActive = true;
            await _context.SaveChangesAsync();
            return new Response<string>(true, "فعال شد");
        }

        public async Task<Response<string>> DisableBlogCategory(int categoryId)
        {
            var category = await _context.BlogCategories.FirstOrDefaultAsync(d => d.Id == categoryId);
            if (category == null)
            {
                return new Response<string>(404);
            }

            category.IsActive = false;
            await _context.SaveChangesAsync();
            return new Response<string>(true, "غیرفعال شد");
        }

        private BlogCategoryVM CreateViewModelFromBlogCategory(BlogCategory category)
        {
            var viewModel = new BlogCategoryVM()
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
