using DevExpress.ExpressApp.DC;

namespace DXApplication.Blazor.Common;


public class Enums
{
    public enum RegistrationType
    {
        [XafDisplayName("Cấp mới")]
        Dossier = 0,
        [XafDisplayName("Gia hạn")]
        Renew = 1,
        [XafDisplayName("Sửa đổi/ Cấp đổi/ Cấp lại")]
        Amend = 2 
    }
    public enum RegistrationStatus
    {
        [XafDisplayName("Mới")]
        New = 0,
        [XafDisplayName("Đã tiếp nhận")]
        Received = 1,
        [XafDisplayName("Đăng ký thành công")]
        SignUpSuccess = 2,
        [XafDisplayName("Gia hạn thành công")]
        RenewalSuccessful = 3,
        [XafDisplayName("Sửa đổi/ Cấp đổi/ Cấp lại thành công")]
        AmendSuccessful = 4,
        [XafDisplayName("Thông tin chưa đầy đủ")]
        InformationIsIncomplete = 5,
        [XafDisplayName("Huỷ hồ sơ xét duyệt")]
        CancelTheApprovalFile = 6,
    }
    public enum OrganizationStatus
    {
        [XafDisplayName("Chưa cấp GCN")]
        NotIssuedYet = 0,
        [XafDisplayName("Còn hiệu lực")]
        StillValidated = 1,
        [XafDisplayName("Sắp hết hạn")]
        AboutToExpire = 2,
        [XafDisplayName("Hết hạn")]
        Expired = 3

    }
}


