using Microsoft.AspNetCore.Authorization;

namespace API.Filter
{
	public class PermissionAttribute : AuthorizeAttribute
	{
		public PermissionAttribute(string permission)
		{
			Policy = permission;
		}
	}
}
