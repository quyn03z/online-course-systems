using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.QuizzModel
{
	public class QuizzRequestModel
	{
		[Required(ErrorMessage = "Tên quizz là bắt buộc.")]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Tên quizz phải từ 3 đến 50 ký tự.")]
		public string Title { get; set; }


		[Range(1, int.MaxValue, ErrorMessage = "QuizzTime phải lớn hơn 0.")]
		public int QuizzTime { get; set; }
	}
}
