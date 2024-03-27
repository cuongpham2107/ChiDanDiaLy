using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DXApplication.Blazor.Common;
using DXApplication.Module.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DXApplication.Module.BusinessObjects.Project
{
    [DefaultClassOptions]
    [XafDisplayName("Tổ chức")]
    [ImageName("organization")]
    [NavigationItem(Menu.Manage)]
    //[CustomDetailView(Tabbed = true)]
    public class Organization : BaseObject
    { 
        public Organization(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }
        Enums.OrganizationStatus organizationStatus;
        DateTime? expired;
        string certificateNumber;
        DateTime? dateRange;
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
        [XafDisplayName("Số GCN")]
        public string CertificateNumber
        {
            get => certificateNumber;
            set => SetPropertyValue(nameof(CertificateNumber), ref certificateNumber, value);
        }
        [XafDisplayName("Ngày cấp")]
        public DateTime? DateRange
        {
            get => dateRange;
            set => SetPropertyValue(nameof(DateRange), ref dateRange, value);
        }
        [XafDisplayName("Hết hạn")]
        public DateTime? Expired
        {
            get => expired;
            set => SetPropertyValue(nameof(Expired), ref expired, value);
        }
        [XafDisplayName("Tình trạng")]
        public Enums.OrganizationStatus OrganizationStatus
        {
            get => organizationStatus;
            set => SetPropertyValue(nameof(OrganizationStatus), ref organizationStatus, value);
        }
        [XafDisplayName("Sản phẩm")]
        [Association("Organization-Products")]
        public XPCollection<Product> Products
        {
            get
            {
                return GetCollection<Product>(nameof(Products));
            }
        }
        [XafDisplayName("Nguyên liệu")]
        [Association("Organization-Ingredients")]
        public XPCollection<Ingredient> Ingredients
        {
            get
            {
                return GetCollection<Ingredient>(nameof(Ingredients));
            }
        }

    }
}