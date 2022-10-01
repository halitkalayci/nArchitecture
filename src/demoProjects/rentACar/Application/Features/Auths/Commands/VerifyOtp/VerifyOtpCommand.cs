using Application.Features.Auths.Constants;
using Application.Features.Auths.Rules;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Core.Security.OtpAuthenticator;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Commands.VerifyOtp
{
    public class VerifyOtpCommand : IRequest
    {
        public int UserId { get; set; }
        public string ActivationCode { get; set; }

        public class VerifyOtpCommandHandler : IRequestHandler<VerifyOtpCommand, Unit>
        {
            private IUserRepository _userRepository;
            private IOtpAuthenticatorRepository _otpAuthenticatorRepository;
            private IOtpAuthenticatorHelper _otpAuthenticationHelper;
            private AuthBusinessRules _authBusinessRules;

            public VerifyOtpCommandHandler(IOtpAuthenticatorRepository otpAuthenticatorRepository, IOtpAuthenticatorHelper otpAuthenticationHelper, IUserRepository userRepository, AuthBusinessRules authBusinessRules)
            {
                _otpAuthenticatorRepository = otpAuthenticatorRepository;
                _otpAuthenticationHelper = otpAuthenticationHelper;
                _userRepository = userRepository;
                _authBusinessRules = authBusinessRules;
            }

            public async Task<Unit> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
            {
                OtpAuthenticator otpAuthenticator = await _otpAuthenticatorRepository.GetAsync(i=>i.UserId == request.UserId);
                await _authBusinessRules.OtpAuthenticatorMustExist(otpAuthenticator);

                User user = await _userRepository.GetAsync(i => i.Id == request.UserId);
                await VerifyCode(otpAuthenticator,user, request.ActivationCode);

                otpAuthenticator.IsVerified = true;
                user.AuthenticatorType = Core.Security.Enums.AuthenticatorType.Otp;

                await _userRepository.UpdateAsync(user);
                await _otpAuthenticatorRepository.UpdateAsync(otpAuthenticator);

                return Unit.Value;
            }

            private async Task VerifyCode(OtpAuthenticator authenticator, User user, string authenticatorCode)
            {
                if (user.AuthenticatorType == Core.Security.Enums.AuthenticatorType.Email)
                    await VerifyEmailCode(authenticator,user, authenticatorCode);
                else if (user.AuthenticatorType == Core.Security.Enums.AuthenticatorType.Otp)
                    await VerifyOtpCode(authenticator,user, authenticatorCode);
            }

            private async Task VerifyOtpCode(OtpAuthenticator authenticator,User user, string authenticatorCode)
            {
                bool result = await _otpAuthenticationHelper.VerifyCode(authenticator.SecretKey, authenticatorCode);
                if (!result)
                    throw new BusinessException(Messages.WrongOtpCode);
            }

            private Task VerifyEmailCode(OtpAuthenticator authenticator, User user, string authenticatorCode)
            {
                throw new NotImplementedException();
            }
        }
    }
}
