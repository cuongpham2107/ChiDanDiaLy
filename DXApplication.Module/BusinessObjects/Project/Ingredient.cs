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
    [XafDisplayName("Vùng nguyên liệu")]
    [ImageName("ingredient")]
    //[CustomDetailView(Tabbed = true)]
    public class Ingredient : BaseObject
    { 
        public Ingredient(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.DateOfEntry = DateTime.Now;
        }

        DateTime? dateOfEntry;
        int area;
        int dryTeaYield;
        int yieldOfFreshTea;
        Organization organization;
        [Association("Organization-Ingredients")]
        [XafDisplayName("Tổ chức")]
        public Organization Organization
        {
            get => organization;
            set => SetPropertyValue(nameof(Organization), ref organization, value);
        }
        [XafDisplayName("Diện tích khoảng")]
        public int Area
        {
            get => area;
            set => SetPropertyValue(nameof(Area), ref area, value);
        }
        [XafDisplayName("Sản lượng chè tươi")]
        public int YieldOfFreshTea
        {
            get => yieldOfFreshTea;
            set => SetPropertyValue(nameof(YieldOfFreshTea), ref yieldOfFreshTea, value);
        }
        [XafDisplayName("Sản lượng chè khô")]
        public int DryTeaYield
        {
            get => dryTeaYield;
            set => SetPropertyValue(nameof(DryTeaYield), ref dryTeaYield, value);
        }
        [XafDisplayName("Ngày nhập")]
        [ModelDefault("AllowEdit","False")]
        public DateTime? DateOfEntry
        {
            get => dateOfEntry;
            set => SetPropertyValue(nameof(DateOfEntry), ref dateOfEntry, value);
        }
        [Association("Ingredient-Medias")]
        [XafDisplayName("Hình ảnh")]
        public XPCollection<Media> Medias
        {
            get
            {
                return GetCollection<Media>(nameof(Medias));
            }
        }
    }
}