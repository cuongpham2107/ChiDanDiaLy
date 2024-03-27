using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Map.Native;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.XtraPrinting.Native;
using DXApplication.Module.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using static DXApplication.Blazor.Common.Enums;

namespace DXApplication.Module.BusinessObjects.Project
{
    [DefaultClassOptions]
    [XafDisplayName("Đơn đăng ký")]
    [ImageName("registration")]
    [CustomDetailView(FieldsReadonly =  new[] { nameof(RegistrationType), nameof(RegistrationStatus) })]
    //[CustomDetailView(Tabbed = true)]
    [Appearance("StatusNew",BackColor = "CornflowerBlue",FontColor = "Black",FontStyle = DevExpress.Drawing.DXFontStyle.Bold,Context = "Any",AppearanceItemType ="ViewItem",TargetItems = "RegistrationStatus", Criteria = "[RegistrationStatus] = ##Enum#DXApplication.Blazor.Common.Enums+RegistrationStatus,New#")]
    public abstract class Registration : BaseObject
    {
        public Registration(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
           
        }
        RegistrationStatus registrationStatus;
        RegistrationType registrationType;
        string email;
        string phone;
        string address;
        string fullName;
        string nameOfOrganization;
        [XafDisplayName("Tên tổ chức, các nhân")]
        public string NameOfOrganization
        {
            get => nameOfOrganization;
            set => SetPropertyValue(nameof(NameOfOrganization), ref nameOfOrganization, value);
        }
        [XafDisplayName("Họ và tên người đại diện")]
        public string FullName
        {
            get => fullName;
            set => SetPropertyValue(nameof(FullName), ref fullName, value);
        }
        [XafDisplayName("Địa chỉ")]
        public string Address
        {
            get => address;
            set => SetPropertyValue(nameof(Address), ref address, value);
        }
        [XafDisplayName("Số điện thoại")]
        public string Phone
        {
            get => phone;
            set => SetPropertyValue(nameof(Phone), ref phone, value);
        }
        [XafDisplayName("Địa chỉ email")]
        public string Email
        {
            get => email;
            set => SetPropertyValue(nameof(Email), ref email, value);
        }
        [XafDisplayName("Loại hồ sơ")]
        public RegistrationType RegistrationType
        {
            get => registrationType;
            set
            {
                SetPropertyValue(nameof(RegistrationType), ref registrationType, value);
            }
        }
        [XafDisplayName("Trạng thái")]
        public RegistrationStatus RegistrationStatus
        {
            get => registrationStatus;
            set => SetPropertyValue(nameof(RegistrationStatus), ref registrationStatus, value);
        }

    }
}