using System.Linq;
using CuttingEdge.Conditions;
using LogbookApi.Database;
using LogbookApi.Exceptions;

namespace LogbookApi.Providers.Implementation
{
    public class PageProvider : IPageProvider
    {
        private readonly jetstrea_LogbookEntities _context;

        public PageProvider(jetstrea_LogbookEntities context)
        {
            Condition.Requires(context, nameof(context)).IsNotNull();
            _context = context;
        }
        public Page GetPage(int page)
        {
            var result = _context.Page.FirstOrDefault(p => p.PageNumber == page);
            return result;
        }

        public Page SavePage(Page page)
        {
            Condition.Requires(page, nameof(page)).IsNotNull();

            if (!page.IsValid)
            {
                throw new InvalidPageException(page.Error);
            }

            return page;
        }

        public int GetLastPageNumber()
        {
            return _context.Page.Max(p => p.PageNumber);
        }
    }
}