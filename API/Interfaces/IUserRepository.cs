using System.Collections.Generic;
using System.Threading.Tasks;
using API.CommonMessages;
using API.Entities;

namespace API.Interfaces
{
  public interface IUserRepository
  {
    void Update(AppUser user);
    Task<bool> SaveAllAsync();
    Task<IEnumerable<AppUser>> GetUserListAsync();
    Task<AppUser> GetUserByIdAsync(int id);
    Task<AppUser> GetUserByUserNameAsync(string userName);
    Task<MemberDTO> GetMemberAsync(string username);
    Task<IEnumerable<MemberDTO>> GetMembersAsync();
  }
}