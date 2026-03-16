using DataAccess.Models.AnswerModel;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.QuestionModel
{


	public class QuizzQuestionsRequestModel
	{
		public int? QuizzId { get; set; }

		public string QuestionText { get; set; }

		public int? TypeId { get; set; }
		public virtual ICollection<AnswerRequestModel> Answers { get; set; }

	}
	public class QuestionRequestModel
	{
		public string QuestionText { get; set; }

		public int? TypeId { get; set; }

		public virtual ICollection<AnswerRequestModel> Answers { get; set; }

	}

	public class AnswerRequestModel
	{
		public int? QuestionId { get; set; }

		public string AnswerText { get; set; }

		public bool? IsCorrect { get; set; }
	}



}
