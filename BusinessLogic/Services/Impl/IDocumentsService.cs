using DataAccess.Models.DocumentModel;
using DataAccess.Models.LessonModel;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Impl
{
	public interface IDocumentsService
	{

		// lấy hết cả locked
		Task<List<DocumentResponseModel>> GetAllsManaDocumentsAsync(int lessonId);

		// lấy hết không có locked
		Task<List<DocumentResponseModel>> GetAllsDocuments(int lessonId);

		Task<DocumentResponseModel> AddManaDocumentAsync(DocumentRequestModel documentRequestModel, int lessonId);
		Task<DocumentResponseModel> UpdateManaDocumentAsync(int documentId, DocumentRequestModel documentRequestModel);
		Task<bool> RemoveManaDocumentAsync(int documentId);
	}
}
