using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
	public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
		{
			var permissions = context.User.Claims
				.Where(x => x.Type == "permission")
				.Select(x => x.Value);

			if (permissions.Contains(requirement.Permission))
			{
				context.Succeed(requirement);
			}
				
			return Task.CompletedTask;
		}
	}
}
