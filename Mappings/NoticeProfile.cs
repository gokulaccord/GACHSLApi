using AutoMapper;
using GACHSLApi.DTOs;
using GACHSLApi.Entities;

namespace GACHSLApi.Mapping
{
    public class NoticeProfile : Profile
    {
        public NoticeProfile()
        {
            CreateMap<Notice, NoticeDto>();

            CreateMap<CreateNoticeDto, Notice>();

            CreateMap<UpdateNoticeDto, Notice>();
        }
    }
}