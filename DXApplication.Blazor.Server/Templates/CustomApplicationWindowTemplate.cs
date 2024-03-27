using DevExpress.Blazor;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Editors.ActionControls;
using DevExpress.ExpressApp.Blazor.Templates;
using DevExpress.ExpressApp.Blazor.Templates.ContextMenu;
using DevExpress.ExpressApp.Blazor.Templates.ContextMenu.ActionControls;
using DevExpress.ExpressApp.Blazor.Templates.Navigation.ActionControls;
using DevExpress.ExpressApp.Blazor.Templates.Security.ActionControls;
using DevExpress.ExpressApp.Blazor.Templates.Toolbar.ActionControls;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Templates.ActionControls;
using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace DXApplication.Blazor.Server.Templates
{
    public class CustomApplicationWindowTemplate : WindowTemplateBase, ISupportActionsToolbarVisibility, ISelectionDependencyToolbar, ISupportListEditorInlineActions, ISupportListEditorContextMenuActions, ITemplateToolbarProvider
    {
        public CustomApplicationWindowTemplate()
        {
            NavigateBackActionControl = new NavigateBackActionControl();
            AddActionControl(NavigateBackActionControl);
            AccountComponent = new AccountComponentAdapter();
            AddActionControls(AccountComponent.ActionControls);
            ShowNavigationItemActionControl = new ShowNavigationItemActionControl();
            AddActionControl(ShowNavigationItemActionControl);

            IsActionsToolbarVisible = true;
            Toolbar = new DxToolbarAdapter(new DxToolbarModel());
            Toolbar.AddActionContainer(nameof(PredefinedCategory.ObjectsCreation));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.RecordsNavigation), ToolbarItemAlignment.Right);
            Toolbar.AddActionContainer(nameof(PredefinedCategory.Save), ToolbarItemAlignment.Left);
            Toolbar.AddActionContainer(nameof(PredefinedCategory.SaveOptions), ToolbarItemAlignment.Left, isDropDown: true, defaultActionId: "SaveAndClose", autoChangeDefaultAction: true);
            Toolbar.AddActionContainer(nameof(PredefinedCategory.Close));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.UndoRedo));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.Edit));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.RecordEdit));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.View), ToolbarItemAlignment.Right);
            Toolbar.AddActionContainer(nameof(PredefinedCategory.Reports));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.Search));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.Filters));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.FullTextSearch));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.Tools));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.Export));
            Toolbar.AddActionContainer(nameof(PredefinedCategory.Unspecified));

            HeaderToolbar = new DxToolbarAdapter(new DxToolbarModel()
            {
                CssClass = "ps-2"
            });
            HeaderToolbar.AddActionContainer(nameof(PredefinedCategory.QuickAccess), ToolbarItemAlignment.Right);
            HeaderToolbar.AddActionContainer(nameof(PredefinedCategory.Notifications), ToolbarItemAlignment.Right);
            HeaderToolbar.AddActionContainer(nameof(PredefinedCategory.Diagnostic), ToolbarItemAlignment.Right);

            ListEditorActionColumnAdapter = new ListEditorActionColumnAdapter();
            ListEditorActionColumnAdapter.AddActionContainer(nameof(PredefinedCategory.ListView));
            ListEditorActionColumnAdapter.AddActionContainer(nameof(PredefinedCategory.Edit));
            ListEditorActionColumnAdapter.AddActionContainer(nameof(PredefinedCategory.RecordEdit));

            DxContextMenuAdapter = new DxContextMenuAdapter(new DxContextMenuModel());
            DxContextMenuAdapter.AddActionContainer(nameof(PredefinedCategory.ObjectsCreation));
            DxContextMenuAdapter.AddActionContainer(nameof(PredefinedCategory.Save));
            DxContextMenuAdapter.AddActionContainer(nameof(PredefinedCategory.SaveOptions), isDropDown: true, defaultActionId: "SaveAndNew", autoChangeDefaultAction: true);
            DxContextMenuAdapter.AddActionContainer(nameof(PredefinedCategory.Edit));
            DxContextMenuAdapter.AddActionContainer(nameof(PredefinedCategory.RecordEdit));
            DxContextMenuAdapter.AddActionContainer(nameof(PredefinedCategory.UndoRedo));
            DxContextMenuAdapter.AddActionContainer(nameof(PredefinedCategory.Print));
            DxContextMenuAdapter.AddActionContainer(nameof(PredefinedCategory.View));
            DxContextMenuAdapter.AddActionContainer(nameof(PredefinedCategory.Export));
            DxContextMenuAdapter.AddActionContainer(nameof(PredefinedCategory.Reports));
            DxContextMenuAdapter.AddActionContainer(nameof(PredefinedCategory.OpenObject));
            DxContextMenuAdapter.AddActionContainer(nameof(PredefinedCategory.Menu));
        }
        protected override IEnumerable<IActionControlContainer> GetActionControlContainers() => Toolbar.ActionContainers.Union(HeaderToolbar.ActionContainers.Union(ListEditorActionColumnAdapter.ActionContainers.Union(DxContextMenuAdapter.ActionContainers)));
        protected override RenderFragment CreateComponent() => CustomApplicationWindowTemplateComponent.Create(this);
        protected override void BeginUpdate()
        {
            base.BeginUpdate();
            ((ISupportUpdate)Toolbar).BeginUpdate();
        }
        protected override void EndUpdate()
        {
            ((ISupportUpdate)Toolbar).EndUpdate();
            base.EndUpdate();
        }
        public bool IsActionsToolbarVisible { get; private set; }
        public NavigateBackActionControl NavigateBackActionControl { get; }
        public AccountComponentAdapter AccountComponent { get; }
        public ShowNavigationItemActionControl ShowNavigationItemActionControl { get; }
        public DxToolbarAdapter Toolbar { get; }
        public DxToolbarAdapter HeaderToolbar { get; }
        public ListEditorActionColumnAdapter ListEditorActionColumnAdapter { get; }
        public DxContextMenuAdapter DxContextMenuAdapter { get; }
        public string AboutInfoString { get; set; }
        void ISupportActionsToolbarVisibility.SetVisible(bool isVisible) => IsActionsToolbarVisible = isVisible;
    }
}
