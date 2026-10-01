// System
using System.Reflection;
using Autodesk.Revit.UI;
using ribbonUtils = RevitAddIn.Utilities.Ribbon_Utils;

// This application belongs to the root name space
namespace RevitAddIn
{
    // Implementing the interface
    public class Application : IExternalApplication
    {
        // This will run on startup
        public Result OnStartup(UIControlledApplication uiCtlApp)
        {
            // Collect the controlled application
            var ctlApp = uiCtlApp.ControlledApplication;
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyPath = assembly.Location;

            // Registering globals
            Globals.RegisterProperties(uiCtlApp);
            Globals.RegisterTooltips("RevitAddIn.Resources.Files.Tooltips");

            #region Ribbon setup

            // Add ribbon tab
            ribbonUtils.AddRibbonTab(uiCtlApp, Globals.AddinName);

            // Create panel
            var panelGeneral = ribbonUtils.AddRibbonPanelToTab(uiCtlApp, Globals.AddinName, "General");

            // Add button to panel
            var buttonTest = ribbonUtils.AddPushButtonToPanel(panelGeneral, "Testing",
                "RevitAddIn.Cmds_General.Cmd_Test");

            #endregion

            // Final return
            return Result.Succeeded;
        }

        // This will run on shutdown
        public Result OnShutdown(UIControlledApplication uiCtlApp)
        {
            return Result.Succeeded;
        }
    }
}