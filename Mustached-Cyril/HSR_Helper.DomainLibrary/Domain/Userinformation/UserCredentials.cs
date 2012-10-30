using System.Xml.Serialization;
using HSR_Helper.DomainLibrary.Persistency;
using HSR_Helper.DomainLibrary.Security;

namespace HSR_Helper.DomainLibrary.Domain.Userinformation
{
    public sealed class UserCredentials : SecureObject
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnObjectChanged();
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnObjectChanged();
            }
        }

        [XmlIgnore]
        public bool CredentialsFilled
        {
            get
            {
                return (!Name.Equals("user") && !Password.Equals("pass")) && (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Password));
            }
        }
    }
}