
using AutoMapper;
using CommandsService.Dtos;
using CommandsService.Models;
using PlatformServices;

namespace CommandsService.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // Source --> target
            // platform
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformPublishedDto, Platform>()
                      .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.Id));

            // Commands
            CreateMap<CommandCreateDto, Command>();
            CreateMap<Command, CommandCreateDto>();

            // GRPC
            CreateMap<GrpcPlatformModel, Platform>()
                .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.PlatformId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Commands, opt => opt.Ignore());
        }
    }
}