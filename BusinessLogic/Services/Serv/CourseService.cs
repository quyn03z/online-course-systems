using DataAccess.Models.CourseModel;
using BusinessLogic.Services.Impl;
using DataAccess.Repositories.Impl;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Claims;
using BusinessLogic.Exceptions;

namespace BusinessLogic.Services.Serv
{
	public class CourseService : ICourseService
	{
		private readonly ICourseRepository _courseRepository;
		private readonly IClaimService _claimService;
		private readonly IMenteeScoresRepository _menteeScoresRepository;
		private readonly IUserRepository _userRepository;

		public CourseService(ICourseRepository courseRepository, IClaimService claimService, IMenteeScoresRepository menteeScoresRepository, IUserRepository userRepository)
		{
			_courseRepository = courseRepository;
			_claimService = claimService;
			_menteeScoresRepository = menteeScoresRepository;
			_userRepository = userRepository;
		}

		public async Task<CourseResponseModel> AddCourseAsync(CourseRequestModel courseRequestModel)
		{
			var userId = _claimService.GetUserId();
			return await _courseRepository.AddCourseAsync(courseRequestModel,userId.Value);
		}

		public async Task<string> DownloadCertificateAsync(int courseId)
		{
			var userId = _claimService.GetUserId();
			if (userId == null) throw new UnauthorizedException("Người dùng chưa xác thực.");

			var course = await _courseRepository.GetCourseDetailsById(courseId);
			if (course == null) throw new NotFoundException("Khóa học không tồn tại.");

			var progress = await _menteeScoresRepository.CheckProgressAsync(courseId, userId.Value);
			if (!progress.IsCompleted)
			{
				throw new BadRequestException("Bạn chưa hoàn thành khóa học để nhận chứng chỉ.");
			}

			var user = await _userRepository.GetUserByIdAsync(userId.Value);
			var completionDate = DateTime.Now.ToString("dd/MM/yyyy");

			// Một template HTML siêu cao cấp cho chứng chỉ - Đã fix lỗi layout
			string htmlTemplate = $@"
            <!DOCTYPE html>
            <html lang='vi'>
            <head>
                <meta charset='UTF-8'>
                <style>
                    @import url('https://fonts.googleapis.com/css2?family=Libre+Baskerville:ital,wght@0,400;0,700;1,400&family=Montserrat:wght@400;700&display=swap');
                    
                    body {{
                        margin: 0;
                        padding: 0;
                        background-color: #fff;
                        display: flex;
                        justify-content: center;
                        align-items: center;
                        width: 800px;
                        height: 600px;
                    }}
                    .certificate-container {{
                        width: 800px;
                        height: 600px;
                        padding: 0;
                        background: #fff;
                        position: relative;
                        box-sizing: border-box;
                        overflow: hidden;
                    }}
                    .outer-border {{
                        border: 15px solid #1a2a6c;
                        height: 100%;
                        width: 100%;
                        padding: 10px;
                        box-sizing: border-box;
                    }}
                    .inner-border {{
                        border: 3px solid #b38728;
                        height: 100%;
                        width: 100%;
                        padding: 30px;
                        box-sizing: border-box;
                        background: #fff;
                        display: flex;
                        flex-direction: column;
                        align-items: center;
                        position: relative;
                    }}
                    .corner {{
                        position: absolute;
                        width: 60px;
                        height: 60px;
                        border: 4px solid #b38728;
                    }}
                    .top-left {{ top: 5px; left: 5px; border-right: none; border-bottom: none; }}
                    .top-right {{ top: 5px; right: 5px; border-left: none; border-bottom: none; }}
                    .bottom-left {{ bottom: 5px; left: 5px; border-right: none; border-top: none; }}
                    .bottom-right {{ bottom: 5px; right: 5px; border-left: none; border-top: none; }}

                    .header-section {{
                        margin-top: 20px;
                        margin-bottom: 20px;
                    }}
                    .main-title {{
                        font-family: 'Libre Baskerville', serif;
                        font-size: 45px;
                        font-weight: 700;
                        color: #1a2a6c;
                        margin: 0;
                        text-transform: uppercase;
                        letter-spacing: 5px;
                    }}
                    .sub-title {{
                        font-family: 'Montserrat', sans-serif;
                        font-size: 16px;
                        color: #b38728;
                        letter-spacing: 6px;
                        margin-top: 5px;
                        font-weight: bold;
                    }}
                    .recipient-label {{
                        font-family: 'Libre Baskerville', serif;
                        font-style: italic;
                        font-size: 18px;
                        color: #555;
                        margin-top: 30px;
                    }}
                    .recipient-name {{
                        font-family: 'Libre Baskerville', serif;
                        font-size: 38px;
                        font-weight: 700;
                        color: #1a2a6c;
                        margin: 15px 0;
                        padding-bottom: 5px;
                        border-bottom: 2px solid #b38728;
                        width: 80%;
                    }}
                    .completion-text {{
                        font-family: 'Montserrat', sans-serif;
                        font-size: 14px;
                        color: #555;
                        width: 75%;
                        line-height: 1.5;
                        margin-top: 10px;
                    }}
                    .course-name {{
                        font-family: 'Montserrat', sans-serif;
                        font-size: 22px;
                        font-weight: bold;
                        color: #1a2a6c;
                        margin: 15px 0;
                    }}
                    .footer-section {{
                        width: 100%;
                        display: flex;
                        justify-content: space-between;
                        align-items: flex-end;
                        margin-top: auto;
                        padding-bottom: 20px;
                    }}
                    .footer-item {{
                        text-align: center;
                        width: 200px;
                    }}
                    .signature-line {{
                        border-top: 1px solid #333;
                        margin-top: 10px;
                        padding-top: 5px;
                        font-family: 'Montserrat', sans-serif;
                        font-size: 12px;
                        font-weight: bold;
                    }}
                    .signature-font {{
                        font-family: 'Libre Baskerville', serif;
                        font-style: italic;
                        font-size: 20px;
                        color: #1a2a6c;
                    }}
                    .seal-container {{
                        position: absolute;
                        bottom: 80px;
                        right: 40px;
                    }}
                    .official-seal {{
                        width: 100px;
                        height: 100px;
                        background: radial-gradient(circle, #f9f295, #e0aa3e, #d59e36);
                        border-radius: 50%;
                        border: 2px double #fff;
                        display: flex;
                        flex-direction: column;
                        justify-content: center;
                        align-items: center;
                        box-shadow: 0 5px 10px rgba(0,0,0,0.2);
                        transform: rotate(-10deg);
                    }}
                    .seal-text {{
                        font-family: 'Montserrat', sans-serif;
                        font-size: 9px;
                        font-weight: bold;
                        color: #1a2a6c;
                        text-align: center;
                    }}
                </style>
            </head>
            <body>
                <div class='certificate-container'>
                    <div class='outer-border'>
                        <div class='inner-border'>
                            <div class='corner top-left'></div>
                            <div class='corner top-right'></div>
                            <div class='corner bottom-left'></div>
                            <div class='corner bottom-right'></div>

                            <div class='header-section'>
                                <h1 class='main-title'>CERTIFICATE</h1>
                                <div class='sub-title'>OF COMPLETION</div>
                            </div>

                            <div class='recipient-label'>This is to certify that</div>
                            <div class='recipient-name'>{user.Firstname} {user.Lastname}</div>

                            <div class='completion-text'>
                                has successfully completed all requirements and assessments for the online course
                            </div>
                            <div class='course-name'>""{course.CourseName}""</div>

                            <div class='seal-container'>
                                <div class='official-seal'>
                                    <div class='seal-text'>OFFICIAL<br>GRADUATE<br>MSF SYSTEM</div>
                                    <div style='font-size: 16px;'>★</div>
                                </div>
                            </div>

                            <div class='footer-section'>
                                <div class='footer-item'>
                                    <div class='signature-font'>{completionDate}</div>
                                    <div class='signature-line'>Date of Issue</div>
                                </div>
                                <div class='footer-item'>
                                    <div class='signature-font'>MSF Learning</div>
                                    <div class='signature-line'>Authorized Signature</div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </body>
            </html>";

			var bytes = Encoding.UTF8.GetBytes(htmlTemplate);
			return Convert.ToBase64String(bytes);
		}

		public async Task<List<CourseResponseHomeModel>> GetAllHomeCoursePageAsync(int page, int pageSize, int? courseTypeId, int? priceOrder, string search = "")
		{
			return await _courseRepository.GetAllHomeCoursePageAsync(page, pageSize,courseTypeId,priceOrder,search);
		}

		public async Task<List<CourseResponseModel>> GetAllManaCourseByUserIdAsync(int page, int pageSize, string search = "")
		{
			var userId = _claimService.GetUserId();
			return await _courseRepository.GetAllManaCourseByUserIdAsync(userId.Value,page, pageSize,search);
		}



		public async Task<CourseResponseHomeModel> GetCourseDetailsById(int courseId)
		{
			return await _courseRepository.GetCourseDetailsById(courseId);
		}

		public async Task<string> RemoveCourseById(int courseId)
		{
			var userId = _claimService.GetUserId();
			return await _courseRepository.RemoveCourseById(courseId,userId.Value);
		}

		public async Task<string> UpdateCourseAsync(CourseRequestModel courseRequestModel, int courseId)
		{
			var userId = _claimService.GetUserId();
			return await _courseRepository.UpdateCourseAsync(courseRequestModel, courseId,userId.Value);
		}


	}
}
