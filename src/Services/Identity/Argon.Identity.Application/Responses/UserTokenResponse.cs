using System;
using System.Collections.Generic;

namespace Argon.Identity.Responses
{
    public class UserTokenResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaimResponse> Claims { get; set; }
    }
}