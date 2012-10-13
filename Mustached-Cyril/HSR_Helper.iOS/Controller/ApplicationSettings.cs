using System;
using System.Linq;
using HSR_Helper.DomainLibrary.iOS.Persistency;
using HSR_Helper.DomainLibrary.Domain.Userinformation;
using HSR_Helper.DomainLibrary.Persistency;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace HSR_Helper.iOS
{
	public class ApplicationSettings
	{
		private static ApplicationSettings _instance;
		public static ApplicationSettings Instance
		{
			get
			{
				if (_instance == null)
					_instance = new ApplicationSettings();
				return _instance;
			}
		}

		private UserCredentials _userCredentials;
		public UserCredentials UserCredentials
		{
			get
			{
				if (_userCredentials == null)
					_userCredentials = Persistency.Load<UserCredentials>();
				return _userCredentials;
			}
		}

		private UserTimetableList _userTimetablelist;
		public UserTimetableList UserTimetablelist
		{
			get
			{
				if (_userTimetablelist == null)
					_userTimetablelist = Persistency.Load<UserTimetableList>();
				return _userTimetablelist;
			}
		}

		public IPhonePersistency Persistency{ get; private set; }

		private ApplicationSettings()
		{
			Persistency = new IPhonePersistency();
		}

		public class UserTimetableList : PersistentObject
		{
			public HashSet<string> Usernames{ get; set; }

			public string LastOpenedTimetable{ get; set; }

			public UserTimetableList()
			{
				Usernames = new HashSet<string>();
			}

			public override bool Equals(object obj)
			{
				var o = obj as UserTimetableList;
				if (o != null)
				{
					return Usernames.SetEquals(o.Usernames);
				}
				return false;
			}

			public override int GetHashCode()
			{
				var o = "";
				foreach (var s in Usernames)
				{
					o += s;
				}
				return o.GetHashCode();
			}
		}
	}
}

