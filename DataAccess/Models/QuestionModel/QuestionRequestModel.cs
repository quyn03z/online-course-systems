using DataAccess.Models.AnswerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.QuestionModel
{
	public class QuestionRequestModel
	{

		public int? QuizzId { get; set; }

		public string QuestionText { get; set; }

		public int? TypeId { get; set; }
	}
}
