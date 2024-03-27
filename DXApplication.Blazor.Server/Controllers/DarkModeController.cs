using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Blazor.Services;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXApplication.Blazor.Server.Controllers
{
    
    public partial class DarkModeController : WindowController
    {
        public DarkModeController()
        {
            // Create a Simple Action.
            var action = new SimpleAction(this,"DarkMode",PredefinedCategory.View);
            action.Caption = string.Empty;
            action.ImageName = "dark-mode";
            action.Execute += async (s, e) => {
                var themeService = Application.ServiceProvider.GetService<IThemeService>();
                var currentTheme = themeService.CurrentTheme;
                if (currentTheme != null && currentTheme.Caption.Contains("Blazing Dark")){
                    // Obtain the Blazing Dark theme.
                    var theme = themeService.GetThemeByCaption("Litera");
                    // Set the current theme to Blazing Dark.
                    await themeService.SetCurrentThemeAsync(theme);
                }
                else
                {
                    // Obtain the Blazing Dark theme.
                    var theme = themeService.GetThemeByCaption("Blazing Dark");
                    // Set the current theme to Blazing Dark.
                    await themeService.SetCurrentThemeAsync(theme);
                }
                
            };
        } 
    }
}
