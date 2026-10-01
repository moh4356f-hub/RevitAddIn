using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI;

namespace RevitAddIn.Utilities
{
    public static class Ribbon_Utils
    {
		// Method to add a tab
        public static Result AddRibbonTab(UIControlledApplication uiCtlApp, string tabName)
        {
			try
			{
				uiCtlApp.CreateRibbonTab(tabName);
				return Result.Succeeded;
			}
			catch
			{
				Debug.WriteLine($"Error: Could not create tab {tabName}");
				return Result.Failed;
			}
        }

		// Method to create a panel
		public static RibbonPanel AddRibbonPanelToTab(UIControlledApplication uiCtlApp, string tabName, string panelName)
		{
			try
			{
				return uiCtlApp.CreateRibbonPanel(tabName, panelName);
			}
			catch
			{

				Debug.WriteLine($"Error: Could not add {panelName} to {tabName}");
				return null;
			}
		}

		// Method to get a panel
		public static RibbonPanel GetRibbonPanelByName(UIControlledApplication uiCtlApp, string tabName, string panelName)
		{
			var ribbonPanels = uiCtlApp.GetRibbonPanels(tabName);
			
			foreach(var panel in ribbonPanels)
			{
				if (panel.Name == panelName)
				{
					return panel;
				}
			}

			return null;
		}

        /// <summary>
        /// Method to add button to panel
        /// </summary>
        /// <param name="panel">This is the ribbon panel to add to.</param>
        /// <param name="buttonName">Test</param>
        /// <param name="internalName">Test</param>
        /// <param name="assemblyPath">Test</param>
        /// <param name="className">Test</param>
        /// <returns></returns>
        public static PushButton AddPushButtonToPanel(RibbonPanel panel, string buttonName, string className)
		{
			// If no panel, return an error
			if (panel is null)
			{
                Debug.WriteLine($"Error: Could not create {buttonName}.");
                return null;
			}

			// Get our base name
			var baseName = CommandToBaseName(className);

			PushButtonData pushButtonData = new PushButtonData(baseName, buttonName, Globals.AssemblyPath, className);

			if (panel.AddItem(pushButtonData) is PushButton pushButton)
			{
				pushButton.ToolTip = LookupTooltip(baseName);
				pushButton.Image = GetIcon(baseName, resolution: 16);
				pushButton.LargeImage = GetIcon(baseName, resolution: 32);

				return pushButton;
			}
			else
			{
                Debug.WriteLine($"Error: Could not create {buttonName}.");
                return null;
            }
		}

		// Method to get base name
		public static string CommandToBaseName(string commandName)
		{
			return commandName.Replace("RevitAddIn.Cmds_", "").Replace(".Cmd", "");
		}

		// Method to get a value from a dictionary key
		public static string LookupTooltip(string key, string failValue = null)
		{
			failValue ??= "No tooltip was found.";

			if (Globals.Tooltips.TryGetValue(key, out string value))
			{
				return value;
			}

			return failValue;
		}

		// Method to get an icon as an image source
		public static ImageSource GetIcon(string baseName, int resolution = 32)
		{
			var resourcePath = $"RevitAddIn.Resources.Icons{resolution}.{baseName}{resolution}.png";

			using(var stream = Globals.Assembly.GetManifestResourceStream(resourcePath))
			{
				if (stream is null) { return null; }

				var decoder = new PngBitmapDecoder(
					stream,
					BitmapCreateOptions.PreservePixelFormat,
					BitmapCacheOption.Default);

				if (decoder.Frames.Count > 0)
				{
					return decoder.Frames.First();
				}
				else
				{
					return null;
				}
			}
		}
    }
}
