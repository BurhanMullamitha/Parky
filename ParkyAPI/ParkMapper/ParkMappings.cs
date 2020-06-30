using System;
using AutoMapper;
using ParkyAPI.Models;
using ParkyAPI.Models.Dtos;

namespace ParkyAPI.ParkMapper
{
  public class ParkMappings : Profile
  {
    public ParkMappings()
    {
      CreateMap<NationalPark, NationalParkDto>().ReverseMap();
      CreateMap<Trail, TrailDto>().ReverseMap();
      CreateMap<Trail, TrailUpdateDto>().ReverseMap();
      CreateMap<Trail, TrailCreateDto>().ReverseMap();
    }
  }
}
