using System.Threading.Tasks;
using System.Web.Http;
using LogbookApi.Database;
using LogbookApi.Providers;

namespace LogbookApi.Controllers
{
    [RoutePrefix("api/Page")]
    public class PageController : ApiController
    {
        private readonly IPageProvider _pageProvider;
        public PageController(IPageProvider pageProvider)
        {
            _pageProvider = pageProvider;
        }
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            var result =  _pageProvider.GetPage(id);

            return Ok(result);
        }

        [HttpPost]
        [Route("Save")]
        public IHttpActionResult Post(Page page)
        {
            return Created("here", page);
        }

        [HttpPut]
        [Route("Update")]
        public IHttpActionResult Put(Page page)
        {
            return Ok();
        }
    }
}
