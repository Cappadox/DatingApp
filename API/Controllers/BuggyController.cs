using API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public class BuggyController : BaseController
  {
    private readonly DataContext _dataContext;

    public BuggyController(DataContext context)
    {
      _dataContext = context;
    }
    [Authorize]
    [HttpGet("auth")]
    public ActionResult<string> GetSecret()
    {
      return "secret text";
    }
    [HttpGet("not-found")]
    public ActionResult<string> GetNotFound()
    {
      return NotFound();
    }

    [HttpGet("server-error")]
    public ActionResult<string> GetServerError()
    {
      var thing = _dataContext.Users.Find(-1);
      var thingTo = thing.ToString();
      return thingTo;
    }

    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest()
    {
        return BadRequest("this is not a good request");
    }

  }
}