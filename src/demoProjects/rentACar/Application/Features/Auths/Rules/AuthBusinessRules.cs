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
    }
}
