using System.Xml.Serialization;
using HSR_Helper.DomainLibrary.Persistency;
using HSR_Helper.DomainLibrary.Security;

namespace HSR_Helper.DomainLibrary.Domain.Userinformation
{
	public sealed class UserCredentials : SecureObject
	{
		public string Name { get; set; }
		public string Password { get; set; }
		[XmlIgnore]
		public bool CredentialsFilled {
			get {
				return !string.IsNullOrEmpty (Name) && !string.IsNullOrEmpty (Password);
			}
		}
	}
}