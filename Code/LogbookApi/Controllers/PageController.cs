using System.Web.Http;

namespace LogbookApi.Controllers
{
    [RoutePrefix("api/Page")]
    public class PageController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Post(Page page)
        {
            return Created("here", page);
        }

        [HttpPut]
        public IHttpActionResult Put(Page page)
        {
            return Ok();
        }
    }
}
