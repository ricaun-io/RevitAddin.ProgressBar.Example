using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI;
using System;

namespace RevitAddin.ProgressBar.Example.Revit
{
    [AppLoader]
    public class App : IExternalApplication
    {
        private static RibbonPanel ribbonPanel;
        public Result OnStartup(UIControlledApplication application)
        {
            ribbonPanel = application.CreatePanel("ProgressBar");

            ribbonPanel.CreatePushButton<Commands.CommandCopy100>("Copy")
                .SetLargeImage("Resources/Revit.ico");

            ribbonPanel.CreatePushButton<Commands.CommandCopy100Cancel>("Copy\rCancel")
                .SetLargeImage("Resources/Revit.ico");


            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanel?.Remove();
            return Result.Succeeded;
        }
    }
}