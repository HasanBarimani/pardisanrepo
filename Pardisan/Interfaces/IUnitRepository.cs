using Pardisan.ViewModels.API.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Interfaces
{
    public interface IUnitRepository
    {
        Task<object> GetByProperty(int propertyId);
        Task<int> Create(CreateUnitVM input);
        Task<object> Detail(int id);
        Task Delete(int id);
        Task Edit(EditUnitVM input);
        Task<bool> DoesItExist(int id);
    }
}
