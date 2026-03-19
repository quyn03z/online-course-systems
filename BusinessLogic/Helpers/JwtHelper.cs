using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
	public static class JwtHelper
	{
		public static string GenerateToken(User user, IConfiguration configuration, IEnumerable<string> permissions)
		{
			var secretKey = configuration.GetValue<string>("JwtConfiguration:SecretKey");
			var issuer = configuration.GetValue<string>("JwtConfiguration:ValidIssuer");
			var audience = configuration.GetValue<string>("JwtConfiguration:ValidAudience");

			var key = Encoding.ASCII.GetBytes(secretKey ?? string.Empty);

			var tokenHandler = new JwtSecurityTokenHandler();

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
				new Claim(ClaimTypes.Name, user.Username),
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.Role, user.Role.RoleName),
				new Claim("permissions", string.Join(",", permissions)),
				new Claim("Avatar", user.Avatar ?? "https://static.vecteezy.com/system/resources/thumbnails/024/983/914/small/simple-user-default-icon-free-png.png"),
			};

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddMinutes(30),
				Issuer = issuer,
				Audience = audience,
				SigningCredentials =
					new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}

		public static string GenerateRefreshToken()
		{
			var randomNumber = new byte[64];
			using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
			rng.GetBytes(randomNumber);
			return Convert.ToBase64String(randomNumber);
		}


		public static ClaimsPrincipal ValidateToken(string token, IConfiguration configuration)
		{
			var secretKey = configuration.GetValue<string>("JwtConfiguration:SecretKey");
			var key = Encoding.ASCII.GetBytes(secretKey ?? string.Empty);

			var tokenHandler = new JwtSecurityTokenHandler();
			var validationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuer = false,
				ValidateAudience = false,
				ValidateLifetime = true,
				ClockSkew = TimeSpan.Zero
			};

			try
			{
				var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
				return principal;
			}
			catch
			{
				return null;
			}
		}

	}	
}

