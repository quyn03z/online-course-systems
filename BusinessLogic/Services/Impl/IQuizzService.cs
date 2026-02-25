using DataAccess.Models.QuizzModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Impl
{
	public interface IQuizzService
	{
		Task<IEnumerable<QuizzResponseModel>> GetAllQuizzAsync();


	}
}
