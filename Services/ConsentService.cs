using GACHSLApi.Common;
using GACHSLApi.DTOs.Consent;
using GACHSLApi.Entities;
using GACHSLApi.Interfaces;

namespace GACHSLApi.Services
{
    public class ConsentService : IConsentService
    {
        private readonly IConsentRepository _consentRepository;

        public ConsentService(IConsentRepository consentRepository)
        {
            _consentRepository = consentRepository;
        }

        public async Task<ApiResponse<List<ConsentDto>>> GetAllAsync()
        {
            var consents = await _consentRepository.GetAllAsync();

            var result = consents.Select(c => new ConsentDto
            {
                ConsentId = c.ConsentId,
                MemberId = c.MemberId,
                MemberName = c.Member?.FullName ?? "",
                FlatNumber = c.Member?.FlatNumber ?? "",
                ConsentStatus = c.ConsentStatus,
                ConsentDate = c.ConsentDate,
                Remarks = c.Remarks,
                DocumentId = c.DocumentId,
                IsActive = c.IsActive
            }).ToList();

            return new ApiResponse<List<ConsentDto>>(true, "Success", result);
        }

        public async Task<ApiResponse<ConsentDto>> GetByIdAsync(int id)
        {
            var consent = await _consentRepository.GetByIdAsync(id);

            if (consent == null)
                return new ApiResponse<ConsentDto>(false, "Consent not found.", null);

            var dto = new ConsentDto
            {
                ConsentId = consent.ConsentId,
                MemberId = consent.MemberId,
                MemberName = consent.Member?.FullName ?? "",
                FlatNumber = consent.Member?.FlatNumber ?? "",
                ConsentStatus = consent.ConsentStatus,
                ConsentDate = consent.ConsentDate,
                Remarks = consent.Remarks,
                DocumentId = consent.DocumentId,
                IsActive = consent.IsActive
            };

            return new ApiResponse<ConsentDto>(true, "Success", dto);
        }

        public async Task<ApiResponse<object>> CreateAsync(CreateConsentDto dto)
        {
            var consent = new Consent
            {
                MemberId = dto.MemberId,
                ConsentStatus = dto.ConsentStatus,
                ConsentDate = dto.ConsentDate,
                Remarks = dto.Remarks,
                DocumentId = dto.DocumentId > 0 ? dto.DocumentId : null,
                IsActive = true,
                CreatedBy = 1,
                CreatedOn = DateTime.UtcNow
            };
            // Check duplicate consent
if (await _consentRepository.ExistsByMemberIdAsync(dto.MemberId))
{
    return new ApiResponse<object>(
        false,
        "Consent already exists for this member.",
        null);
}
            await _consentRepository.AddAsync(consent);
            await _consentRepository.SaveChangesAsync();

            return new ApiResponse<object>(
                true,
                "Consent saved successfully.",
                null);
        }

        public async Task<ApiResponse<object>> UpdateAsync(int id, UpdateConsentDto dto)
        {
            var consent = await _consentRepository.GetByIdAsync(id);

            if (consent == null)
            {
                return new ApiResponse<object>(
                    false,
                    "Consent not found.",
                    null);
            }

            consent.ConsentStatus = dto.ConsentStatus;
            consent.ConsentDate = dto.ConsentDate;
            consent.Remarks = dto.Remarks;
            consent.DocumentId = dto.DocumentId > 0 ? dto.DocumentId : null;
            consent.IsActive = dto.IsActive;

            consent.UpdatedBy = 1;
            consent.UpdatedOn = DateTime.UtcNow;

            await _consentRepository.UpdateAsync(consent);
            await _consentRepository.SaveChangesAsync();

            return new ApiResponse<object>(
                true,
                "Consent updated successfully.",
                null);
        }

        public async Task<ApiResponse<object>> DeleteAsync(int id)
        {
            var consent = await _consentRepository.GetByIdAsync(id);

            if (consent == null)
            {
                return new ApiResponse<object>(
                    false,
                    "Consent not found.",
                    null);
            }

            await _consentRepository.DeleteAsync(consent);
            await _consentRepository.SaveChangesAsync();

            return new ApiResponse<object>(
                true,
                "Consent deleted successfully.",
                null);
        }

    }
}