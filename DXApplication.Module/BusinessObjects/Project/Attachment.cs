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
    [XafDisplayName("Tệp đính kèm")]
    [NavigationItem(Menu.Manage)]
    [ImageName("file")]
    //[CustomDetailView(Tabbed = true)]
    public class Attachment : BaseObject
    { 
        public Attachment(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
           
        }

        Registration_Amend registration_Amend;
        Registration_Renew registration_Renew;
        Registration_Dossier registration_Dossier;
        DevExpress.Persistent.BaseImpl.FileData fileData;
        string name;

        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }


        public DevExpress.Persistent.BaseImpl.FileData FileData
        {
            get => fileData;
            set => SetPropertyValue(nameof(FileData), ref fileData, value);
        }
        [Association("Registration_Dossier-Attachments")]
        [ModelDefault("AllowEdit","False")]
        [VisibleInListView(false)]
        public Registration_Dossier Registration_Dossier
        {
            get => registration_Dossier;
            set => SetPropertyValue(nameof(Registration_Dossier), ref registration_Dossier, value);
        }
        [Association("Registration_Renew-Attachments")]
        [ModelDefault("AllowEdit", "False")]
        [VisibleInListView(false)]
        public Registration_Renew Registration_Renew
        {
            get => registration_Renew;
            set => SetPropertyValue(nameof(Registration_Renew), ref registration_Renew, value);
        }
        [Association("Registration_Amend-Attachments")]
        [ModelDefault("AllowEdit", "False")]
        [VisibleInListView(false)]
        public Registration_Amend Registration_Amend
        {
            get => registration_Amend;
            set => SetPropertyValue(nameof(Registration_Amend), ref registration_Amend, value);
        }
    }
}