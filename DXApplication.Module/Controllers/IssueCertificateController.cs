using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DXApplication.Module.BusinessObjects.Project;
using DXApplication.Module.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXApplication.Module.Controllers
{
   
    public partial class IssueCertificateController : CustomController 
    {
        public IssueCertificateController()
        {
            IssueCertificateAction(); 
        }
        private void IssueCertificateAction()
        {
            var action = CreateAction(ActionType.Simple, "IssueCertificate") as SimpleAction;
            action.Caption = "Cấp giấy chứng nhận";
            action.TargetViewNesting = Nesting.Root;
            action.TargetViewType = ViewType.DetailView;
            action.TargetObjectType = typeof(Registration_Dossier);
            action.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            action.ImageName = string.Empty;
            action.ConfirmationMessage = "Xác nhận cấp giấy chứng nhận cho cơ sở này!!!";
            action.Execute += (object s, SimpleActionExecuteEventArgs e) =>
            {
                
                if(((DetailView)ObjectSpace.Owner).CurrentObject is Registration_Dossier t) 
                {
                    Organization _o = ObjectSpace.CreateObject<Organization>();
                    var _t = ObjectSpace.GetObject(t);
                    _o.NameOfOrganization = _t.NameOfOrganization;
                    _o.FullName = _t.FullName;
                    _o.Address = _t.Address;
                    _o.Email = _t.Email;
                    _o.Phone = _t.Phone;
                    _o.CertificateNumber = "SKHCN-CTC-" + GenerateRandomString();
                    _o.DateRange = DateTime.Now;
                    _o.Expired = DateTime.Now.AddYears(5);
                    _o.OrganizationStatus = Blazor.Common.Enums.OrganizationStatus.StillValidated;
                    this.ObjectSpace.CommitChanges();
                    Application.ShowViewStrategy.ShowMessage("Cấp giấy chứng nhận thành công", InformationType.Success,5000,InformationPosition.Bottom);
                }
            };
        }
        private string GenerateRandomString()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 8)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
