using AutoMapper;
using GACHSLApi.DTOs.Meeting;
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

            CreateMap<Meeting, MeetingDto>();

            CreateMap<CreateMeetingDto, Meeting>();

            CreateMap<UpdateMeetingDto, Meeting>();

        }

    }
}