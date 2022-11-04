using ADMembers.Internals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace ADMembers.Controllers
{
    /// <summary>
    /// TODO: WRITE SUMMARY FOR MainWindowController
    /// </summary>
    internal class MainWindowController : INotifyPropertyChanged
    {
        private string _adGroup = "";
        public event PropertyChangedEventHandler PropertyChanged;
        private IEnumerable<PrincipalContainer> _members;
        private string _status;
        private string _adUser = "";
        private IEnumerable<PrincipalContainer> _groups;
        private int _selectedTab = 0;
        private PrincipalContainer _selectedGroup;
        private PrincipalContainer _selectedUser;
        private bool _searching = false;
        private Guid? _guid;

        public MainWindowController()
        {
            try
            {
                _adUser = (Environment.UserName ?? ""); 
                if (string.IsNullOrEmpty(_adUser))
                {
                    _adUser = Thread.CurrentPrincipal.Identity.Name.ToLowerInvariant();
                }
                if((_adUser ?? "").IndexOf("\\") > 0)
                {
                    _adUser = _adUser.Split('\\')[1];
                }
            }
            catch { }
        }

        public Guid? ItemGuid
        {
            get
            {
                return _guid;
            }
            set
            {
                _guid = value;
                RaisePropertyChanged("ItemGuid");
            }
        }

        public string ADGroup
        {
            get { return _adGroup; }
            set 
            { 
                _adGroup = value;
                RaisePropertyChanged("ADGroup");
            }
        }

        public bool Enabled
        {
            get { return !_searching; }
        }

        public bool Searching
        {
            get { return _searching; }
            set { _searching = value; RaisePropertyChanged("Enabled"); }
            
        }

        public int SelectedTab
        {
            get { return _selectedTab; }
            set { _selectedTab = value; RaisePropertyChanged("SelectedTab"); }
        }

        public string ADUser
        {
            get { return _adUser; }
            set
            {
                _adUser = value;
                RaisePropertyChanged("ADUser");
            }
        }

        public IEnumerable<PrincipalContainer> Members
        {
            get { return _members; }
            set
            {
                _members = value;
                RaisePropertyChanged("Members");
            }
        }

        public IEnumerable<PrincipalContainer> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                RaisePropertyChanged("Groups");
            }
        }

        public PrincipalContainer SelectedGroup
        {
            get { return _selectedGroup; }
            set { _selectedGroup = value; RaisePropertyChanged("SelectedGroup"); }
        }

        public PrincipalContainer SelectedUser
        {
            get
            {
                return _selectedUser;
            }
            set
            {
                _selectedUser = value; RaisePropertyChanged("SelectedUser");
            }
        }

        public string Status
        {
            get { return _status; }
        }


        public ICommand CopyUserToClipboard
        {
            get
            {
                return new RelayCommand(() =>
                {
                    AddToClipboard(_selectedUser.SamAccountName);

                },() => {
                    return null != _selectedUser;
                });

            }
        }

        private void AddToClipboard(string text)
        {            
            Clipboard.SetText(text);
        }

        public ICommand CopyGroupToClipboard
        {
            get
            {
                return new RelayCommand(() =>
                {
                    AddToClipboard(_selectedGroup.SamAccountName);
                }, () =>
                {
                    return null != _selectedGroup;
                });

            }
        }

        public ICommand SearchUser
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    Searching = true;
                    SelectedUser = null;
                    SetStatus("Searching for {0}...", _adUser);
                    await SearchUserAsync();
                }, () => {
                    return !string.IsNullOrEmpty(_adUser);
                });
            }
        }

        public ICommand GetGroupMembers
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    SelectedTab = 0;
                    ADGroup = _selectedGroup.SamAccountName;
                    if (Search.CanExecute(null))
                    {
                        await Task.Run(() =>
                        {
                            Search.Execute(null);
                        });
                    }

                }, () =>
                {
                    return null != _selectedGroup && _selectedGroup.Type.Equals("group");
                });
            }
        }

        public ICommand GetUsersGroups
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    SelectedTab = 1;
                    ADUser = _selectedUser.SamAccountName;
                    if (SearchUser.CanExecute(null))
                    {
                        await Task.Run(() =>
                        {
                            SearchUser.Execute(null);
                        });
                    }
                }, () =>
                {
                    return null != _selectedUser && _selectedUser.Type.Equals("user"); 
                });
            }
        }

        public ICommand GetGroupsGroups
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    ADGroup = _selectedUser.SamAccountName;
                    if (Search.CanExecute(null))
                    {
                        await Task.Run(() =>
                        {
                            Search.Execute(null);
                        });
                    }
                }, () =>
                {
                    return null != _selectedUser && _selectedUser.Type.Equals("group");
                });
            }
        }

        public Visibility GetUserGroupsVisibility
        {
            get
            {
                return (null != _selectedUser && _selectedUser.Type.Equals("user")) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility GetGroupsGroupsVisibility
        {
            get
            {
                return (null != _selectedUser && _selectedGroup.Type.Equals("group")) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public ICommand Search
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    Searching = true;
                    SelectedGroup = null;
                    SetStatus("Searching for {0}...", _adGroup);
                    await SearchGroupAsync();
                }, () =>
                {
                    return !string.IsNullOrEmpty(_adGroup);
                });
            }
        }

        public ICommand ClearGroups
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Groups = null;
                }, () => Groups != null);
            }
        }

        public ICommand ClearUsers
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Members = null;
                }, () => Members != null);
            }
        }


        private async Task SearchUserAsync()
        {
            var sw = Stopwatch.StartNew();
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain);
            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, _adUser);
            var groups = new List<PrincipalContainer>();
            if (user != null)
            {
                foreach (var g in user.GetGroups())
                {
                    groups.Add(new PrincipalContainer(g));
                }

                SetStatus("{0} groups{1} found. Time: {2} ms", groups.Count, groups.Count == 1 ? "" : "s", sw.ElapsedMilliseconds);
            }
            else
            {
                SetStatus("No user called \"{0}\" was found", _adUser);
            }
            Groups = groups.OrderBy(p => p.Name);
            sw = null;
            Searching = false;
        }

        private async Task SearchGroupAsync()
        {
            var sw = Stopwatch.StartNew();
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain);
            GroupPrincipal group = GroupPrincipal.FindByIdentity(ctx, _adGroup);

            var users = new List<PrincipalContainer>();

            if (group != null)
            {
                ItemGuid = group.Guid;
                try
                {
                    foreach (var p in group.GetMembers())
                    {
                        users.Add(new PrincipalContainer(p));
                    }
                    SetStatus("{0} user{1} found. Time: {2} ms", users.Count, users.Count == 1 ? "" : "s", sw.ElapsedMilliseconds);
                }
                catch(Exception ex)
                {
                    Logger.Log(ex);
                    SetStatus("Error occurred: {0}. More info in log file \"{1}\"", ex.Message, Logger.GetLogFileName());                    
                }
                
            }
            else
            {
                SetStatus("No group called \"{0}\" was found", _adGroup);
            }
            if (null != users)
            {
                Members = users.OrderBy(p => p.DisplayName);
            }
            sw = null;
            Searching = false;
        }

        private void SetStatus(string message, params object[] parameters)
        {
            _status = string.Format(message, parameters);
            RaisePropertyChanged("Status");
        }

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
