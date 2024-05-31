using Pardisan.ViewModels.DataTable;
using Pardisan.ViewModels.Estate;
using Pardisan.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pardisan.ViewModels.API.CustomerForSale;

namespace Pardisan.Interfaces
{
    public interface ICustomerForSaleRepository
    {
        Task<Response<string>> Add(UpsertCustomerForSaleVM input);

        Task<Response<string>> Active(int id);
        Task<Response<string>> Disable(int id);
        Task<Response<string>> Update(UpsertCustomerForSaleVM input);
        Task<Response<UpsertCustomerForSaleVM>> GetById(int id);
        Task<Response<List<CustomerForSaleVM>>> GetAll();
        Task<object> Detail(int id);
        Task<bool> DoesItExist(int id);
    }
}
