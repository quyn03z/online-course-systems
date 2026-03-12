using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.RoleModel
{

	public class RoleRequestModel
	{
		[Required(ErrorMessage = "Tên Role là bắt buộc.")]
		[StringLength(100, MinimumLength = 3, ErrorMessage = "Tên Role phải từ 3 đến 100 ký tự.")]
		public string RoleName { get; set; }
	}

	public class RoleResponseModel
	{
		public int Id { get; set; }
		public string RoleName { get; set; }
	}
}
