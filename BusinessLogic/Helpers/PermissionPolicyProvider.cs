using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
	public class PermissionPolicyProvider : DefaultAuthorizationPolicyProvider
	{
		public PermissionPolicyProvider(IOptions<AuthorizationOptions> options) : base(options) { }

		public override Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
		{
			var policy = new AuthorizationPolicyBuilder()
				.AddRequirements(new PermissionRequirement(policyName))
				.Build();

			return Task.FromResult(policy);
		}
	}
}
