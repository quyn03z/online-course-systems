using DataAccess.Models.MenteeScoreModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Impl
{
	public interface IMenteeScoreService
	{
		Task<MenteeScoreRequestModel> AddMenteeScoreAsync(MenteeScoreRequestModel menteeScoreRequestModel);

	}
}
