using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HSR_Helper.DomainLibrary.Domain;
using System.ComponentModel;

namespace HSR_Helper.DomainLibrary.Helper
{
    public delegate void BadgeInformationCallback(BadgeInformation badgeInformation);
    public delegate void TimetableCallback(Timetable timetable);
    public delegate void LunchtableCallback(Lunchtable lunchtable);

    public static class DomainLibraryHelper
    {
        
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

                                callback(lunchtable);
                            };
            b.RunWorkerAsync();
        }
    }
}
