using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.AnswerModel
{
	public class AnswerResponseModel
	{
		public int AnswerId { get; set; }

		public int? QuestionId { get; set; }

		public string AnswerText { get; set; }

		public bool? IsCorrect { get; set; }


	}
}
