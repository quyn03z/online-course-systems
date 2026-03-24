using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.RequestAuditModel
{
	public class RequestAuditInfo
	{
		public string IpAddress { get; set; }
		public string UserAgent { get; set; }
		public DateTime RequestTime { get; set; }
	}
}
