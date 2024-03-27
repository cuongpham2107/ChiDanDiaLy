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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXApplication.Module.Extension;
using DXApplication.Module.BusinessObjects.Project;
using DXApplication.Blazor.Common;

namespace DXApplication.Module.Controllers
{
   
    public partial class MailController : CustomController
    {
      
        public MailController()
        {
            SendMailDossier();
            SendMailRenew();
            SendMailAmend();
        }
        private void SendMailDossier()
        {
            var action = CreateAction(ActionType.Simple, "MailDossier") as SimpleAction;
            action.TargetViewNesting = Nesting.Root;
            action.TargetViewType = ViewType.DetailView;
            action.TargetObjectType = typeof(Registration_Dossier);
            action.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            action.Caption = "Gửi Mail";
            action.ImageName = "mail";
            action.TargetObjectsCriteria = "[RegistrationStatus] <> ##Enum#DXApplication.Blazor.Common.Enums+RegistrationStatus,New#";
            action.ConfirmationMessage = "Xác nhận gửi Mail!!!";
            action.Execute += (object s, SimpleActionExecuteEventArgs e) =>
            {
                MailHelper.EmailParameter parameter = MailHelper.GetEmailParams(ObjectSpace);
                var obj = e.CurrentObject as Registration_Dossier;
                if(string.IsNullOrEmpty(obj.Email)) throw new UserFriendlyException("Không có địa chỉ email của tổ chức này.");
                if(obj.RegistrationStatus != Enums.RegistrationStatus.New) 
                    throw new UserFriendlyException($"Hồ sơ đăng kí đã được duyệt rồi! Vui lòng kiểm tra lai");

                string ketqua = "";
                string content = "";
                switch (obj.RegistrationStatus)
                {
                    case Enums.RegistrationStatus.Received:
                        ketqua = "Đã tiếp nhận";
                        content = "Hồ sơ của Tổ chức/ Cá nhân đã được tiếp nhận chúng tôi sẽ liên hệ với Tổ chức/ Cá nhận qua số điện thoại để thống nhất và làm việc trực tiếp tại Cơ sở";
                        break;
                    case Enums.RegistrationStatus.SignUpSuccess:
                        ketqua = "Đăng ký thành công";
                        content = "Hồ sơ của Tổ chức/ Cá nhân đã được đăng ký thành công chúng tôi sẽ liên hệ với Tổ chức/ Cá nhận qua số điện thoại để hẹn ngày có thông báo chính thức về chỉ dẫn địa lý của cơ sở";
                        break;
                    case Enums.RegistrationStatus.InformationIsIncomplete:
                        ketqua = "Thông tin chưa đầy đủ";
                        content = "Hồ sơ của Tổ chức/ Cá nhân thông tin chưa được đầy đủ vui lòng kiểm tra lại và gửi xét duyệt lại với chúng tôi!!!";
                        break;
                    case Enums.RegistrationStatus.CancelTheApprovalFile:
                        ketqua = "Huỷ hồ sơ xét duyệt";
                        content = "Hồ sơ của Tổ chức/ Cá nhân đã bị huỷ xét duyệt vui lòng hiện hệ qua hotline để được trợ giúp";
                        break;
                }
                var (header, body) = MailHelper.GetEmailTemplate("HoSoDangKy", ObjectSpace);
                if (string.IsNullOrEmpty(header))
                    header = "Thông báo về tình trạng xét duyệt hồ sơ";
                if (string.IsNullOrEmpty(body))
                    body = 
                    "Kính gửi Tổ chức/ Cá nhân: {{ten}}." +
                    "<br/>Trân trọng thông báo kết quả kiểm tra hồ sơ như sau:" +
                    "<br/>Ngày kiểm tra hồ sơ đăng ký: {{ngay}}" +
                    "<br/>Kết quả: {{ketqua}} " +
                    "<br/>Cơ quan thực hiện: Sở Khoa Học Công Nghệ Tỉnh Thái Nguyên" +
                    "<br/>Nội dung: {{content}}"
                    ;

                Dictionary<string, string> pairs = new() {
                    { "{{ten}}", obj.NameOfOrganization },
                    { "{{ngay}}",  DateTime.Now.ToString() },
                    { "{{ketqua}}", ketqua },
                    { "{{content}}", content },
                };
                var message = MailHelper.CreateBody(pairs, body);
                MailHelper.SendEmail(parameter.From, obj.Email, header, message, parameter);
                Application.ShowViewStrategy.ShowMessage(
                    $"Gửi mail thành công tới địa chỉ: ${obj.Email}", 
                    InformationType.Success,
                    5000,
                    InformationPosition.Bottom
                    );
            };
        }
        private void SendMailRenew()
        {
            var action = CreateAction(ActionType.Simple, "MailRenew") as SimpleAction;
            action.TargetViewNesting = Nesting.Root;
            action.TargetViewType = ViewType.DetailView;
            action.TargetObjectType = typeof(Registration_Renew);
            action.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            action.Caption = "Gửi Mail";
            action.ImageName = "mail";
            //action.TargetObjectsCriteria = "[RegistrationStatus] <> ##Enum#DXApplication.Blazor.Common.Enums+RegistrationStatus,New#";
            action.ConfirmationMessage = "Xác nhận gửi Mail!!!";
            action.Execute += (object s, SimpleActionExecuteEventArgs e) =>
            {
                MailHelper.EmailParameter parameter = MailHelper.GetEmailParams(ObjectSpace);
                var obj = e.CurrentObject as Registration_Renew;
                if (string.IsNullOrEmpty(obj.Email)) throw new UserFriendlyException("Không có địa chỉ email của tổ chức này.");
                if (obj.RegistrationStatus != Enums.RegistrationStatus.New) throw new UserFriendlyException($"Hồ sơ đăng kí đã được duyệt rồi! Vui lòng kiểm tra lai");
                string ketqua = "";
                string content = "";
                switch (obj.RegistrationStatus)
                {
                    case Enums.RegistrationStatus.Received:
                        ketqua = "Đã tiếp nhận";
                        content = "Hồ sơ của Tổ chức/ Cá nhân đã được tiếp nhận chúng tôi sẽ liên hệ với Tổ chức/ Cá nhận qua số điện thoại để thống nhất và làm việc trực tiếp tại Cơ sở";
                        break;
                    case Enums.RegistrationStatus.RenewalSuccessful:
                        ketqua = "Gia hạn thành công";
                        content = "Sau khi tiếp nhận đơn gia hạn, Hồ sơ của Tổ chức/ Cá nhân đã được gia hạn thành công!!!";
                        break;
                    case Enums.RegistrationStatus.InformationIsIncomplete:
                        ketqua = "Thông tin chưa đầy đủ";
                        content = "Hồ sơ của Tổ chức/ Cá nhân thông tin chưa được đầy đủ vui lòng kiểm tra lại và gửi xét duyệt lại với chúng tôi!!!";
                        break;
                    case Enums.RegistrationStatus.CancelTheApprovalFile:
                        ketqua = "Huỷ hồ sơ xét duyệt";
                        content = "Hồ sơ của Tổ chức/ Cá nhân đã bị huỷ xét duyệt vui lòng hiện hệ qua hotline để được trợ giúp";
                        break;
                }
                var (header, body) = MailHelper.GetEmailTemplate("HoSoDangKy", ObjectSpace);
                if (string.IsNullOrEmpty(header))
                    header = "Thông báo về tình trạng xét duyệt hồ sơ";
                if (string.IsNullOrEmpty(body))
                    body =
                    "Kính gửi Tổ chức/ Cá nhân: {{ten}}." +
                    "<br/>Trân trọng thông báo kết quả kiểm tra hồ sơ như sau:" +
                    "<br/>Ngày kiểm tra hồ sơ gia hạn: {{ngay}}" +
                    "<br/>Kết quả kiểm tra: {{ketqua}} " +
                    "<br/>Cơ quan thực hiện: Sở Khoa Học Công Nghệ Tỉnh Thái Nguyên" +
                    "<br/>Nội dung: {{content}}"
                    ;

                Dictionary<string, string> pairs = new() {
                    { "{{ten}}", obj.NameOfOrganization },
                    { "{{ngay}}",  DateTime.Now.ToString() },
                    { "{{ketqua}}", ketqua },
                    { "{{content}}", content },
                };
                var message = MailHelper.CreateBody(pairs, body);
                MailHelper.SendEmail(parameter.From, obj.Email, header, message, parameter);
                Application.ShowViewStrategy.ShowMessage(
                    $"Gửi mail thành công tới địa chỉ: ${obj.Email}",
                    InformationType.Success,
                    5000,
                    InformationPosition.Bottom
                    );
            };
        }
        private void SendMailAmend()
        {
            var action = CreateAction(ActionType.Simple, "MailAmend") as SimpleAction;
            action.TargetViewNesting = Nesting.Root;
            action.TargetViewType = ViewType.DetailView;
            action.TargetObjectType = typeof(Registration_Amend);
            action.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            action.Caption = "Gửi Mail";
            action.ImageName = "mail";
            //action.TargetObjectsCriteria = "[RegistrationStatus] <> ##Enum#DXApplication.Blazor.Common.Enums+RegistrationStatus,New#";
            action.ConfirmationMessage = "Xác nhận gửi Mail!!!";
            action.Execute += (object s, SimpleActionExecuteEventArgs e) =>
            {
                MailHelper.EmailParameter parameter = MailHelper.GetEmailParams(ObjectSpace);
                var obj = e.CurrentObject as Registration_Amend;
                if (string.IsNullOrEmpty(obj.Email)) 
                    throw new UserFriendlyException("Không có địa chỉ email của tổ chức này.");
                if (obj.RegistrationStatus != Enums.RegistrationStatus.New) 
                    throw new UserFriendlyException($"Hồ sơ đăng kí đã được duyệt rồi! Vui lòng kiểm tra lai");
                string ketqua = "";
                string content = "";
                switch (obj.RegistrationStatus)
                {
                    case Enums.RegistrationStatus.Received:
                        ketqua = "Đã tiếp nhận";
                        content = "Hồ sơ của Tổ chức/ Cá nhân đã được tiếp nhận chúng tôi sẽ liên hệ với Tổ chức/ Cá nhận qua số điện thoại để thống nhất và làm việc trực tiếp tại Cơ sở";
                        break;
                    case Enums.RegistrationStatus.RenewalSuccessful:
                        ketqua = "Sửa đổi/ Cấp đổi/ Cấp lại thành công";
                        content = "Sau khi tiếp nhận đơn Sửa đổi/ Cấp đổi/ Cấp lại, Hồ sơ của Tổ chức/ Cá nhân đã được Sửa đổi/ Cấp đổi/ Cấp lại thành công!!!";
                        break;
                    case Enums.RegistrationStatus.InformationIsIncomplete:
                        ketqua = "Thông tin chưa đầy đủ";
                        content = "Hồ sơ của Tổ chức/ Cá nhân thông tin chưa được đầy đủ vui lòng kiểm tra lại và gửi xét duyệt lại với chúng tôi!!!";
                        break;
                    case Enums.RegistrationStatus.CancelTheApprovalFile:
                        ketqua = "Huỷ hồ sơ xét duyệt";
                        content = "Hồ sơ của Tổ chức/ Cá nhân đã bị huỷ xét duyệt vui lòng hiện hệ qua hotline để được trợ giúp";
                        break;
                }
                var (header, body) = MailHelper.GetEmailTemplate("HoSoDangKy", ObjectSpace);
                if (string.IsNullOrEmpty(header))
                    header = "Thông báo về tình trạng xét duyệt hồ sơ";
                if (string.IsNullOrEmpty(body))
                    body =
                    "Kính gửi Tổ chức/ Cá nhân: {{ten}}." +
                    "<br/>Trân trọng thông báo kết quả kiểm tra hồ sơ như sau:" +
                    "<br/>Ngày kiểm tra hồ sơ Sửa đổi/ Cấp đổi/ Cấp lại: {{ngay}}" +
                    "<br/>Kết quả kiểm tra: {{ketqua}} " +
                    "<br/>Cơ quan thực hiện: Sở Khoa Học Công Nghệ Tỉnh Thái Nguyên" +
                    "<br/>Nội dung: {{content}}"
                    ;

                Dictionary<string, string> pairs = new() {
                    { "{{ten}}", obj.NameOfOrganization },
                    { "{{ngay}}",  DateTime.Now.ToString() },
                    { "{{ketqua}}", ketqua },
                    { "{{content}}", content },
                };
                var message = MailHelper.CreateBody(pairs, body);
                MailHelper.SendEmail(parameter.From, obj.Email, header, message, parameter);
                Application.ShowViewStrategy.ShowMessage(
                    $"Gửi mail thành công tới địa chỉ: ${obj.Email}",
                    InformationType.Success,
                    5000,
                    InformationPosition.Bottom
                    );
            };
        } 
    }  
}
