using DataAccess.Models.AnswerModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.QuestionModel
{
	public class QuestionResponseModel
	{
		public int QuestionId { get; set; }

		public int? QuizzId { get; set; }

		public string QuestionText { get; set; }
		public int? TypeId { get; set; }
		public List<AnswerResponseModel> Answers { get; set; } = new();


	}



}
