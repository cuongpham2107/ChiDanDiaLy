using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DXApplication.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Contact")]
    [XafDisplayName("Mẫu email")]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    public class EmailTemplate : BaseObject
    {
        public EmailTemplate(Session session) : base(session) { }

        string name;
        [XafDisplayName("Tên mẫu"), ToolTip("")]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }

        string header;
        [XafDisplayName("Tiêu đề"), ToolTip("")]
        public string Header
        {
            get => header;
            set => SetPropertyValue(nameof(Header), ref header, value);
        }

        string body;
        [XafDisplayName("Nội dung"), ToolTip(""), Size(SizeAttribute.Unlimited)]
        public string Body
        {
            get => body;
            set => SetPropertyValue(nameof(Body), ref body, value);
        }
    }

    
}