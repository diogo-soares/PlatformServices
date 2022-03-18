using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformServices.Data;
using PlatformServices.Dtos;
using PlatformServices.Models;
using PlatformServices.SyncDataServices.Http;

namespace PlatformServices.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataCliente _commandDataClient;

        public PlatformsController(IPlatformRepo repository, IMapper mapper, ICommandDataCliente commandDataClient)
        {
            _repository = repository;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
        }

        [HttpGet("get-all")]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            var platformItem = _repository.GetallPlatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItem));
        }

        [HttpGet("{id}", Name = "GetPlatformsById")]
        public ActionResult<PlatformReadDto> GetPlatformsById(int id)
        {
            var platformItem = _repository.GetPlatformById(id);

            return platformItem == null ? NotFound() : Ok(_mapper.Map<PlatformReadDto>(platformItem));
        }

        public async ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            var platformModel = _mapper.Map<Platform>(platformCreateDto);
            _repository.CreatePlatform(platformModel);
            _repository.SaveChanges();

            var PlatformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

            try
            {
                await _commandDataClient.SendPlatformToCommand(PlatformReadDto);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"--> cloud not send sync: {ex.Message}");
            }

            return CreatedAtRoute(nameof(GetPlatformsById), new { Id = PlatformReadDto.Id }, PlatformReadDto);

        }
    }
}