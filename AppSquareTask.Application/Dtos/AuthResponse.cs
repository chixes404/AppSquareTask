using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSquareTask.Application.Dtos
{
	public class AuthResponse
	{
		public bool Succeeded { get; set; }
		public string Message { get; set; }
		public string Token { get; set; }
		public DateTime TokenExpiration { get; set; }
		public IEnumerable<string> Errors { get; set; }
	}
}

