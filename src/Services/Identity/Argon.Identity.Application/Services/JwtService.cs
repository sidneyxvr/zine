using Argon.Identity.Configurations;
using Argon.Identity.Models;
using Argon.Identity.Responses;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Argon.Identity.Services
{
    public class JwtService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public JwtService(
            IOptions<JwtSettings> jwtSettings,
            TokenValidationParameters tokenValidationParameters)
        {
            _jwtSettings = jwtSettings.Value;
            _tokenValidationParameters = tokenValidationParameters;
        }

        public string CodifyToken(ICollection<Claim> claims, Guid userId, string userEmail)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, userEmail));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString(CultureInfo.CurrentCulture)));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(CultureInfo.CurrentCulture), ClaimValueTypes.Integer64));

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _jwtSettings.Emitter,
                Audience = _jwtSettings.ValidOn,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ValidityInHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        public UserLoginResponse GetUserLoginResponse(string encodedToken, string refreshToken, Guid userId, string userEmail, IEnumerable<Claim> claims)
        {
            return new UserLoginResponse
            {
                AccessToken = encodedToken,
                RefreshToken = refreshToken,
                ExpiresIn = TimeSpan.FromHours(_jwtSettings.ValidityInHours).TotalSeconds,
                UserToken = new UserTokenResponse
                {
                    Id = userId,
                    Email = userEmail,
                    Claims = claims.Select(c => new UserClaimResponse { Type = c.Type, Value = c.Value })
                }
            };
        }

        public RefreshToken GenerateRefreshToken(string token)
        {
            var claimsSimplified = GetUserClaimsSimplifiedOrDefault(token);

            if (claimsSimplified is null)
            {
                return null;
            }

            var randomNumber = new byte[32];
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            rngCryptoServiceProvider.GetBytes(randomNumber);

            var refreshToken = Convert.ToBase64String(randomNumber);

            return new RefreshToken
            {
                JwtId = claimsSimplified.Value.Jti,
                CreatedAt = DateTime.UtcNow,
                UserId = claimsSimplified.Value.UserId,
                ValidityInHours = _jwtSettings.RefreshTokenValidityInHours,
                Token = refreshToken
            };
        }

        public (Guid UserId, Guid Jti)? GetUserClaimsSimplifiedOrDefault(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principalClaims = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);

                if (!IsValidJwt(validatedToken))
                {
                    return null;
                }

                var jwtId = principalClaims.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;
                var userId = principalClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                return (new Guid(userId), new Guid(jwtId));
            }
            catch
            {
                return null;
            }
        }

        private static bool IsValidJwt(SecurityToken token)
        {
            return (token is JwtSecurityToken jwtSecurityToken) &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
