using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HSR_Helper.DomainLibrary.Domain;
using System.ComponentModel;
using RestSharp;

namespace HSR_Helper.DomainLibrary.Helper
{
    public delegate void BadgeInformationCallback(BadgeInformation badgeInformation);
    public delegate void TimetableCallback(Timetable timetable);
    public delegate void LunchtableCallback(Lunchtable lunchtable);

    public static class DomainLibraryHelper
    {
        private static readonly string SVGROUP_REST_URL = "http://kck.me/svhsr";
        
        public static void GetUserBadgeInformation(UserInformation userInformation, BadgeInformationCallback callback)
        {
            
        }

        public static void GetUserTimetable(UserInformation userInformation, TimetableCallback callback)
        {
            
        }

        public static void GetLunchtable(LunchtableCallback callback)
        {
            var b = new BackgroundWorker();
            b.DoWork += (sender, args) =>
                            {
                                var lunchtable = new Lunchtable();
                                RestClient restClient = new RestClient(SVGROUP_REST_URL);
                                RestRequest request = new RestRequest("/menus", Method.GET);
                                restClient.ExecuteAsync(request, (response, handle) =>
                                                                     {
                                                                         lunchtable.fudi = response.Content;
                                                                         callback(lunchtable);
                                                                     });
                            };
            b.RunWorkerAsync();
        }
    }
}
