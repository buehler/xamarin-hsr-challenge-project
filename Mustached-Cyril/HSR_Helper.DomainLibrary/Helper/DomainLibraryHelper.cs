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
    public delegate void BadgeInformationCallback(BadgeInformation badgeInformation);
    public delegate void TimetableCallback(Timetable timetable);
    public delegate void LunchtableCallback(Lunchtable lunchtable);

    public static class DomainLibraryHelper
    {
        private const string SvgroupRestUrl = "http://kck.me/svhsr";
        private const string HsrRestUrl = "";

        public static void GetUserBadgeInformation(UserCredentials userCredentials, BadgeInformationCallback callback)
        {
            throw new NotImplementedException("Kommt am 7.okt");
        }

        public static void GetUserTimetable(UserCredentials userCredentials, TimetableCallback callback)
        {
            throw new NotImplementedException("scheiss adunis fuzzies sölled d'schnittstell usegeh.");
        }

        public static void GetLunchtable(LunchtableCallback callback)
        {
            var b = new BackgroundWorker();
            b.DoWork += (sender, args) =>
                            {
                                var restClient = new RestClient(SvgroupRestUrl);
                                restClient.ExecuteAsync(new RestRequest("/days", Method.GET), (response, handle) =>
                                                                     {
                                                                         var lunchtable = JsonHelper.ParseJson<Lunchtable>(response);
                                                                         callback(lunchtable);
                                                                     });
                            };
            b.RunWorkerAsync();
        }
    }
}
