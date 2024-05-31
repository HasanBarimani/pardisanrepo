using Pardisan.ViewModel.Feature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Interfaces
{
    public interface IFeatureRepository
    {
        Task Create(CreateFeatureVM input);
        Task<object> GetAll();
        Task<object> Detail(int id);
        Task<bool> DoesItExist(int id);
        Task Edit(EditFeatureVM input);
        Task Delete(int id);
    }
}
