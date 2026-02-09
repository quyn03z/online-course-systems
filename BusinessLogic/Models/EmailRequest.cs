using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
	public class EmailRequest
	{
		public string Email { get; set; }	
	}

	public class ForgotPassWordModel
	{
		public string Email { get; set; }
		public string Token { get; set; }
	}
}
