using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HSR_Helper.DomainLibrary.Domain.Lunchtable;
using HSR_Helper.DomainLibrary.Domain.Userinformation;
using HSR_Helper.DomainLibrary.Domain.Timetable;
using System.ComponentModel;
using RestSharp;
using System.Threading;

namespace HSR_Helper.DomainLibrary.Helper
{
    public delegate void BadgeInformationCallback(BadgeInformation badgeInformation);
    public delegate void TimetableCallback(Timetable timetable,object[] callbackArguments);
    public delegate void LunchtableCallback(Lunchtable lunchtable);

    public static class DomainLibraryHelper
    {
        private const string SvgroupRestUrl = "http://kck.me/svhsr";
        private const string HsrRestUrl = "https://stundenplanws.hsr.ch:4443";

        public static void GetUserBadgeInformation(UserCredentials userCredentials, BadgeInformationCallback callback)
        {
            throw new NotImplementedException("Nur Testdaten vorhanden und scheiss - SOAP Schnittstelle");
        }

        public static void GetUserTimetable(UserCredentials userCredentials, TimetableCallback callback, object[] callbackArguments = null)
        {
            GetUserTimetable(userCredentials, userCredentials.Name, callback, callbackArguments);
        }

        public static void GetUserTimetable(UserCredentials userCredentials, string username, TimetableCallback callback, object[] callbackArguments = null)
        {
            var b = new BackgroundWorker();
            b.DoWork += (sender, args) =>
            {
                if (userCredentials.CredentialsFilled)
                {
                    
                    var restClient = new RestClient(HsrRestUrl);
                    restClient.Authenticator = new HttpBasicAuthenticator(userCredentials.Name, userCredentials.Password);
                    restClient.AddDefaultHeader("Accept", "text/json");
                    restClient.ExecuteAsync(new RestRequest("/api/Timetable/" + username, Method.GET), (response, handle) =>
                    {
                        var timetable = JsonHelper.ParseJson<Timetable>(response);
                        timetable.LastUpdated = DateTime.Now;
                        timetable.Username = username;
                        callback(timetable, callbackArguments);
                    });
                }
                else
                {
                    var timetable = new Timetable();
                    timetable.Semester = "keine userdaten.";
                    var day = new TimetableDay();
                    day.Id = 0;
                    day.Weekday = "error";
                    var lession = new Lession();
                    lession.Name = "userdaten erfassen";
                    lession.CourseAllocations.Add(new CourseAllocation(){Timeslot="um daten abzurufen\nbitte userdaten eingeben!"});
                    day.Lessions.Add(lession);
                    timetable.TimetableDays.Add(day);
                    timetable.LastUpdated = DateTime.Now;
                    callback(timetable, callbackArguments);
                }};
            b.RunWorkerAsync();
        }

        public static void GetLunchtable(LunchtableCallback callback)
        {
            var b = new BackgroundWorker();
            b.DoWork += (sender, args) =>
            {
                var restClient = new RestClient(SvgroupRestUrl);
                restClient.ExecuteAsync(new RestRequest("/days", Method.GET), (response, handle) =>
                {
                    Lunchtable lunchtable;
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        lunchtable = JsonHelper.ParseJson<Lunchtable>(response);
                        callback(lunchtable);
                    }
                    else
                    {
                        var errorMsg = new Dish("Errorbeschreibung", (response.ErrorMessage != null ? response.ErrorMessage : response.StatusCode + ": " + response.StatusDescription));
                        lunchtable = new Lunchtable();
                        var lunchday = new LunchDay("error");
                        lunchday.Dishes.Add(errorMsg);
                        lunchtable.LunchDays.Add(lunchday);
                        callback(lunchtable);
                    }
                });
            };
            b.RunWorkerAsync();
        }
    }
}
