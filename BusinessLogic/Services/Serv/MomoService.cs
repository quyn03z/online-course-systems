using BusinessLogic.Models.Momo;
using BusinessLogic.Models.Order;
using BusinessLogic.Services.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Serv
{
	public class MomoService : IMomoService
	{
		private readonly IOptions<MoMoConfig> _options;

		public MomoService(IOptions<MoMoConfig> options)
		{
			_options = options;
		}

		public async Task<MomoCreatePaymentResponeModel> CreatePaymentAsync(OrderInfoModel model)
		{
			model.OrderId = DateTime.Now.Ticks.ToString();
			model.OrderInfo = "Khách hàng: " + model.FullName +" " + model.OrderInfo;

			// Encode CourseId into extraData
			var extraData = Convert.ToBase64String(Encoding.UTF8.GetBytes(model.CourseId.ToString()));

			var rawData = $"partnerCode={_options.Value.PartnerCode}&accessKey={_options.Value.AccessKey}&requestId={model.OrderId}&amount={model.Amount}&orderId={model.OrderId}&orderInfo={model.OrderInfo}&returnUrl={_options.Value.ReturnUrl}&notifyUrl={_options.Value.NotifyUrl}&extraData={extraData}";

			var signature = ComputeHmacSha256(rawData, _options.Value.SecretKey);
			var client = new RestClient(_options.Value.MomoApiUrl);
			var request = new RestRequest() { Method = RestSharp.Method.Post };
			request.AddHeader("Content-Type", "application/json; charset=UTF-8");

			var requestData = new
			{
				accessKey = _options.Value.AccessKey,
				partnerCode = _options.Value.PartnerCode,
				requestType = _options.Value.RequestType,
				notifyUrl = _options.Value.NotifyUrl,
				returnUrl = _options.Value.ReturnUrl,
				orderId = model.OrderId,
				amount = model.Amount.ToString(),
				orderInfo = model.OrderInfo,
				requestId = model.OrderId,
				extraData = extraData,
				signature = signature
			};
			request.AddParameter("application/json", JsonConvert.SerializeObject(requestData), ParameterType.RequestBody);

			var response = await client.ExecuteAsync(request);

			return JsonConvert.DeserializeObject<MomoCreatePaymentResponeModel>(response.Content);
		}

		public async Task<MomoExecuteResponseModel> PaymentExecuteAsync(IQueryCollection collection)
		{
			var amount = collection.FirstOrDefault(s => s.Key == "amount").Value.ToString();
			var orderInfo = collection.FirstOrDefault(s => s.Key == "orderInfo").Value.ToString();
			var orderId = collection.FirstOrDefault(s => s.Key == "orderId").Value.ToString();
			var extraDataEncoded = collection.FirstOrDefault(s => s.Key == "extraData").Value.ToString();

			int courseId = 0;
			if (!string.IsNullOrEmpty(extraDataEncoded))
			{
				try
				{
					var extraDataRaw = Encoding.UTF8.GetString(Convert.FromBase64String(extraDataEncoded));
					int.TryParse(extraDataRaw, out courseId);
				}
				catch (Exception) { }
			}

			return new MomoExecuteResponseModel()
			{
				Amount = amount,
				OrderId = orderId,
				OrderInfo = orderInfo,
				CourseId = courseId
			};
		}


		private string ComputeHmacSha256(string message, string secretKey)
		{
			var keyBytes = Encoding.UTF8.GetBytes(secretKey);
			var messageBytes = Encoding.UTF8.GetBytes(message);

			byte[] hashBytes;

			using (var hmac = new HMACSHA256(keyBytes))
			{
				hashBytes = hmac.ComputeHash(messageBytes);
			}

			var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

			return hashString;
		}

	}
}
