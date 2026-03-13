using BusinessLogic.Exceptions;
using BusinessLogic.Services.Impl;
using DataAccess.Models.DocumentModel;
using DataAccess.Models.LessonModel;
using DataAccess.Repositories.Impl;
using DataAccess.Repositories.Repo;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Serv
{
	public class DocumentsService : IDocumentsService
	{
		private readonly IDocumentsRepository _documentsRepository;
		private readonly ILessonService _lessonService;

		public DocumentsService(IDocumentsRepository documentsRepository, ILessonService lessonService)
		{
			_documentsRepository = documentsRepository;
			_lessonService = lessonService;
		}

		public async Task<DocumentResponseModel> AddManaDocumentAsync(DocumentRequestModel documentRequestModel, int lessonId)
		{
			try
			{
				var document = new Documents
				{
					LessonId = lessonId,
					Title = documentRequestModel.Title,
					Description = documentRequestModel.Description,
					FileUrl = documentRequestModel.FileUrl,
					IsLocked = documentRequestModel.IsLocked,
					CreateDate = DateTime.UtcNow,
					IsDelete = false
				};
				await _documentsRepository.AddAsync(document);

				return new DocumentResponseModel
				{
					DocumentId = document.DocumentId,
					LessonId = lessonId,
					Title = document.Title,
					Description = document.Description,
					FileUrl = document.FileUrl,
					IsLocked = document.IsLocked,
					IsDelete = document.IsDelete
				};
			}
			catch (Exception ex)
			{
				throw new Exception("Có lỗi khi thêm bài  pdf mới.", ex);
			}
		}

		public async Task<List<DocumentResponseModel>> GetAllsDocuments(int lessonId)
		{
			if (!await _lessonService.GetLessonsById(lessonId))
			{
				throw new Exception("Lesson Id không tồn tại.");
			}
			var documents = await _documentsRepository.GetAllsDocuments(lessonId);

			return documents.Select(x => new DocumentResponseModel
			{
				DocumentId = x.DocumentId,
				LessonId = x.LessonId,
				Title = x.Title,
				Description = x.Description,
				IsLocked = x.IsLocked,
				FileUrl = x.FileUrl
			}).ToList();
		}

		public async Task<List<DocumentResponseModel>> GetAllsManaDocumentsAsync(int lessonId)
		{
			try
			{
				var allsDocu = await _documentsRepository.GetAllsManaDocumentsAsync(lessonId);
				return allsDocu.Select(x => new DocumentResponseModel
				{
					DocumentId = x.DocumentId,
					LessonId = x.LessonId,
					Title = x.Title,
					Description = x.Description,
					IsLocked = x.IsLocked,
					FileUrl = x.FileUrl,
				}).ToList();
			}
			catch (Exception ex)
			{
				throw new Exception("Có lỗi khi lấy tất cả bài học.", ex);
			}
		}

		public async Task<DocumentResponseModel> UpdateManaDocumentAsync(int documentId, DocumentRequestModel documentRequestModel)
		{
			try
			{
				var document = await _documentsRepository.GetByIdAsync(documentId);
				if (document == null) throw new BadRequestException("Documment không tồn tại trong hệ thống!");

				document.Title = documentRequestModel.Title;
				document.Description = documentRequestModel.Description;
				document.FileUrl = documentRequestModel.FileUrl;
				document.IsLocked = documentRequestModel.IsLocked;

				await _documentsRepository.UpdateAsync(document);

				return new DocumentResponseModel
				{
					DocumentId = document.DocumentId,
					LessonId = document.LessonId,
					Title = document.Title,
					Description = document.Description,
					FileUrl = document.FileUrl,
					IsLocked = document.IsLocked,
					IsDelete = document.IsDelete,
				};
			}
			catch (Exception ex)
			{
				throw new Exception("Có lỗi khi cập nhật bài học pdf.", ex);
			}
		}

		public async Task<bool> RemoveManaDocumentAsync(int documentId)
		{
			try
			{
				var document = await _documentsRepository.GetByIdAsync(documentId);
				if (document == null) throw new BadRequestException("Documment không tồn tại trong hệ thống!");

				await _documentsRepository.DeleteAsync(document);
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception("Có lỗi khi xóa bài học pdf.", ex);
			}
		}
	}
}
