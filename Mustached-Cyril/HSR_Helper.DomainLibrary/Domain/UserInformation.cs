using System.Xml.Serialization;
using HSR_Helper.DomainLibrary.Persistency;
using HSR_Helper.DomainLibrary.Security;

namespace HSR_Helper.DomainLibrary.Domain
{
	public sealed class UserInformation : ISecureObject, IPersistentObject
	{
        [XmlIgnore]
		public string Id { 
			get { return GetType().Name; }
		}
		public string Name { get; set; }
		public string Password { get; set; }
	}
}