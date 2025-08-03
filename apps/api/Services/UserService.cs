using Shared.Domain.User.Models;
using Shared.Domain.User.Dtos;
using user.Repositories;
using System;
using Api.Commons;

namespace user.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepo;

        public UserService(UserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<ApiResponse<List<User>>> GetAllUsersAsync()
        {
            var users = await _userRepo.GetAllAsync();
            return ApiResponse<List<User>>.Ok(users, "查詢成功");
        }

        public async Task<ApiResponse<User>> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null)
                return ApiResponse<User>.Fail("找不到使用者", 404);

            return ApiResponse<User>.Ok(user);
        }

        public async Task<ApiResponse<User>> CreateUserAsync(CreateUserDto dto)
        {

            var exists = await _userRepo.FindByEmailAsync(dto.Email);
            if (exists != null)
            {
                return ApiResponse<User>.Fail("Email已被使用", ErrorCodes.ValidationFailed);
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                Role = dto.Role,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            await _userRepo.AddAsync(user);
            await _userRepo.SaveChangesAsync();

            return ApiResponse<User>.Ok(user, "Create User Success");
        }

        public async Task<ApiResponse<User>> UpdateUserAsync(Guid id, UpdateUserDto dto)
        {
            var existingUser = await _userRepo.GetByIdAsync(id);
            if (existingUser == null)
                return ApiResponse<User>.Fail("找不到使用者", 404);

            if (!string.IsNullOrEmpty(dto.Email))
            {
                var userWithEmail = await _userRepo.FindByEmailAsync(dto.Email);
                if (userWithEmail != null && userWithEmail.Id != id)
                {
                    return ApiResponse<User>.Fail("Email已被使用", ErrorCodes.ValidationFailed);
                }
            }

            if (dto.Name != null) existingUser.Name = dto.Name;
            if (dto.Email != null) existingUser.Email = dto.Email;
            if (dto.PhoneNumber != null) existingUser.PhoneNumber = dto.PhoneNumber;
            if (dto.Address != null) existingUser.Address = dto.Address;
            if (dto.Role.HasValue) existingUser.Role = dto.Role.Value;

            existingUser.UpdatedAt = DateTime.UtcNow;

            _userRepo.Update(existingUser);
            await _userRepo.SaveChangesAsync();

            return ApiResponse<User>.Ok(existingUser, "更新成功");
        }


        public async Task<ApiResponse<bool>> DeleteUserAsync(Guid id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null)
                return ApiResponse<bool>.Fail("找不到使用者", 404);

            _userRepo.Remove(user);
            await _userRepo.SaveChangesAsync();

            return ApiResponse<bool>.Ok(true);
        }

        public async Task<ApiResponse<string>> UpdatePasswordAsync(Guid userId, UpdatePasswordDto dto)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null) return ApiResponse<string>.Fail("用戶不存在");

            bool isCorrentPassword = BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.Password);
            if (!isCorrentPassword) return ApiResponse<string>.Fail("舊密碼錯誤");

            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepo.SaveChangesAsync();

            return ApiResponse<string>.Ok("密碼已更新");
        }
    }
}
