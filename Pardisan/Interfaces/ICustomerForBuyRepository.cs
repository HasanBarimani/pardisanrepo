using Pardisan.ViewModels.API.CustomerForSale;
using Pardisan.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pardisan.ViewModels.API.CustomerForBuy;

namespace Pardisan.Interfaces
{
    public interface ICustomerForBuyRepository
    {
        Task<Response<string>> Add(EditCustomerForBuyVM input);

        Task<Response<string>> Active(int id);
        Task<Response<string>> Disable(int id);
        Task<Response<string>> Update(EditCustomerForBuyVM input);
        Task<Response<EditCustomerForBuyVM>> GetById(int id);
        Task<Response<List<CustomerForBuyVM>>> GetAll();
        Task<object> Detail(int id);
        Task<bool> DoesItExist(int id);
    }
}
