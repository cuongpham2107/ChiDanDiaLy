using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
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
    [XafDisplayName("Cấp mới")]
    [ImageName("revision")]
    //[CustomDetailView(Tabbed = true)]
    
    public class Registration_Dossier : Registration
    {
        public Registration_Dossier(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.RegistrationType = Enums.RegistrationType.Dossier;
            this.RegistrationStatus = Enums.RegistrationStatus.New;
        }


        DateTime? submissionDate;
        string officeAddress;
        bool productBrand;
        string consumptionMarket;
        string packageSize;
        string productCategories;
        string projectedOutput;
        string packagingLocations;
        string productionLocations;

        //Đăng ký sử dụng chỉ dẫn địa lý
        [XafDisplayName("Sản xuất tại: ")]
        public string ProductionLocations
        {
            get => productionLocations;
            set => SetPropertyValue(nameof(ProductionLocations), ref productionLocations, value);
        }
        [XafDisplayName("Chế biến, đóng gói tại: ")]
        public string PackagingLocations
        {
            get => packagingLocations;
            set => SetPropertyValue(nameof(PackagingLocations), ref packagingLocations, value);
        }
        [XafDisplayName("Sản lượng dự kiến: ")]
        public string ProjectedOutput
        {
            get => projectedOutput;
            set => SetPropertyValue(nameof(ProjectedOutput), ref projectedOutput, value);
        }
        [XafDisplayName("Chủng loại sản phẩm: ")]
        public string ProductCategories
        {
            get => productCategories;
            set => SetPropertyValue(nameof(ProductCategories), ref productCategories, value);
        }
        [XafDisplayName("Quy trình đóng gói: ")]
        public string PackageSize
        {
            get => packageSize;
            set => SetPropertyValue(nameof(PackageSize), ref packageSize, value);
        }
        [XafDisplayName("Thị trường tiêu thụ: ")]
        public string ConsumptionMarket
        {
            get => consumptionMarket;
            set => SetPropertyValue(nameof(ConsumptionMarket), ref consumptionMarket, value);
        }
        [PersistentAlias("")]
        [XafDisplayName("Có nhãn hiệu không?")]
        [CaptionsForBoolValues("Có", "Không")]
        public bool ProductBrand
        {
            get => productBrand;
            set => SetPropertyValue(nameof(ProductBrand), ref productBrand, value);
        }
        [XafDisplayName("Địa chỉ văn phòng: ")]
        public string OfficeAddress
        {
            get => officeAddress;
            set => SetPropertyValue(nameof(OfficeAddress), ref officeAddress, value);
        }
        [XafDisplayName("Ngày gửi đơn: ")]
        public DateTime? SubmissionDate
        {
            get => submissionDate;
            set => SetPropertyValue(nameof(SubmissionDate), ref submissionDate, value);
        }

        [Association("Registration_Dossier-Files")]
        public XPCollection<File> Files
        {
            get
            {
                return GetCollection<File>(nameof(Files));
            }
        }
    }
}