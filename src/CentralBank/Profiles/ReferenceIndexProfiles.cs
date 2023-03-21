using AutoMapper;
using CentralBank.Dtos;
using CentralBank.Models;

namespace CentralBank.Profiles
{
    public class ReferenceIndexProfiles : Profile
    {
        public ReferenceIndexProfiles()
        {
            CreateMap<ReferenceIndex, ReferenceIndexReadDto>();
            CreateMap<ReferenceIndexCreateDto, ReferenceIndex>();
        }
    }
}
