using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HSR_Helper.DomainLibrary.Persistency;
using HSR_Helper.DomainLibrary.Security;

namespace HSR_Helper.DomainLibrary.Domain
{
	public class UserInformation : ISecureObject, IPersistentObject
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Password { get; set; }
	}
}