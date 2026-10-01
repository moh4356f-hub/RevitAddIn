using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI;
using System.Collections;
using System.Globalization;
using System.Resources;
using Assembly = System.Reflection.Assembly;

namespace RevitAddIn
{
    public static class Globals
    {
        #region Properties
        // Applications
        public static UIControlledApplication UiCtlApp { get; set; }
        public static ControlledApplication CtlApp { get; set; }
        public static UIApplication UiApp { get; set; }

        // Assembly
        public static Assembly Assembly { get; set; }
        public static string AssemblyPath { get; set; }

        // Revit versions
        public static string RevitVersion { get; set; }
        public static int RevitVersionInt { get; set; }

        // User names
        public static string UsernameRevit { get; set; }
        public static string UsernameWindows { get; set; }

        // Guids and versioning
        public static string AddinVersionNumber { get; set; }
        public static string AddinVersionName { get; set; }
        public static string AddinName { get; set; }
        public static string AddinGuid { get; set; }

        // Dictionaries for resources
        public static Dictionary<string, string> Tooltips { get; set; } = new Dictionary<string, string>();

        #endregion

        #region Register method
        // Method to register properties
        public static void RegisterProperties(UIControlledApplication uiCtlApp)
        {
            UiCtlApp = uiCtlApp;
            CtlApp = uiCtlApp.ControlledApplication;
            // UiApp set on idling

            Assembly = Assembly.GetExecutingAssembly();
            AssemblyPath = Assembly.Location;

            RevitVersion = CtlApp.VersionNumber;
            RevitVersionInt = Int32.Parse(RevitVersion);

            // Revit username set on idling
            UsernameWindows = Environment.UserName;

            AddinVersionNumber = "25.03.01";
            AddinVersionName = "WIP";
            AddinName = "RevitAddIn";
            AddinGuid = "C4E78CA9-5EE5-4C48-B44E-3CA018682F17";
        }

        #endregion

        // Register our tooltips
        public static void RegisterTooltips(string resourcePath)
        {
            var resourceManager = new ResourceManager(resourcePath, typeof(Application).Assembly);
            var resourceSet = resourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);

            foreach(DictionaryEntry entry in resourceSet)
            {
                var key = entry.Key.ToString();
                var value = entry.Value.ToString();

                Tooltips[key] = value;
            }
        }
    }
}
