using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using DataAccess.Models.DocumentModel;
using DataAccess.Models.LessonModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class DocumentsController : BaseController
	{
		private readonly IDocumentsService _documentsService;

		public DocumentsController(IDocumentsService documentsService)
		{
			_documentsService = documentsService;
		}

		[HttpGet("alls-documents/{lessonId}")]
		public async Task<IActionResult> GetAllLessonAsync(int lessonId)
		{
			try
			{
				return Ok(ApiResult<IEnumerable<DocumentResponseModel>>.Success(await _documentsService.GetAllsDocuments(lessonId)));
			}
			catch (Exception ex)
			{
				throw new Exception("Có lỗi khi lấy tất cả bài học.");
			}
		}
	}
}
