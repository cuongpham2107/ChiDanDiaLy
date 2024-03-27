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
    [NavigationItem(Menu.Manage)]
    [XafDisplayName("Sản phẩm")]
    [ImageName("product")]
    //[CustomDetailView(Tabbed = true)]
    public class Product : BaseObject
    { 
        public Product(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
           
        }

        Organization organization;
        int price;
        string content;
        string description;
        string name;
        [XafDisplayName("Tên: ")]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }
        [XafDisplayName("Thuộc tổ chức: ")]
        [Association("Organization-Products")]
        public Organization Organization
        {
            get => organization;
            set => SetPropertyValue(nameof(Organization), ref organization, value);
        }
        [XafDisplayName("Mô tả: ")]
        public string Description
        {
            get => description;
            set => SetPropertyValue(nameof(Description), ref description, value);
        }
        [XafDisplayName("Nội dung: ")]
        public string Content
        {
            get => content;
            set => SetPropertyValue(nameof(Content), ref content, value);
        }
        [XafDisplayName("Giá: ")]
        public int Price
        {
            get => price;
            set => SetPropertyValue(nameof(Price), ref price, value);
        }
        [Association("Product-Medias")]
        public XPCollection<Media> Medias
        {
            get
            {
                return GetCollection<Media>(nameof(Medias));
            }
        }
    }
}