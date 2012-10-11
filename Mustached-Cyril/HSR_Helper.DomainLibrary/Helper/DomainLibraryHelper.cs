using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HSR_Helper.DomainLibrary.Domain.Lunchtable;
using HSR_Helper.DomainLibrary.Domain.Userinformation;
using HSR_Helper.DomainLibrary.Domain.Timetable;
using System.ComponentModel;
using RestSharp;

namespace HSR_Helper.DomainLibrary.Helper
{
	public delegate void BadgeInformationCallback (BadgeInformation badgeInformation);
	public delegate void TimetableCallback (Timetable timetable);
	public delegate void LunchtableCallback (Lunchtable lunchtable);

	public static class DomainLibraryHelper
	{
		//TODO: Errorhandling
		private const string SvgroupRestUrl = "http://kck.me/svhsr";
		private const string HsrRestUrl = "https://stundenplanws.hsr.ch:4443";

		public static void GetUserBadgeInformation (UserCredentials userCredentials, BadgeInformationCallback callback)
		{
			throw new NotImplementedException ("Kommt am 7.okt");
		}

		public static void GetUserTimetable (UserCredentials userCredentials, TimetableCallback callback)
		{
			if (userCredentials.CredentialsFilled) {
				var b = new BackgroundWorker ();
				b.DoWork += (sender, args) =>
				{
					var restClient = new RestClient (HsrRestUrl);
					restClient.Authenticator = new HttpBasicAuthenticator (userCredentials.Name, userCredentials.Password);
					restClient.AddDefaultHeader ("Accept", "text/json");
					restClient.ExecuteAsync (new RestRequest ("/api/Timetable/" + userCredentials.Name, Method.GET), (response, handle) =>
					{
						var timetable = JsonHelper.ParseJson<Timetable> (response);
						callback (timetable);
					});
				};
				b.RunWorkerAsync ();
			} else {
				var timetable = new Timetable ();
				timetable.Semester = "keine Angaben";
				timetable.TimetableDays.Add (new TimetableDay (){Id = 0, Weekday = "kein Login"});
				callback (timetable);
			}
		}

		public static void GetLunchtable (LunchtableCallback callback)
		{
			var b = new BackgroundWorker ();
			b.DoWork += (sender, args) =>
			{
				var restClient = new RestClient (SvgroupRestUrl);
				restClient.ExecuteAsync (new RestRequest ("/days", Method.GET), (response, handle) =>
				{
					var lunchtable = JsonHelper.ParseJson<Lunchtable> (response);
					callback (lunchtable);
				});
			};
			b.RunWorkerAsync ();
		}
	}
}
