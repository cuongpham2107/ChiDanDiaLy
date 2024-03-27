using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DXApplication.Blazor.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DXApplication.Module.BusinessObjects.Project
{
    [DefaultClassOptions]
    [NavigationItem(Menu.Manage)]
    [XafDisplayName("Thư viện ảnh")]
    [ImageName("media")]
    //[CustomDetailView(Tabbed = true)]
    public class Media : BaseObject
    { 
        public Media(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
           
        }

        Ingredient ingredient;
        Product product;
        MediaDataObject photo;
        string name;
        [XafDisplayName("Tên")]
        [FieldSize(255)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }

        [XafDisplayName("Hình ảnh")]
        [ImageEditor(ListViewImageEditorCustomHeight = 75, DetailViewImageEditorFixedHeight = 150)]
        public MediaDataObject Photo
        {
            get => photo;
            set => SetPropertyValue(nameof(Photo), ref photo, value);
        }

        [Association("Product-Medias")]
        [ModelDefault("AllowEdit", "False")]
        [VisibleInListView(false)]
        public Product Product
        {
            get => product;
            set => SetPropertyValue(nameof(Product), ref product, value);
        }
        [Association("Ingredient-Medias")]
        [ModelDefault("AllowEdit", "False")]
        [VisibleInListView(false)]
        public Ingredient Ingredient
        {
            get => ingredient;
            set => SetPropertyValue(nameof(Ingredient), ref ingredient, value);
        }

    }
}