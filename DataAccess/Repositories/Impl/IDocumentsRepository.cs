using DataAccess.Models.LessonModel;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Impl
{
	public interface IDocumentsRepository : IBaseRepository<Documents>
	{
		// lấy hết cả locked
		Task<List<Documents>> GetAllsManaDocumentsAsync(int lessonId);

		// lấy hết không có locked

		Task<List<Documents>> GetAllsDocuments(int lessonId);



	}
}
