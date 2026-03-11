using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using BusinessLogic.Services.Serv;
using DataAccess.Models.DocumentModel;
using DataAccess.Models.LessonModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Teacher
{
	public class ManaDocumentsController : BaseController
	{
		private readonly IDocumentsService _documentsService;

		public ManaDocumentsController(IDocumentsService documentsService)
		{
			_documentsService = documentsService;
		}

		[HttpGet("alls-documents/{lessonId}")]
		public async Task<IActionResult> GetAllsLesson(int lessonId)
		{
			try
			{
				return Ok(ApiResult<IEnumerable<DocumentResponseModel>>.Success(await _documentsService.GetAllsManaDocumentsAsync(lessonId)));
			}
			catch (Exception ex)
			{
				throw new Exception("Có lỗi khi lấy tất cả bài học.");
			}
		}


		[HttpPost("add-document/{lessonId}")]
		public async Task<IActionResult> AddDocumentAsync(DocumentRequestModel documentRequestModel, int lessonId)
		{
			try
			{
				return Ok(ApiResult<DocumentResponseModel>.Success(await _documentsService.AddManaDocumentAsync(documentRequestModel, lessonId)));
			}
			catch (Exception ex)
			{
				throw new Exception("Có lỗi khi thêm bài  pdf mới.");
			}
		}

		[HttpPut("update-document/{documentId}")]
		public async Task<IActionResult> UpdateLessonAsync(int documentId, DocumentRequestModel documentRequestModel)
		{
			try
			{
				return Ok(ApiResult<DocumentResponseModel>.Success(await _documentsService.UpdateManaDocumentAsync(documentId, documentRequestModel)));
			}
			catch (Exception ex)
			{
				throw new Exception("Có lỗi khi cập nhật bài học.");
			}
		}




	}
}
