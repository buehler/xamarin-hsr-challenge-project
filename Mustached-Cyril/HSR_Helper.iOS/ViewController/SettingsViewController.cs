using System;
using MonoTouch.Dialog;
using HSR_Helper.DomainLibrary.Domain.Timetable;
using HSR_Helper.DomainLibrary.Domain.Lunchtable;
using MonoTouch.UIKit;
using HSR_Helper.DomainLibrary.Persistency;
using System.Collections.Generic;

namespace HSR_Helper.iOS
{
    public class SettingsViewController : DefaultDialogViewController
    {
        public SettingsViewController() : base(new RootElement("Einstellungen"))
        {
            var userEntry = new EntryElement("Benutzername", "benutzername", ApplicationSettings.Instance.UserCredentials.Name);
            var passwordEntry = new EntryElement("Passwort", "passwort", ApplicationSettings.Instance.UserCredentials.Password, true);
            userEntry.AutocorrectionType = MonoTouch.UIKit.UITextAutocorrectionType.No;
            userEntry.AutocapitalizationType = MonoTouch.UIKit.UITextAutocapitalizationType.None;
            userEntry.Changed += UsernameChanged;
            passwordEntry.Changed += PasswordChanged;
            
            
            Root.Add(new Section("Benutzerinformationen"){
                userEntry,
                passwordEntry
            });
            
            Root.Add(new Section("Stundenplaneinstellungen"){
                new StyledStringElement("Andere Stundenpläne", () => {NavigationController.PushViewController(new SettingsTimetablesDetailViewController(), true);}){
                    Accessory = UITableViewCellAccessory.DisclosureIndicator
                }
            });

            Title = "Einstellungen";
            NavigationItem.Title = "Einstellungen";
            TabBarItem.Image = UIImage.FromBundle("Settings-icon");
        }

        private void UsernameChanged(object s, EventArgs e)
        {
            var field = s as EntryElement;
            if (field != null)
            {
                ApplicationSettings.Instance.Persistency.Delete(new HSR_Helper.DomainLibrary.Domain.Timetable.Timetable(){Username = ApplicationSettings.Instance.UserCredentials.Name});
                ApplicationSettings.Instance.UserCredentials.Name = field.Value;
                ApplicationSettings.Instance.Persistency.Save(ApplicationSettings.Instance.UserCredentials);
            }
        }

        private void PasswordChanged(object s, EventArgs e)
        {
            var field = s as EntryElement;
            if (field != null)
            {
                ApplicationSettings.Instance.UserCredentials.Password = field.Value;
                ApplicationSettings.Instance.Persistency.Save(ApplicationSettings.Instance.UserCredentials);
            }
        }

    }
}

