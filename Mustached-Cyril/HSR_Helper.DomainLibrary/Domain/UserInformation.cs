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
		public virtual Guid Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string Password { get; set; }
	}
}