using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;

// Associate with general commands
namespace RevitAddIn.Cmds_General
{
    // Example command
    [Transaction(TransactionMode.Manual)]
    public class Cmd_Test : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Collect the document
            UIApplication uiApp = commandData.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Document doc = uiDoc.Document;

            // Code logic here
            TaskDialog.Show("It's working!", doc.Title);


            // Final return
            return Result.Succeeded;
        }
    }
}