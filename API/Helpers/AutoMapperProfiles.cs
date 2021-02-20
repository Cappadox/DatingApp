using System.Linq;
using API.CommonMessages;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {
      CreateMap<AppUser,MemberDTO>().ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(sourceMember => sourceMember.Photos.
         FirstOrDefault(x => x.IsMain == true).Url));
      CreateMap<Photo, PhotoDTO>();
    }
  }
}