using Pardisan.ViewModels;
using Pardisan.ViewModels.News;
using Pardisan.ViewModels.DataTable;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pardisan.ViewModels.Page;
using Pardisan.Models;

namespace Pardisan.Interfaces
{
    public interface IPageRepository
    {

        #region page
        Task<DatatableResponse> GetPagesDatatable(PagesDatatableInput input);
        Task<Response<PageIndexViewModel>> GetAllPages();
        Task<Response<string>> AddPages(UpsertPageVM input);
        Task<Response<string>> UpdatePages(UpsertPageVM input);
        Task<Response<PageVM>> GetPagesById(int id);
        Task<Response<string>> ActivePages(int id);
        Task<Response<string>> DisablePages(int id);
        Task<Page> GetPage(string pagename);
        #endregion


      

    }

}
