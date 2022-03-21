using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CommandsService.Dtos;
using CommandsService.Models;

namespace CommandsService.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // Source --> target
            // platform
            CreateMap<Platform, PlatformReadDto>();

            // Commands
            CreateMap<CommandCreateDto, Command>();
            CreateMap<Command, CommandCreateDto>();
        }
    }
}