using Pardisan.ViewModels.DataTable;
using Pardisan.ViewModels.Estate;
using Pardisan.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pardisan.ViewModels.Aparat;

namespace Pardisan.Interfaces
{
    public interface IAparatRepository
    {
        Task<Response<string>> Add(UpsertAparatVM input);
        Task<DatatableResponse> GetDatatable(AparatDatatableInput input);
        Task<Response<string>> Active(int id);
        Task<Response<string>> Disable(int id);
        Task<Response<string>> Update(UpsertAparatVM input);
        Task<Response<UpsertAparatVM>> GetById(int id);
        Task<Response<List<AparatVM>>> GetAll();
    }
}
