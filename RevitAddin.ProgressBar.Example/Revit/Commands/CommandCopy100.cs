﻿using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using ricaun.Revit.UI.StatusBar;
using ricaun.Revit.UI.StatusBar.Utils;
using System.Collections.Generic;
using System.Linq;

namespace RevitAddin.ProgressBar.Example.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class CommandCopy100 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document document = uidoc.Document;
            View view = uidoc.ActiveView;
            Selection selection = uidoc.Selection;

            var elementsIds = selection.GetElementIds().ToList();

            if (elementsIds.Any())
            {
                using (Transaction transaction = new Transaction(document))
                {
                    transaction.Start("Copy Elements");

                    RevitProgressBarUtils.Run(uiapp.Application.VersionName, 100, (i) =>
                    {
                        ElementTransformUtils.CopyElements(document, elementsIds, XYZ.BasisX * (i + 1));
                    });
                    transaction.Commit();
                }
            }
            else
            {
                BalloonUtils.Show("Select elements.", uiapp.Application.VersionName);
            }

            return Result.Succeeded;
        }
    }
}
