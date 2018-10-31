using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace ADMembers.Controllers
{
    public class PrincipalContainer
    {        
        private Principal _principal;
        private string _domainName;

        public PrincipalContainer(Principal principal)
        {            
            this._principal = principal;

            if (IsUser)
            {
                try
                {
                    Title = GetProperty("title");
                }
                catch { }
            }

            try
            {
                _domainName = principal.Sid.Translate(typeof(NTAccount)).ToString();
            }
            catch
            {

            }

            //var list = new StringBuilder();
            //DirectoryEntry obj = this._principal.GetUnderlyingObject() as DirectoryEntry;

            //foreach (PropertyValueCollection item in obj.Properties)
            //{
            //    list.AppendLine(item.PropertyName + " = " + item.Value.ToString());                
            //}
            //list.AppendLine("---------------------------");
            //System.IO.File.AppendAllText("c:\\temp\\ADGroupContent.txt", list.ToString());
        }

        public string Title { get; private set; }

        public string DomainName
        {
            get
            {
                return _domainName;
            }
        }

        

        public string DisplayName
        {
            get
            {
                return (null == _principal ? "" : _principal.DisplayName); 
            }
        }

        public string Name
        {
            get
            {
                return (null == _principal ? "" : _principal.Name);
            }
        }

        public string SamAccountName
        {
            get
            {
                return (null == _principal ? "" : _principal.SamAccountName);
            }
        }

        public string Description
        {
            get
            {
                return (null == _principal ? "" : _principal.Description);
            }
        }


        public bool IsUser
        {
            get
            {
                return Type.Equals("user");
            }
        }

        public string Type
        {
            get
            {
                return _principal.StructuralObjectClass.ToLowerInvariant();
            }
        }

        private String GetProperty(String property)
        {
            DirectoryEntry directoryEntry = _principal.GetUnderlyingObject() as DirectoryEntry;
            if (directoryEntry.Properties.Contains(property))
                return directoryEntry.Properties[property].Value.ToString();
            else
                return String.Empty;
        }
        
    }
}
