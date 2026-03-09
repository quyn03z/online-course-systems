using BusinessLogic.Models;
using BusinessLogic.Services.Impl;
using DataAccess.Repositories.Impl;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Serv
{
	public class AuditLogsService : IAuditLogsService
	{
		private readonly IAuditLogsRepository _auditLogsRepository;

		public AuditLogsService(IAuditLogsRepository auditLogsRepository)
		{
			_auditLogsRepository = auditLogsRepository;
		}

		public async Task<(IEnumerable<AuditLogResponseModel> Logs, int TotalCount)> GetPagedAsync(int page, int pageSize)
		{
			var (logs, totalCount) = await _auditLogsRepository.GetPagedAsync(page, pageSize);

			var mappedLogs = logs.Select(l =>
			{
				var model = new AuditLogResponseModel
				{
					AuditLogId = l.AuditLogId,
					UserId = l.UserId,
					Action = l.Action,
					Entity = l.Entity,
					KeyValues = l.KeyValues,
					OldValues = l.OldValues,
					NewValues = l.NewValues,
					CreatedAt = l.CreatedAt,
					User = l.User != null ? new AuditLogUserDto
					{
						UserName = l.User.Username,
						Email = l.User.Email
					} : null
				};

				try
				{
					var oldDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(l.OldValues) ?? new Dictionary<string, object>();
					var newDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(l.NewValues) ?? new Dictionary<string, object>();
					
					string? action = l.Action?.Trim();

					if (string.Equals(action, "Update", StringComparison.OrdinalIgnoreCase) || 
						string.Equals(action, "Modified", StringComparison.OrdinalIgnoreCase))
					{
						foreach (var key in newDict.Keys)
						{
							var oldVal = oldDict.ContainsKey(key) ? oldDict[key]?.ToString() : null;
							var newVal = newDict[key]?.ToString();

							if (oldVal != newVal)
							{
								model.Changes.Add(new AuditLogChangeDto
								{
									PropertyName = key,
									OldValue = oldVal ?? "null",
									NewValue = newVal ?? "null"
								});
							}
						}
					}
					else if (string.Equals(action, "Insert", StringComparison.OrdinalIgnoreCase) || 
							 string.Equals(action, "Added", StringComparison.OrdinalIgnoreCase))
					{
						foreach (var key in newDict.Keys)
						{
							model.Changes.Add(new AuditLogChangeDto
							{
								PropertyName = key,
								OldValue = "",
								NewValue = newDict[key]?.ToString() ?? "null"
							});
						}
					}
					else if (string.Equals(action, "Delete", StringComparison.OrdinalIgnoreCase) || 
							 string.Equals(action, "Deleted", StringComparison.OrdinalIgnoreCase))
					{
						foreach (var key in oldDict.Keys)
						{
							model.Changes.Add(new AuditLogChangeDto
							{
								PropertyName = key,
								OldValue = oldDict[key]?.ToString() ?? "null",
								NewValue = ""
							});
						}
					}
				}
				catch (Exception ex)
				{
					// Fallback if JSON parsing fails
				}

				return model;
			}).ToList();

			return (mappedLogs, totalCount);
		}
		public async Task LogActionAsync(int? userId, string action, string entity, string keyValues = "{}", string oldValues = "{}", string newValues = "{}")
		{
			var auditLog = new AuditLog
			{
				UserId = userId,
				Action = action,
				Entity = entity,
				KeyValues = keyValues,
				OldValues = oldValues,
				NewValues = newValues,
				CreatedAt = DateTime.UtcNow
			};

			await _auditLogsRepository.AddAsync(auditLog);
		}
	}
}
