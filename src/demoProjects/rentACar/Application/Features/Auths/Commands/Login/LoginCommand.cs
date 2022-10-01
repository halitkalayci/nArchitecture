using Application.Features.Auths.Constants;
using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.JWT;
using Core.Security.OtpAuthenticator;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Commands.Login
{
    public class LoginCommand : IRequest<LoggedDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? AuthenticatorCode { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, LoggedDto>
        {
            private IUserRepository _userRepository;
            private IUserOperationClaimRepository _userOperationClaimRepository;
            private IMapper _mapper;
            private AuthBusinessRules _authBusinessRules;
            private ITokenHelper _tokenHelper;
            private IOtpAuthenticatorHelper _otpAuthenticatorHelper;
            private IOtpAuthenticatorRepository _otpAuthenticatorRepository;
            public LoginCommandHandler(IUserRepository userRepository, IMapper mapper, AuthBusinessRules authBusinessRules, ITokenHelper tokenHelper, IUserOperationClaimRepository userOperationClaimRepository, IOtpAuthenticatorHelper otpAuthenticatorHelper, IOtpAuthenticatorRepository otpAuthenticatorRepository)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _authBusinessRules = authBusinessRules;
                _tokenHelper = tokenHelper;
                _userOperationClaimRepository = userOperationClaimRepository;
                _otpAuthenticatorHelper = otpAuthenticatorHelper;
                _otpAuthenticatorRepository = otpAuthenticatorRepository;
            }

            public async Task<LoggedDto> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                User? user = await _userRepository.GetAsync(e=>e.Email == request.Email);
                await _authBusinessRules.UserShouldExist(user);
                await _authBusinessRules.UserPasswordShouldMatch(user.Id, request.Password);

                LoggedDto loggedDto = new();
                if(user.AuthenticatorType is not AuthenticatorType.None)
                {
                    loggedDto.RequiredAuthenticatorType = user.AuthenticatorType;
                    if (string.IsNullOrEmpty(request.AuthenticatorCode))
                    {
                        //TODO : Send code
                        return loggedDto;
                    }
                    await VerifyOtp(user, request.AuthenticatorCode);
                }
                AccessToken createdAccessToken = await CreateAccessToken(user);


                loggedDto.AccessToken = createdAccessToken;
                //loggedDto.RefreshToken = addedRefreshToken;
                return loggedDto;
            }

            private async Task<AccessToken> CreateAccessToken(User user)
            {
                IPaginate<UserOperationClaim> userOperationClaims =
                    await _userOperationClaimRepository.GetListAsync(u => u.UserId == user.Id,
                                                                     include: u =>
                                                                         u.Include(u => u.OperationClaim)
                    );
                IList<OperationClaim> operationClaims =
                    userOperationClaims.Items.Select(u => new OperationClaim
                    { Id = u.OperationClaim.Id, Name = u.OperationClaim.Name }).ToList();

                AccessToken accessToken = _tokenHelper.CreateToken(user, operationClaims);
                return accessToken;
            }

            private async Task VerifyOtp(User user, string authCode)
            {
                if (user.AuthenticatorType == AuthenticatorType.Email)
                    await VerifyEmailTypeOtp(user, authCode);
                else if (user.AuthenticatorType == AuthenticatorType.Otp)
                    await VerifyCodeOtp(user, authCode);
            }

            private async Task VerifyEmailTypeOtp(User user, string authCode)
            {
                throw new NotImplementedException();
            }
            private async Task VerifyCodeOtp(User user, string authCode)
            {
                OtpAuthenticator authenticator = await _otpAuthenticatorRepository.GetAsync(i => i.UserId == user.Id);
                await _authBusinessRules.OtpAuthenticatorMustExist(authenticator);

                bool result = await _otpAuthenticatorHelper.VerifyCode(authenticator.SecretKey, authCode);

                if (result is false)
                    throw new BusinessException(Messages.WrongOtpCode);
            }
        }
    }
}
