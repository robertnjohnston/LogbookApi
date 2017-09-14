using System.Threading.Tasks;
using CuttingEdge.Conditions;
using LogbookApi.Models;

namespace LogbookApi.Providers.Implementation
{
    public class PageProvider : IPageProvider
    {
        private jetstrea_LogbookEntities _context;

        public PageProvider(jetstrea_LogbookEntities context)
        {
            Condition.Requires(context, nameof(context)).IsNotNull();
            _context = context;
        }
        public Task<Page> GetPage(int page)
        {
            throw new System.NotImplementedException();
        }

        public Task SavePage(Page page)
        {
            throw new System.NotImplementedException();
        }
    }
}