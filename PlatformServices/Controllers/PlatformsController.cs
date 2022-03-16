using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformServices.Data;
using PlatformServices.Dtos;
using PlatformServices.Models;

namespace PlatformServices.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
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

        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
           var platformModel = _mapper.Map<Platform>(platformCreateDto);
           _repository.CreatePlatform(platformModel);
           _repository.SaveChanges();

           var PlatformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

           return CreatedAtRoute(nameof(GetPlatformsById), new { Id = PlatformReadDto.Id }, PlatformReadDto);

        } 
    }
} 