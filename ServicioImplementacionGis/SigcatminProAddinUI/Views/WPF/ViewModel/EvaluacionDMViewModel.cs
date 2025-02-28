using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using SigcatminProAddinUI.Models;
using SigcatminProAddinUI.Resources.Extensions;
using SigcatminProAddinUI.Resources.Helpers;
using SigcatminProAddinUI.Resourecs.Constants;
using System.Linq;
using System;
using ActiproSoftware.Windows.Extensions;
using System.Windows;
using Sigcatmin.pro.Application.Contracts.Responses;

namespace SigcatminProAddinUI.Views.WPF.ViewModel
{
    public class EvaluacionDMViewModel
    {
        public List<string> LayersText = new List<string>()
        {
            "Caram",
            "Catastro Forestal",
            "Predio Rural",
            "Limite Departamental",
            "Limite Provincial",
            "Limite Distrital",
            "Centros Poblados",
            "Red Hidrografica",
            "Red Vial"
        };
        public int RadioDefaultValue { get; set; } = 5;
        public IEnumerable<CoordinatedResponse> Coordinates;
        public List<ComboBoxItemGeneric<int>> GetItemsComboTypeConsult()
        {
            return new List<ComboBoxItemGeneric<int>>
            {
                new ComboBoxItemGeneric<int> { Id = 1, DisplayName = "Código" },
                new ComboBoxItemGeneric<int> { Id = 2, DisplayName = "Nombre" },
            };

        }
        public List<ComboBoxItemGeneric<int>> GetItemsComboZona()
        {
            return new List<ComboBoxItemGeneric<int>>
            {
                new ComboBoxItemGeneric<int> { Id = 17, DisplayName = "17" },
                new ComboBoxItemGeneric<int> { Id = 18, DisplayName = "18" },
                new ComboBoxItemGeneric<int> { Id = 19, DisplayName = "19" },
            };
        }
        public List<ComboBoxItemGeneric<int>> GetItemsComboSistema()
        {
            return new List<ComboBoxItemGeneric<int>>
            {
                new ComboBoxItemGeneric<int> { Id =1, DisplayName = "WGS-84" },
                new ComboBoxItemGeneric<int> { Id =2, DisplayName = "PSAD-56" },
            };
        }
        public bool ValidTotalRowsDerechosMineros(int totalRows, string searchValue)
        {
            if (totalRows == 0)
            {
                MessageBoxHelper.ShowWarning(ErrorMessage.NoRecordsFound.FormatMessage(searchValue),
                                                                TitlesMessage.NoMatches);
                return false;
            }
            if (totalRows >= 150)
            {
                MessageBoxHelper.ShowWarning(ErrorMessage.TooManyMatches.FormatMessage(totalRows),
                                                                TitlesMessage.HighMatchLevel);
                return false;
            }

            return true;
        }

        public List<CoordinateModel> GetCoordinatesByTypeSystem(int typeSystem) {
            var firstCoordinate = Coordinates.FirstOrDefault();
            if (typeSystem == Convert.ToInt32(firstCoordinate.CodigoDatum))
            {
                return Coordinates.Select(x => new CoordinateModel
                {
                    Vertice = x.Vertice,
                    Norte = x.Norte,
                    Este = x.Este,
                }).ToList();
            }

            if(typeSystem != Convert.ToInt32(firstCoordinate.CodigoDatum))
            {
                return Coordinates.Select(x => new CoordinateModel
                {
                    Vertice = x.Vertice,
                    Norte = x.NorteEquivalente,
                    Este = x.EsteEquivalente,
                }).ToList();
            }

            return new List<CoordinateModel>();
        }

        public IEnumerable<UIElement> GetPolygonElements(List<CoordinateModel> coordinates, double canvasWidth, double canvasHeight)
        {
            if (coordinates == null || !coordinates.Any())
                return Enumerable.Empty<UIElement>();

            var points = new PointCollection(coordinates.Select(c => new Point(c.Este, c.Norte)));
            var scaledCoordinates = PolygonHelper.ScaleAndCenterCoordinates(points, canvasWidth, canvasHeight);

            var polygon = PolygonHelper.GeneratePolygon(scaledCoordinates);
            var labels = PolygonHelper.GenerateLabelsByPolygon(scaledCoordinates);

            var elements = new List<UIElement> { polygon };
            elements.AddRange(labels);

            return elements;
        }


    }
}
