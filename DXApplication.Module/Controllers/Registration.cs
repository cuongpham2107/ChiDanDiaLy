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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXApplication.Module.Extension;
using DXApplication.Blazor.Common;
using DevExpress.Utils;

namespace DXApplication.Module.Controllers
{
    public partial class Registration : CustomController
    {
       
        public Registration()
        {
            //InitializeComponent();
            Registration_Dossier_Status();
            Registration_Renew_Status();
            Registration_Amend_Status();
        }
        private void Registration_Dossier_Status()
        {
            var action = CreateAction(
                ActionType.SingleChoice,
                "Dossier_Status"
                ) as SingleChoiceAction;
            action.TargetObjectType = typeof(Registration_Dossier);
            action.TargetViewType = ViewType.DetailView;
            action.TargetViewNesting = Nesting.Root;
            action.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            action.ConfirmationMessage = "Xác nhận chuyển đổi trạng thái không?";
            action.Caption = "Trạng Thái";
            action.ImageName = "clipboard";
            SetSingleChoiceAction(action, new ChoiceActionItem[]
            {
                new ("Đã tiếp nhận",Enums.RegistrationStatus.Received),
                new ("Đăng ký thành công",Enums.RegistrationStatus.SignUpSuccess),
                new ("Thông tin chưa đầy đủ",Enums.RegistrationStatus.InformationIsIncomplete),
                new ("Huỷ hồ sơ xét duyệt",Enums.RegistrationStatus.CancelTheApprovalFile)
            });
            action.Execute += (object s, SingleChoiceActionExecuteEventArgs e) => {

                var obj = e.CurrentObject as Registration_Dossier;

                switch (e.SelectedChoiceActionItem.Data)
                {
                    case (Enums.RegistrationStatus.Received):
                        obj.RegistrationStatus = Enums.RegistrationStatus.Received;
                        break;
                    case (Enums.RegistrationStatus.SignUpSuccess):
                        obj.RegistrationStatus = Enums.RegistrationStatus.SignUpSuccess;
                        break;
                    case (Enums.RegistrationStatus.InformationIsIncomplete):
                        obj.RegistrationStatus = Enums.RegistrationStatus.InformationIsIncomplete;
                        break;
                    case (Enums.RegistrationStatus.CancelTheApprovalFile):
                        obj.RegistrationStatus = Enums.RegistrationStatus.CancelTheApprovalFile;
                        break;
                }
                ObjectSpace.CommitChanges();
                Application.ShowViewStrategy.ShowMessage($"Bạn đã chỉ sửa trạng thái: {e.SelectedChoiceActionItem.Caption}! Cho hồ sơ này");
            };
        }
        private void Registration_Renew_Status()
        {
            var action = CreateAction(
               ActionType.SingleChoice,
               "Renew_Status"
               ) as SingleChoiceAction;
            action.TargetObjectType = typeof(Registration_Renew);
            action.TargetViewType = ViewType.DetailView;
            action.TargetViewNesting = Nesting.Root;
            action.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            action.ConfirmationMessage = "Xác nhận chuyển đổi trạng thái không?";
            action.Caption = "Trạng thái";
            action.ImageName = "clipboard";
            SetSingleChoiceAction(action, new ChoiceActionItem[]
            {
                new ("Đã tiếp nhận",Enums.RegistrationStatus.Received),
                new ("Gia hạn thành công",Enums.RegistrationStatus.RenewalSuccessful),
                new ("Thông tin chưa đầy đủ",Enums.RegistrationStatus.InformationIsIncomplete),
                new ("Huỷ hồ sơ xét duyệt",Enums.RegistrationStatus.CancelTheApprovalFile)
            });
            action.Execute += (object s, SingleChoiceActionExecuteEventArgs e) => {

                var obj = e.CurrentObject as Registration_Renew;

                switch (e.SelectedChoiceActionItem.Data)
                {
                    case (Enums.RegistrationStatus.Received):
                        obj.RegistrationStatus = Enums.RegistrationStatus.Received;
                        break;
                    case (Enums.RegistrationStatus.RenewalSuccessful):
                        obj.RegistrationStatus = Enums.RegistrationStatus.RenewalSuccessful;
                        break;
                    case (Enums.RegistrationStatus.InformationIsIncomplete):
                        obj.RegistrationStatus = Enums.RegistrationStatus.InformationIsIncomplete;
                        break;
                    case (Enums.RegistrationStatus.CancelTheApprovalFile):
                        obj.RegistrationStatus = Enums.RegistrationStatus.CancelTheApprovalFile;
                        break;
                }
                ObjectSpace.CommitChanges();
                Application.ShowViewStrategy.ShowMessage($"Bạn đã chỉ sửa trạng thái: {e.SelectedChoiceActionItem.Caption}! Cho hồ sơ này");
            };
        }
        private void Registration_Amend_Status()
        {
            var action = CreateAction(
               ActionType.SingleChoice,
               "Amend_Status"
               ) as SingleChoiceAction;
            action.TargetObjectType = typeof(Registration_Amend);
            action.TargetViewType = ViewType.DetailView;
            action.TargetViewNesting = Nesting.Root;
            action.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            action.ConfirmationMessage = "Xác nhận chuyển đổi trạng thái không?";
            action.Caption = "Trạng thái";
            action.ImageName = "clipboard";
            SetSingleChoiceAction(action, new ChoiceActionItem[]
            {
                new ("Đã tiếp nhận",Enums.RegistrationStatus.Received),
                new ("Sửa đổi/ Cấp đổi/ Cấp lại thành công",Enums.RegistrationStatus.AmendSuccessful),
                new ("Thông tin chưa đầy đủ",Enums.RegistrationStatus.InformationIsIncomplete),
                new ("Huỷ hồ sơ xét duyệt",Enums.RegistrationStatus.CancelTheApprovalFile)
            });
            action.Execute += (object s, SingleChoiceActionExecuteEventArgs e) => {

                var obj = e.CurrentObject as Registration_Amend;

                switch (e.SelectedChoiceActionItem.Data)
                {
                    case (Enums.RegistrationStatus.Received):
                        obj.RegistrationStatus = Enums.RegistrationStatus.Received;
                        break;
                    case (Enums.RegistrationStatus.AmendSuccessful):
                        obj.RegistrationStatus = Enums.RegistrationStatus.AmendSuccessful;
                        break;
                    case (Enums.RegistrationStatus.InformationIsIncomplete):
                        obj.RegistrationStatus = Enums.RegistrationStatus.InformationIsIncomplete;
                        break;
                    case (Enums.RegistrationStatus.CancelTheApprovalFile):
                        obj.RegistrationStatus = Enums.RegistrationStatus.CancelTheApprovalFile;
                        break;
                }
                ObjectSpace.CommitChanges();
                Application.ShowViewStrategy.ShowMessage($"Bạn đã chỉ sửa trạng thái: {e.SelectedChoiceActionItem.Caption}! Cho hồ sơ này");
            };
        }
    }
}
