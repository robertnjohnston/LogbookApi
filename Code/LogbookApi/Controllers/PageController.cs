using System.Web.Http;

namespace LogbookApi.Controllers
{
    [RoutePrefix("api/Page")]
    public class PageController : ApiController
    {
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            return Ok();
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
