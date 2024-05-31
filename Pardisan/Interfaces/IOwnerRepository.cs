using Pardisan.ViewModels;
using Pardisan.ViewModels.API.Owner;
using Pardisan.ViewModels.Owner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Interfaces
{
    public interface IOwnerRepository
    {
        Task<object> GetAll();
        Task<int> Create(CreateOwnerVM input);
        Task<object> Detail(int id);
        Task<object> GetOwnerProperties(int id);
        Task<bool> DoesItExist(int id);
        Task<bool> IsPhoneNumberAlreadyInSystem(string phoneNumber);
        Task Edit(EditOwnerVM input);
        Task Delete(int id);
        Task<object> GetForDashboard();
    }
}
