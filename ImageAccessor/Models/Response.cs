using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageAccessor.Models
{
	public class Response
	{
		public Error Error { get; set; } = new Error();
		public object Value { get; set; }
	}

	public class Error
	{
		public int Code { get; set; }
		public string Message { get; set; }
	}
}
