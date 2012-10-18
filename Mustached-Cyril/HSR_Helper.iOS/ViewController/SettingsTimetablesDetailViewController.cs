using System;
using System.Linq;
using MonoTouch.Dialog;
using MonoTouch.UIKit;
using System.Collections.Generic;
using HSR_Helper.DomainLibrary.Domain.Userinformation;
using HSR_Helper.DomainLibrary.Persistency;

namespace HSR_Helper.iOS
{
    public class SettingsTimetablesDetailViewController : DefaultDialogViewController
    {
        private UIAlertView _alert;
        private Section _usernameSection = new Section("Benutzernamen");

        public SettingsTimetablesDetailViewController() : base(new RootElement("Stundenpläne"))
        {
            Root.Add(new Section(""){
				new StyledStringElement("Hinzufügen", AddTimetable){
					BackgroundColor = ApplicationColors.BUTTON_NORMAL
				}
			});
            Root.Add(_usernameSection);
            foreach (var username in ApplicationSettings.Instance.UserTimetablelist.Usernames)
            {
                _usernameSection.Add(new StringElement(username));
            }
            Title = NavigationItem.Title = "Stundenpläne";
            ApplicationSettings.Instance.UserTimetablelist.ObjectChanged += SavePersistentObject;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            AdvancedConfigEdit();
        }

        public override Source CreateSizingSource(bool unevenRows)
        {
            return new TimetablesDatasource(this);
        }

        void AdvancedConfigEdit()
        {
            NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Edit, delegate
            {
                CreateEditableSection(true);
                ReloadData();   
                TableView.SetEditing(true, true);
                AdvancedConfigDone();                
            });
        }
        
        void AdvancedConfigDone()
        {
            NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
            { 
                ReloadData();
                CreateEditableSection(false);
                TableView.SetEditing(false, true);
                AdvancedConfigEdit();
            });
        }

        private void CreateEditableSection(bool editable)
        {
            var elementList = new List<Element>(_usernameSection.Elements);
            _usernameSection.Clear();
            foreach (var element in elementList)
            {
                if (element is StringElement)
                {
                    _usernameSection.Add(CreateEditableElement((elementList.IndexOf(element) + 1).ToString(), (element as StringElement).Caption, editable));
                }
                else
                {
                    _usernameSection.Add(CreateEditableElement(element.Caption, (element as EntryElement).Value ?? (elementList.IndexOf(element) + 1).ToString(), editable));
                    ApplicationSettings.Instance.UserTimetablelist.Usernames [elementList.IndexOf(element)] = (element as EntryElement).Value ?? (elementList.IndexOf(element) + 1).ToString();
                }
            }
        }

        private Element CreateEditableElement(string caption, string content, bool editable)
        {
            if (editable)
            {
                return new EntryElement(caption, "Benutzername", content);
            }
            else
            {
                return new StringElement(content);
            }
        }

        private class TimetablesDatasource : DefaultDialogViewController.Source
        {
            public TimetablesDatasource(DefaultDialogViewController dvc) : base (dvc)
            {
            }

            public override bool CanEditRow(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
            {
                var element = Container.Root [indexPath.Section] [indexPath.Row];
                return (!element.Caption.Equals("Hinzufügen"));
            }

            public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, MonoTouch.Foundation.NSIndexPath indexPath)
            {
                if (editingStyle == UITableViewCellEditingStyle.Delete)
                {
                    var section = Container.Root [indexPath.Section];
                    var element = section [indexPath.Row];
                    section.Remove(element);
                    ApplicationSettings.Instance.UserTimetablelist.Usernames.RemoveAt(indexPath.Row);
                }
            }

            public override bool CanMoveRow(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
            {
                var element = Container.Root [indexPath.Section] [indexPath.Row];
                return (!element.Caption.Equals("Hinzufügen"));
            }

            public override void MoveRow(UITableView tableView, MonoTouch.Foundation.NSIndexPath sourceIndexPath, MonoTouch.Foundation.NSIndexPath destinationIndexPath)
            {
                // TODO: Implement - see: http://go-mono.com/docs/index.aspx?link=T%3aMonoTouch.Foundation.ModelAttribute
            }
        }

        private void AddTimetable()
        {
            _alert = new UIAlertView("Benutzername eingeben", "", new AlertDelegate(_usernameSection), "Abbrechen", new string[]{"Ok"});
            _alert.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
            _alert.Show();
        }

        private class AlertDelegate : UIAlertViewDelegate
        {
            private Section _usernameSection;

            public AlertDelegate(Section usernameSection)
            {
                _usernameSection = usernameSection;
            }

            public override void Dismissed(UIAlertView alertView, int buttonIndex)
            {
                if (buttonIndex == 1 && !ApplicationSettings.Instance.UserTimetablelist.Usernames.Contains(alertView.GetTextField(0).Text) && !String.IsNullOrEmpty(alertView.GetTextField(0).Text))
                {
                    ApplicationSettings.Instance.UserTimetablelist.Usernames.Add(alertView.GetTextField(0).Text);
                    _usernameSection.Add(new StringElement(alertView.GetTextField(0).Text));
                }
            }
        }

        private void SavePersistentObject(PersistentObject obj)
        {
            (obj as UserTimetableList).Usernames.ToList().ForEach(x => Console.WriteLine(x.ToString()));
            ApplicationSettings.Instance.Persistency.Save(obj);
        }
    }
}

