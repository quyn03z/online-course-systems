using BusinessLogic.Helpers;
using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using BusinessLogic.Services.Serv;
using DataAccess.Models.PageResultModel;
using DataAccess.Models.RoleModel;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BusinessLogic.Models.User;

namespace API.Controllers
{
    [Authorize(Roles = AppConstants.Roles.Admin)]
    public class RoleController : BaseController
    {

        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }


        [HttpGet("get-alls-role")]
        public async Task<IActionResult> GetAllRole()
        {
            return Ok(ApiResult<IEnumerable<RoleResponseModel>>.Success(await _roleService.GetAllsRoleAsync()));
        }


		[HttpPost("create-role")]
		public async Task<IActionResult> CreateRoleAsync(RoleRequestModel roleRequestModel)
		{
			if (!ModelState.IsValid)
				return ValidationError();
			return Ok(ApiResult<string>.Success(await _roleService.CreateRoleAsync(roleRequestModel)));
		}

		[HttpPut("update-role/{roleId}")]
		public async Task<IActionResult> UpdateRoleAsync(RoleRequestModel roleRequestModel, int roleId)
		{
			if (!ModelState.IsValid)
				return ValidationError();
			return Ok(ApiResult<string>.Success(await _roleService.UpdateRoleAsync(roleRequestModel,roleId)));
		}

		[HttpDelete("delete-role/{roleId}")]
		public async Task<IActionResult> DeleteRoleAsync(int roleId)
		{
			return Ok(ApiResult<string>.Success(await _roleService.DeleteRoleAsync(roleId)));
		}


	}
}
