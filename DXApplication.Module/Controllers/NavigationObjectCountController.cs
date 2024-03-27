using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DXApplication.Blazor.Common;
using DXApplication.Module.BusinessObjects.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXApplication.Module.Controllers
{
    
    public class NavigationObjectCountController<T> : WindowController
    {
        private ShowNavigationItemController navigationController;
        protected virtual string Getstring()
        {
            string nameOfObject = "";
            return nameOfObject;
        }
        protected override void OnFrameAssigned()
        {
            UnsubscribeFromEvents();
            base.OnFrameAssigned();
            navigationController = Frame.GetController<ShowNavigationItemController>();
            if (navigationController != null)
            {
                navigationController.NavigationItemCreated += NavigationItemCreated;
            }
        }
        void NavigationItemCreated(object sender, NavigationItemCreatedEventArgs e)
        {
            var lvId = Application.GetListViewId(typeof(T));
            if (e.ModelNavigationItem.Id == lvId)
            {
                using (IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(T)))
                {
                    //IModelListView modelListView = (IModelListView)e.ModelNavigationItem.View;
                    int objectsCount = objectSpace.GetObjectsCount(typeof(T), CriteriaOperator.Parse("RegistrationStatus = ?",Enums.RegistrationStatus.New));
                    e.NavigationItem.Caption = Getstring() + (objectsCount > 0 ? $" ({objectsCount})" : string.Empty);
                }
            }
        }
        private void UnsubscribeFromEvents()
        {
            if (navigationController != null)
            {
                navigationController.NavigationItemCreated -= NavigationItemCreated;
                navigationController = null;
            }
        }
        protected override void Dispose(bool disposing)
        {
            UnsubscribeFromEvents();
            base.Dispose(disposing);
        }
    }
    public class RegistrationDossierController : NavigationObjectCountController<Registration_Dossier>
    {
        protected override string Getstring()
        {
            string nameOfObject = "Cấp mới"; 
            return nameOfObject; 
        }
    }
    public class RegistrationRenewController: NavigationObjectCountController<Registration_Renew>
    {
        protected override string Getstring()
        {
            string nameOfOjbect = "Gia hạn";
            return nameOfOjbect;
        }
    }
    public class RegistrationAmendController: NavigationObjectCountController <Registration_Amend>
    {
        protected override string Getstring()
        {
            string nameOfOjbect = "Sử đổi/ Cấp đổi/ Cấp lại";
            return nameOfOjbect;
        }
    }
}
