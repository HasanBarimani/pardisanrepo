using Pardisan.ViewModels;
using Pardisan.ViewModels.News;
using Pardisan.ViewModels.DataTable;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Interfaces
{
    public interface INewsRepository
    {

        #region News
        Task<DatatableResponse> GetNewsDatatable(NewsDatatableInput input);
        Task<Response<NewsIndexViewModel>> GetAllNews();
        Task<Response<string>> AddNews(UpsertNewsVM input);
        Task<Response<string>> UpdateNews(UpsertNewsVM input);
        Task<Response<NewsVM>> GetNewsById(int id);
        Task<Response<string>> ActiveNews(int id);
        Task<Response<string>> DisableNews(int id);
        #endregion


        #region News Category
        Task<DatatableResponse> GetNewsCategoryDatatable(NewsCategoryDatatableInput input);
        Task<Response<NewsCategoryIndexViewModel>> GetAllNewsCategory();
        Task<IEnumerable<SelectListItem>> CategoryListForDropdown();
        Task<Response<string>> AddNewsCategory(UpsertNewsCategoryVM input);
        Task<Response<string>> UpdateNewsCategory(UpsertNewsCategoryVM input);
        Task<Response<NewsCategoryVM>> GetNewsCategoryById(int id);
        Task<Response<string>> ActiveNewsCategory(int id);
        Task<Response<string>> DisableNewsCategory(int id);
        #endregion

    }

}
