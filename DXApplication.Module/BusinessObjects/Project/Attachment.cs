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

       
        Registration registration;
        FileData fileData;
        string name;

        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }


        public FileData FileData
        {
            get => fileData;
            set => SetPropertyValue(nameof(FileData), ref fileData, value);
        }
        [Association("Registration-Attachments")]
        [ModelDefault("AllowEdit","False")]
        [VisibleInListView(false)]
        public Registration Registration
        {
            get => registration;
            set => SetPropertyValue(nameof(Registration), ref registration, value);
        }
        
    }
}