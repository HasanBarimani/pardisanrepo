using Pardisan.ViewModels;
using Pardisan.ViewModels.Blog;
using Pardisan.ViewModels.DataTable;
using Pardisan.ViewModels.Estate;
using Pardisan.ViewModels.Holding;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pardisan.Interfaces
{
    public interface IHoldingRepository
    {
        Task<Response<List<HoldingVM>>> GetAll();
        Task<DatatableResponse> GetDatatable(HoldingDatatableInput input);
        Task<Response<HoldingIndexViewModel>> GetAllList();
        Task<Response<string>> Add(UpsertHoldingVM input);
        Task<Response<string>> Update(UpsertHoldingVM input);
        Task<Response<UpsertHoldingVM>> GetById(int id);
        Task<Response<string>> Active(int id);
        Task<Response<string>> Disable(int id);
        Task<DatatableResponse> GetGallery(HoldingImagesDatatableInput input);
        Task<Response<string>> DeleteImage(int id);
        Task<Response<string>> InsertImages(HoldingImageVm input);


    }
}
