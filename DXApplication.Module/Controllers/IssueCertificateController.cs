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
   
    public class IssueCertificateController<T> : CustomController where T : class
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
            action.TargetObjectType = typeof(T);
            action.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            action.ImageName = string.Empty;
            //action.TargetObjectsCriteria = "[RegistrationStatus] <> ##Enum#DXApplication.Blazor.Common.Enums+RegistrationStatus,New#";
            action.ConfirmationMessage = "Xác nhận cấp giấy chứng nhận cho cơ sở này!!!";
            action.Execute += (object s, SimpleActionExecuteEventArgs e) =>
            {
                var objCurrent = e.CurrentObject as T;
                if (objCurrent != null)
                {

                }
            };
        }
    }
}
