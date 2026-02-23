using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Claims
{
	public class ClaimService : IClaimService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public ClaimService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public int? GetUserId()
		{
			var idString = GetClaim(ClaimTypes.NameIdentifier);
			if (int.TryParse(idString, out var userId))
			{
				return userId;
			}
			return null;
		}

		public string? GetClaim(string key)
		{
			return _httpContextAccessor.HttpContext?.User?.FindFirst(key)?.Value;
		}
	}
}
