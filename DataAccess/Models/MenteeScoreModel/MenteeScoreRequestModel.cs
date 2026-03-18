using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.MenteeScoreModel
{
	public class MenteeScoreRequestModel
	{
		public int QuizId { get; set; }
		public double? Score { get; set; }
	}
}
