using AutoMapper;
using GACHSLApi.Common;
using GACHSLApi.DTOs.SocietySettings;
using GACHSLApi.Interfaces;

namespace GACHSLApi.Services
{
    public class SocietySettingsService : ISocietySettingsService
    {
        private readonly ISocietySettingsRepository _repository;
        private readonly IMapper _mapper;

        public SocietySettingsService(
            ISocietySettingsRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<SocietySettingsDto>> GetAsync()
        {
            var entity = await _repository.GetAsync();

            if (entity == null)
            {
                return new ApiResponse<SocietySettingsDto>(
                    false,
                    "Society settings not found.",
                    null);
            }

            var dto = _mapper.Map<SocietySettingsDto>(entity);

            return new ApiResponse<SocietySettingsDto>(
                true,
                "Society settings retrieved successfully.",
                dto);
        }

        public async Task<ApiResponse<SocietySettingsDto>> UpdateAsync(UpdateSocietySettingsDto dto)
        {
            var entity = await _repository.GetAsync();

            if (entity == null)
            {
                return new ApiResponse<SocietySettingsDto>(
                    false,
                    "Society settings not found.",
                    null);
            }

            _mapper.Map(dto, entity);

            entity.UpdatedOn = DateTime.UtcNow;

            var updated = await _repository.UpdateAsync(entity);

            var result = _mapper.Map<SocietySettingsDto>(updated);

            return new ApiResponse<SocietySettingsDto>(
                true,
                "Society settings updated successfully.",
                result);
        }
    }
}