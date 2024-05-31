using Pardisan.ViewModels;
using Pardisan.ViewModels.API.Property;
using Pardisan.ViewModels.Blog;
using Pardisan.ViewModels.DataTable;
using Pardisan.ViewModels.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Interfaces
{
    public interface IEstateRepository
    {
        Task<Response<string>> Add(UpsertEstateVM input);
        Task<Response<string>> InsertImages(EstateImageVM input);
        Task<DatatableResponse> GetDatatable(EstateDatatableInput input);
        Task<Response<string>> Active(int id);
        Task<Response<string>> Disable(int id);
        Task<Response<string>> Update(UpsertEstateVM input);
        Task<Response<UpsertEstateVM>> GetById(int id);
        Task<Response<List<EstateVM>>> GetAll();
        Task<DatatableResponse> GetGallery(EstateImagesDatatableInput input);
        Task<Response<string>> DeleteImage(int id);

    }
}
