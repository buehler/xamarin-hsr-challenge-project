using System;
using HSR_Helper.DomainLibrary.iOS.Persistency;

namespace HSR_Helper.iOS
{
	public class ApplicationSettings
	{
		private static ApplicationSettings _instance;
		public static ApplicationSettings Instance {
			get {
				if (_instance == null)
					_instance = new ApplicationSettings ();
				return _instance;
			}
		}

		public IPhonePersistency Persistency{ get; private set; }

		private ApplicationSettings ()
		{
			Persistency = new IPhonePersistency ();
		}
	}
}

