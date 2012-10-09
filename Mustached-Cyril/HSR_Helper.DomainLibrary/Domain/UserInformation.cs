using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HSR_Helper.DomainLibrary.Domain
{
	public abstract class UserInformation
	{
		public string Name { get; set; }
		public string Password { get; set; }
	}
}