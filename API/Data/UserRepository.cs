using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.CommonMessages;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class UserRepository : IUserRepository
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public UserRepository(DataContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }
    public async Task<IEnumerable<AppUser>> GetUserListAsync()
    {
      return await _context.Users.ToListAsync();
    }

    public async Task<AppUser> GetUserByIdAsync(int id)
    {
      return await _context.Users.FindAsync(id);
    }

    public async Task<AppUser> GetUserByUserNameAsync(string userName)
    {
      return await _context.Users.FirstOrDefaultAsync(a => a.UserName == userName);
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }

    public void Update(AppUser user)
    {
      _context.Entry(user).State = EntityState.Modified;
    }

    public async Task<MemberDTO> GetMemberAsync(string username)
    {
      var member = await _context.Users.Where(x => x.UserName == username).
      ProjectTo<MemberDTO>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();

      return member;
    }

    public async Task<IEnumerable<MemberDTO>> GetMembersAsync()
    {
      var members = await _context.Users.ProjectTo<MemberDTO>(_mapper.ConfigurationProvider).ToListAsync();
      return members;
    }
  }
}