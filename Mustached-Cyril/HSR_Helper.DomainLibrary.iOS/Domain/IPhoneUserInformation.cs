using System;
using HSR_Helper.DomainLibrary.Domain;
using MonoTouch.Dialog;

namespace HSR_Helper.DomainLibrary.iOS
{
	public class IPhoneUserInformation : UserInformation
	{

		[Entry ("Benutzername")]
		public override string Name { get; set; }
		[Skip]
		private string
			_password;
		[Password("Passwort")]
		public override string Password { 
			get {
				return _password;
			}
			set {
				_password = value;
				Console.WriteLine ("FUTZ");
			}
		}

		public IPhoneUserInformation ()
		{
			Console.WriteLine ("RUISDUF");
		}

	}
}

