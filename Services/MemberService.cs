using BCrypt.Net;
using GACHSLApi.Common;
using GACHSLApi.DTOs.Member;
using GACHSLApi.Entities;
using GACHSLApi.Interfaces;

namespace GACHSLApi.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IUserRepository _userRepository;

        public MemberService(
            IMemberRepository memberRepository,
            IUserRepository userRepository)
        {
            _memberRepository = memberRepository;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<List<MemberDto>>> GetAllAsync()
        {
            var members = await _memberRepository.GetAllAsync();

            var result = members.Select(m => new MemberDto
            {
                MemberId = m.MemberId,
                FlatNumber = m.FlatNumber,
                FullName = m.FullName,
                Phone = m.Phone,
                Email = m.Email,
                IsOwner = m.IsOwner,
                IsActive = m.IsActive
            }).ToList();

            return new ApiResponse<List<MemberDto>>(true, "Success", result);
        }

        public async Task<ApiResponse<MemberDto>> GetByIdAsync(int id)
        {
            var m = await _memberRepository.GetByIdAsync(id);

            if (m == null)
                return new ApiResponse<MemberDto>(false, "Member not found", null);

            var dto = new MemberDto
            {
                MemberId = m.MemberId,
                FlatNumber = m.FlatNumber,
                FullName = m.FullName,
                Phone = m.Phone,
                Email = m.Email,
                IsOwner = m.IsOwner,
                IsActive = m.IsActive
            };

            return new ApiResponse<MemberDto>(true, "Success", dto);
        }

        // ⭐ AUTO LOGIN CREATION LOGIC
        public async Task<ApiResponse<object>> CreateAsync(CreateMemberDto dto)
        {
            // 1. Create Login User
            var user = new User
            {
                Username = dto.FlatNumber,   // login = flat number
                Email = dto.Email ?? $"{dto.FlatNumber}@gokulaccord.local",
                FullName = dto.FullName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Welcome@123"),
                Role = "User",
                IsActive = true,
                CreatedOn = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            // 2. Create Member linked to User
            var member = new Member
            {
                FlatNumber = dto.FlatNumber,
                FullName = dto.FullName,
                Phone = dto.Phone,
                Email = dto.Email,
                IsOwner = dto.IsOwner,
                IsActive = dto.IsActive,
                UserId = user.UserId,
                CreatedOn = DateTime.UtcNow
            };

            await _memberRepository.AddAsync(member);
            await _memberRepository.SaveChangesAsync();

            return new ApiResponse<object>(true, "Member + Login created successfully", null);
        }

        public async Task<ApiResponse<object>> UpdateAsync(int id, UpdateMemberDto dto)
        {
            var member = await _memberRepository.GetByIdAsync(id);

            if (member == null)
                return new ApiResponse<object>(false, "Member not found", null);

            member.FullName = dto.FullName;
            member.Phone = dto.Phone;
            member.Email = dto.Email;
            member.IsOwner = dto.IsOwner;
            member.IsActive = dto.IsActive;

            await _memberRepository.UpdateAsync(member);
            await _memberRepository.SaveChangesAsync();

            return new ApiResponse<object>(true, "Updated successfully", null);
        }

        public async Task<ApiResponse<object>> DeleteAsync(int id)
        {
            var member = await _memberRepository.GetByIdAsync(id);

            if (member == null)
                return new ApiResponse<object>(false, "Member not found", null);

            await _memberRepository.DeleteAsync(member);
            await _memberRepository.SaveChangesAsync();

            return new ApiResponse<object>(true, "Deleted successfully", null);
        }
    }
}