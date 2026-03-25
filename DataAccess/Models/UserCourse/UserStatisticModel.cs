using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.UserCourse
{
	public class UserStatisticModel
	{
		public double? AvgScore { get; set; }
		public int TotalAttempts { get; set; }
		public double? MaxScore { get; set; }
		public double? MinScore { get; set; }

		public List<CourseScoreUser> ChartStatistic { get; set; }
	}


	public class CourseScoreUser
	{
		public string QuizzName { get; set; }
		public double? Score { get; set; }
	}
}
