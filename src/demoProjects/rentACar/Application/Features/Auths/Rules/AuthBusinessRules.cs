using Application.Features.Auths.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Core.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Rules
{
    public class AuthBusinessRules
    {
        private IUserRepository _userRepository;

        public AuthBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task UserShouldExist(User user)
        {
            if (user is null) throw new BusinessException(Messages.UserShouldExists);
            return Task.CompletedTask;
        }
        public async Task UserPasswordShouldMatch(int id, string password)
        {
            User? user = await _userRepository.GetAsync(i => i.Id == id);
            if (HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new Exception(Messages.PasswordIncorrect);
        }

        public Task OtpAuthenticatorMustExist(OtpAuthenticator? otpAuthenticator)
        {
            if (otpAuthenticator is null) throw new BusinessException(Messages.OtpNotFound);
            return Task.CompletedTask;
        }

        public Task UserShouldNotHaveAuthenticator(User user)
        {
            if (user.AuthenticatorType is not Core.Security.Enums.AuthenticatorType.None) throw new BusinessException("User already have an active otp authenticator.");
            return Task.CompletedTask;
        }

        public Task VerifiedOtpShouldNotExist(OtpAuthenticator? otpAuthenticator)
        {
            if(otpAuthenticator is not null && otpAuthenticator.IsVerified) throw new BusinessException("User already have a verified otp.");
            return Task.CompletedTask;
        }
    }
}
