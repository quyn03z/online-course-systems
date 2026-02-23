using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Claims
{
	public interface IClaimService
	{
		int? GetUserId();
		string? GetClaim(string key);
	}
}
