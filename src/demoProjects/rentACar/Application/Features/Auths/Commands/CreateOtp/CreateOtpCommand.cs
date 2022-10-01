using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.Repositories;
using Core.Security.Entities;
using Core.Security.OtpAuthenticator;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Commands.CreateOtp
{
    public class CreateOtpCommand : IRequest<CreatedOtpDto>
    {
        public int UserId { get; set; }

        public class CreateOtpCommandHandler : IRequestHandler<CreateOtpCommand, CreatedOtpDto>
        {
            private IOtpAuthenticatorHelper _otpAuthenticatorHelper;
            private IOtpAuthenticatorRepository _otpAuthenticatorRepository;
            private IUserRepository _userRepository;
            private AuthBusinessRules _authBusinessRules;

            public CreateOtpCommandHandler(IOtpAuthenticatorRepository otpAuthenticatorRepository, AuthBusinessRules authBusinessRules, IUserRepository userRepository, IOtpAuthenticatorHelper otpAuthenticatorHelper)
            {
                _otpAuthenticatorRepository = otpAuthenticatorRepository;
                _authBusinessRules = authBusinessRules;
                _userRepository = userRepository;
                _otpAuthenticatorHelper = otpAuthenticatorHelper;
            }

            public async Task<CreatedOtpDto> Handle(CreateOtpCommand request, CancellationToken cancellationToken)
            {
                User? user = await _userRepository.GetAsync(i => i.Id == request.UserId);
                await _authBusinessRules.UserShouldExist(user);
                await _authBusinessRules.UserShouldNotHaveAuthenticator(user);

                OtpAuthenticator? otpAuthenticator = await _otpAuthenticatorRepository.GetAsync(i => i.UserId == request.UserId);
                await _authBusinessRules.VerifiedOtpShouldNotExist(otpAuthenticator);
                if (otpAuthenticator is not null)
                    await _otpAuthenticatorRepository.DeleteAsync(otpAuthenticator);
                OtpAuthenticator otpAuthenticatorToCreate = await CreateNewOtpAuthenticator(user);
                OtpAuthenticator createdOtpAuth = await _otpAuthenticatorRepository.AddAsync(otpAuthenticatorToCreate);

                CreatedOtpDto createdOtp = new()
                {
                    SecretKey = await _otpAuthenticatorHelper.ConvertSecretKeyToString(createdOtpAuth.SecretKey)
                };
                return createdOtp;
            }

            private async Task<OtpAuthenticator> CreateNewOtpAuthenticator(User user)
            {
                OtpAuthenticator otpAuthenticator = new()
                {
                    UserId = user.Id,
                    IsVerified = false,
                    SecretKey = await _otpAuthenticatorHelper.GenerateSecretKey()
                };
                return otpAuthenticator;
            }
        }
    }
}
