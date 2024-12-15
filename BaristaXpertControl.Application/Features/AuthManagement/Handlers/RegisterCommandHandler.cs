using BaristaXpertControl.Application.Common.Persistences;
using BaristaXpertControl.Application.Common.Responses;
using BaristaXpertControl.Application.Features.AuthManagement.Commands;
using BaristaXpertControl.Application.Persistences;
using BaristaXpertControl.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BaristaXpertControl.Application.Features.AuthManagement.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public RegisterCommandHandler(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // Kiểm tra thông tin đầu vào
            if (string.IsNullOrWhiteSpace(request.Email))
                throw new ArgumentException("Email is required.");

            if (string.IsNullOrWhiteSpace(request.Username))
                throw new ArgumentException("Username is required.");

            if (string.IsNullOrWhiteSpace(request.Role))
                throw new ArgumentException("Role is required.");

            // Kiểm tra vai trò
            var roleExists = await _unitOfWork.Roles.RoleExistsAsync(request.Role);
            if (!roleExists)
                throw new Exception($"Role '{request.Role}' does not exist.");

            // Tạo mật khẩu tạm thời
            var tempPassword = GenerateTemporaryPassword();

            // Tạo tài khoản người dùng
            var user = new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email
            };

            var createdUser = await _unitOfWork.Users.CreateUserAsync(user, tempPassword);
            if (createdUser == null)
                throw new Exception("Failed to create user.");

            // Gán vai trò
            var roleAssigned = await _unitOfWork.Users.AssignRoleAsync(createdUser, request.Role);
            if (!roleAssigned)
                throw new Exception("Failed to assign role.");

            // Gửi mật khẩu tạm thời đến email
            try
            {
                var subject = "Your Temporary Password";
                var body = $@"
                    <p>Dear {request.Username},</p>
                    <p>Thank you for registering. Your temporary password is:</p>
                    <p><strong>{tempPassword}</strong></p>
                    <p>Please log in and change your password as soon as possible.</p>
                    <p>Best regards,<br/>BaristaXpertControl Team</p>
                ";

                await _emailService.SendEmailAsync(request.Email, subject, body);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
                throw new Exception("Failed to send password email.");
            }

            return new RegisterResponse
            {
                Message = "User registered successfully and temporary password sent."
            };
        }

        private string GenerateTemporaryPassword()
        {
            const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?";
            var random = new Random();
            return new string(Enumerable.Repeat(validChars, 12).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
