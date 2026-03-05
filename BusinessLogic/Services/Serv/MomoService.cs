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
			var rawData = $"partnerCode={_options.Value.PartnerCode}&accessKey={_options.Value.AccessKey}&requestId={model.OrderId}&amount={model.Amount}&orderId={model.OrderId}&orderInfo={model.OrderInfo}&returnUrl={_options.Value.ReturnUrl}&notifyUrl={_options.Value.NotifyUrl}&extraData=";

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
				extraData = "",
				signature = signature
			};
			request.AddParameter("application/json", JsonConvert.SerializeObject(requestData), ParameterType.RequestBody);

			var response = await client.ExecuteAsync(request);

			return JsonConvert.DeserializeObject<MomoCreatePaymentResponeModel>(response.Content);
		}

		public async Task<MomoExecuteResponseModel> PaymentExecuteAsync(IQueryCollection collection, int coursId)
		{
			var amount = await Task.FromResult(collection.First(s => s.Key == "amount").Value);
			var orderInfo = await Task.FromResult(collection.First(s => s.Key == "orderInfo").Value);
			var orderId = await Task.FromResult(collection.First(s => s.Key == "orderId").Value);
			return new MomoExecuteResponseModel()
			{
				Amount = amount,
				OrderId = orderId,
				OrderInfo = orderInfo,
				CourseId = coursId
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
