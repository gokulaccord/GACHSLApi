using AutoMapper;
using GACHSLApi.DTOs.SocietySettings;
using GACHSLApi.Entities;
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

        public async Task<SocietySettingsDto?> GetAsync()
        {
            var entity = await _repository.GetAsync();

            if (entity == null)
                return null;

            return _mapper.Map<SocietySettingsDto>(entity);
        }

        public async Task<SocietySettingsDto?> UpdateAsync(UpdateSocietySettingsDto dto)
        {
            var entity = await _repository.GetAsync();

            if (entity == null)
                return null;

            // Update existing entity from DTO
            _mapper.Map(dto, entity);

            entity.UpdatedOn = DateTime.UtcNow;

            var updated = await _repository.UpdateAsync(entity);

            return _mapper.Map<SocietySettingsDto>(updated);
        }
    }
}