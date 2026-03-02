using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
	public class ForgotPasswordModel
	{
		public string Email { get; set; }
		public string ResetLink { get; set; }
	}
	public class ResetPasswordModel
	{

		[Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
		[MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$", ErrorMessage = "Mật khẩu phải có chữ hoa, chữ thường và số.")]
		public string Password { get; set; }

		[Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
		public string ConfirmPassword { get; set; } = string.Empty;
	}
}
