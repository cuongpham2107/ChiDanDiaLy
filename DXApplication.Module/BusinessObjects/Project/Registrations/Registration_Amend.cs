﻿using DevExpress.Data.Filtering;
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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DXApplication.Module.BusinessObjects.Project
{
    [DefaultClassOptions]
    [XafDisplayName("Sử đổi/ Cấp đổi/ Cấp lại")]
    [ImageName("revision")]
    //[CustomDetailView(Tabbed = true)]

    public class Registration_Amend : Registration
    {
        public Registration_Amend(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.RegistrationType = Enums.RegistrationType.Amend;
            this.RegistrationStatus = Enums.RegistrationStatus.New;
        }
        string reason;
        DateTime? deadlineToDate;
        DateTime? dateOfIssue;
        string certificateNumber;
        string nameCertificates;
        [XafDisplayName("Đề nghị sửa đổi GCN")]
        public string NameCertificates
        {
            get => nameCertificates;
            set => SetPropertyValue(nameof(NameCertificates), ref nameCertificates, value);
        }
        [XafDisplayName("Số Giấy chứng nhận")]
        public string CertificateNumber
        {
            get => certificateNumber;
            set => SetPropertyValue(nameof(CertificateNumber), ref certificateNumber, value);
        }
        [XafDisplayName("Ngày cấp")]
        public DateTime? DateOfIssue
        {
            get => dateOfIssue;
            set => SetPropertyValue(nameof(DateOfIssue), ref dateOfIssue, value);
        }
        [XafDisplayName("Có thời hạn đến ngày")]
        public DateTime? DeadlineToDate
        {
            get => deadlineToDate;
            set => SetPropertyValue(nameof(DeadlineToDate), ref deadlineToDate, value);
        }
        [XafDisplayName("Lý do")]
        [Size(4094)]
        public string Reason
        {
            get => reason;
            set => SetPropertyValue(nameof(Reason), ref reason, value);
        }
        [DevExpress.Xpo.Association("Registration_Amend-Files")]
        public XPCollection<File> Files
        {
            get
            {
                return GetCollection<File>(nameof(Files));
            }
        }
    }
}