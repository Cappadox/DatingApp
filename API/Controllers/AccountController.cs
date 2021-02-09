using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.CommonMessages;
using API.Data;
using API.Entities;
using API.Interfaces;
using API.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  public class AccountController : BaseController
  {
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;

    public AccountController(DataContext context, ITokenService tokenService)
    {
      _context = context;
      _tokenService = tokenService;
    }
    [HttpPost("register")]
    public async Task<ActionResult<UserDTO>> Register(RegisterRequest request)
    {
      if (await UserExists(request.UserName)) return BadRequest("Username exist");

      using var hmac = new HMACSHA512();
      var user = new AppUser
      {
        UserName = request.UserName.ToLower(),
        PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
        PasswordSalt = hmac.Key
      };
      _context.Users.Add(user);
      await _context.SaveChangesAsync();

      var userDTO = new UserDTO
      {
        UserName = user.UserName,
        Token = _tokenService.CreateToken(user)
      };
      return userDTO;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> Login(LoginRequest request)
    {

      var user = await _context.Users.SingleOrDefaultAsync(a => a.UserName == request.UserName);
      if (user == null) return Unauthorized("invalid username");
      using var hmac = new HMACSHA512(user.PasswordSalt);
      var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
      for (int i = 0; i < computedHash.Length; i++)
      {
        if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("invalid password");

      }
      var userDTO = new UserDTO
      {
        UserName = user.UserName,
        Token = _tokenService.CreateToken(user)
      };
      return userDTO;
    }
    private async Task<bool> UserExists(string userName)
    {
      return await _context.Users.AnyAsync(a => a.UserName == userName.ToLower());
    }

  }
}