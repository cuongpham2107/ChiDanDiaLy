using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DXApplication.Module.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXApplication.Module.Controllers
{
 
    public partial class RefreshNavigationController : ViewController
    {
        public RefreshNavigationController()
        {
            TargetViewNesting = Nesting.Root;
            TargetObjectType = typeof(IRefreshNavigation);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            View.ObjectSpace.Committed += ObjectSpace_Committed;
        }
        private void ObjectSpace_Committed(object sender, System.EventArgs e)
        {
            ShowNavigationItemController controller = Application.MainWindow.GetController<ShowNavigationItemController>();
            if (controller != null)
            {
                controller.ShowNavigationItemAction.BeginUpdate();
                controller.RecreateNavigationItems();
                controller.ShowNavigationItemAction.EndUpdate();
            }
        }
        protected override void OnDeactivated()
        {
            View.ObjectSpace.Committed -= ObjectSpace_Committed;
            base.OnDeactivated();
        }
    }
}
