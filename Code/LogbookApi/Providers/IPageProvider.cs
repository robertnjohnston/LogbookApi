using System.Threading.Tasks;
using LogbookApi.Models;

namespace LogbookApi.Providers
{
    public interface IPageProvider
    {
        Task<Page> GetPage(int page);

        Task SavePage(Page page);
    }
}