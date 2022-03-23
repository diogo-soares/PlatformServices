using AutoMapper;
using PlatformServices.Dtos;
using PlatformServices.Models;

namespace PlatformServices.Profiles
{
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            // Source -> target
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformCreateDto, Platform>();

            CreateMap<PlatformReadDto, PlatformPublishedDto>();
        }
    }
}