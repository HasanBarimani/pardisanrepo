using System.Threading.Tasks;
using static Pardisan.Services.HelperService;

namespace Pardisan.Interfaces
{
    public interface IHelperService
    {
        Task<RootApi> CallApi(string apiUrl, string method, string authorization = "", string jsonData = "");

        Task<RootApi<TResponse>> CallApi<TResponse>(string apiUrl, string method, string authorization = "",
            string jsonData = "");
    }
}
