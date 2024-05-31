using Pardisan.ViewModels;
using Pardisan.ViewModels.Blog;
using Pardisan.ViewModels.DataTable;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Interfaces
{
    public interface IBlogRepository
    {

        #region Blog
        Task<DatatableResponse> GetBlogDatatable(BlogDatatableInput input);
        Task<Response<BlogIndexViewModel>> GetAllBlog();
        Task<Response<string>> AddBlog(UpsertBlogVM input);
        Task<Response<string>> UpdateBlog(UpsertBlogVM input);
        Task<Response<BlogVM>> GetBlogById(int id);
        Task<Response<string>> ActiveBlog(int id);
        Task<Response<string>> DisableBlog(int id);
        #endregion


        #region Blog Category
        Task<DatatableResponse> GetBlogCategoryDatatable(BlogCategoryDatatableInput input);
        Task<Response<BlogCategoryIndexViewModel>> GetAllBlogCategory();
        Task<IEnumerable<SelectListItem>> CategoryListForDropdown();
        Task<Response<string>> AddBlogCategory(UpsertBlogCategoryVM input);
        Task<Response<string>> UpdateBlogCategory(UpsertBlogCategoryVM input);
        Task<Response<BlogCategoryVM>> GetBlogCategoryById(int id);
        Task<Response<string>> ActiveBlogCategory(int id);
        Task<Response<string>> DisableBlogCategory(int id);
        #endregion

    }

}
