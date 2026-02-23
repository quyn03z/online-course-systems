using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
	public class User
	{
		public class LoginUserModel
		{
			[Required(ErrorMessage = "Tên đăng nhập là bắt buộc.")]
			public string Username { get; set; }
			[Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
			public string Password { get; set; }
		}


		public class LoginResponseModel
		{
			public string Username { get; set; }

			public string Email { get; set; }

			public string Token { get; set; }
			public string RefreshToken { get; set; }
			public string Role { get; set; }
		}

		public class CreateUserResponseModel 
		{
			public int Id { get; set; }
		}

		public class CreateUserModel
		{
			[Required(ErrorMessage = "Tên đăng nhập là bắt buộc.")]
			[StringLength(50, MinimumLength = 3, ErrorMessage = "Tên đăng nhập phải từ 3 đến 50 ký tự.")]
			public string UserName { get; set; }

			[Required(ErrorMessage = "Email là bắt buộc.")]
			[EmailAddress(ErrorMessage = "Định dạng email không hợp lệ.")]
			[StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự.")]
			public string Email { get; set; }

			[Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
			[MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
			[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$", ErrorMessage = "Mật khẩu phải có chữ hoa, chữ thường và số.")]
			public string Password { get; set; }


			[Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp.")] 
			public string ConfirmPassword { get; set; } = string.Empty;


			public string? FirstName { get; set; }

			public string? LastName { get; set; }

		}

		public class UserResponseModel
		{
			public int Id { get; set; }
			public string Username { get; set; }
			public string Lastname { get; set; }
			public string Firstname { get; set; }
			public string Email { get; set; }
			public bool IsLocked { get; set; }
			public string RoleName { get; set; }
		}


		public class AddUserAdminModel
		{
			[Required(ErrorMessage = "Tên đăng nhập là bắt buộc.")]
			[StringLength(50, MinimumLength = 3, ErrorMessage = "Tên đăng nhập phải từ 3 đến 50 ký tự.")]
			public string UserName { get; set; }

			[Required(ErrorMessage = "Email là bắt buộc.")]
			[EmailAddress(ErrorMessage = "Định dạng email không hợp lệ.")]
			[StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự.")]
			public string Email { get; set; }
			public string? FirstName { get; set; }
			public string? LastName { get; set; }
			[Range(1, 3, ErrorMessage = "RoleId chỉ được phép là 1, 2 hoặc 3.")]
			public int RoleId { get; set; }

		}

		public class UserRequest
		{
			public int userId;
			[Required(ErrorMessage = "Tên đăng nhập là bắt buộc.")]
			[StringLength(50, MinimumLength = 3, ErrorMessage = "Tên đăng nhập phải từ 3 đến 50 ký tự.")]
			public string UserName { get; set; }

			[Required(ErrorMessage = "Email là bắt buộc.")]
			[EmailAddress(ErrorMessage = "Định dạng email không hợp lệ.")]
			[StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự.")]
			public string Email { get; set; }
			public string? FirstName { get; set; }
			public string? LastName { get; set; }
			[Range(1, 3, ErrorMessage = "RoleId chỉ được phép là 1, 2 hoặc 3.")]
			public int RoleId { get; set; }
		}


	}
}
