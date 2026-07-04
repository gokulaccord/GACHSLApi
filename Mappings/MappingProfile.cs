using AutoMapper;
using GACHSLApi.DTOs.SocietySettings;
using GACHSLApi.Entities;

namespace GACHSLApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SocietySettings, SocietySettingsDto>();

            CreateMap<UpdateSocietySettingsDto, SocietySettings>();
        }
    }
}