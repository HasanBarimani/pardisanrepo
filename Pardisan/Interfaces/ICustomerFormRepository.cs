using Pardisan.ViewModels.API.CustomerForBuy;
using Pardisan.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pardisan.ViewModels.API.CustomerForm;

namespace Pardisan.Interfaces
{
    public interface ICustomerFormRepository
    {
        Task<Response<string>> Add(EditCustomerFormVM input);

        Task<Response<string>> Active(int id);
        Task<Response<string>> Disable(int id);
        Task<Response<string>> Update(EditCustomerFormVM input);
        Task<Response<EditCustomerFormVM>> GetById(int id);
        Task<Response<List<CustomerFormVM>>> GetAll();
        Task<object> Detail(int id);
        Task<bool> DoesItExist(int id);
    }
}
