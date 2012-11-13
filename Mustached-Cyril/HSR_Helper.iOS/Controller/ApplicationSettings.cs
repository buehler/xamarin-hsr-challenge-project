using HSR_Helper.DomainLibrary.Domain.Userinformation;
using HSR_Helper.DomainLibrary.iOS.Persistency;
using HSR_Helper.DomainLibrary.Persistency;

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

        public IPersistency Persistency{ get; set; }

        private ApplicationSettings()
        {
            Persistency = new IPhonePersistency();
        }
    }
}

