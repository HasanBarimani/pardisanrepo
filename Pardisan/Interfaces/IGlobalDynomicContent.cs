using Pardisan.ViewModels;
using Pardisan.ViewModels.DynamicContent;
using System.Threading.Tasks;

namespace Pardisan.Interfaces
{
    public interface IGlobalDynomicContent
    {
        Response<GlobalDynomicContentVM> GetAll();
    }
}
