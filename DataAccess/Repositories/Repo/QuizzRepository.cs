using DataAccess.Models.QuizzModel;
using DataAccess.Repositories.Impl;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Repo
{
	public class QuizzRepository : BaseRepository<Quizz>, IQuizzRepository
	{
		public QuizzRepository(OCMSMSFContext context) : base(context)
		{
		}

		
	}
}
