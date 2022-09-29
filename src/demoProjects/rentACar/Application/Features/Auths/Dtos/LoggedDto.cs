using Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Dtos
{
    public class LoggedDto
    {
        public AccessToken AccessToken { get; set; }

        public LoggedDto()
        {

        }

        public LoggedDto(AccessToken token)
        {
            AccessToken = token;
        }

    }
}
