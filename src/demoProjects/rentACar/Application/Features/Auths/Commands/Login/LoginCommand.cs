using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.JWT;
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

        public class LoginCommandHandler : IRequestHandler<LoginCommand, LoggedDto>
        {
            private IUserRepository _userRepository;
            private IUserOperationClaimRepository _userOperationClaimRepository;
            private IMapper _mapper;
            private AuthBusinessRules _authBusinessRules;
            private ITokenHelper _tokenHelper;
            public LoginCommandHandler(IUserRepository userRepository, IMapper mapper, AuthBusinessRules authBusinessRules, ITokenHelper tokenHelper, IUserOperationClaimRepository userOperationClaimRepository)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _authBusinessRules = authBusinessRules;
                _tokenHelper = tokenHelper;
                _userOperationClaimRepository = userOperationClaimRepository;
            }

            public async Task<LoggedDto> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                User? user = await _userRepository.GetAsync(i => i.Email == request.Email);
                await _authBusinessRules.UserShouldExist(user);
                await _authBusinessRules.UserPasswordShouldMatch(user.Id, request.Password);

                AccessToken token = await CreateAccessToken(user);

                return new(token);
            }

            private async Task<AccessToken> CreateAccessToken(User user)
            {
                var userOperationClaims = await _userOperationClaimRepository.GetListAsync(u => u.UserId == user.Id, include: u => u.Include(u => u.OperationClaim));
                var claims = userOperationClaims.Items.Select(i=> new OperationClaim() { Id=i.OperationClaim.Id,Name=i.OperationClaim.Name}).ToList();
                var token = _tokenHelper.CreateToken(user,claims);
                return token;
            }
        }
    }
}
