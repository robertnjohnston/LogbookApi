using System.Threading.Tasks;
using LogbookApi.Database;
using LogbookApi.Models;

namespace LogbookApi.Providers
{
    public interface IPageProvider
    {
        Page GetPage(int page);

        Page SavePage(Page page);

        int GetLastPageNumber();
    }
}