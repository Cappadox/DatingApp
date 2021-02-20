using System.Collections.Generic;
using System.Threading.Tasks;
using API.CommonMessages;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class UserController : BaseController
  {
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public UserController(IUserRepository userRepository, IMapper mapper)
    {
      _mapper = mapper;
      _userRepository = userRepository;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()
    {
      var users = await _userRepository.GetMembersAsync();
      return Ok(users);
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<MemberDTO>> GetUser(string userName)
    {
      var user = await _userRepository.GetMemberAsync(userName);
      return Ok(user);
    }
  }
}