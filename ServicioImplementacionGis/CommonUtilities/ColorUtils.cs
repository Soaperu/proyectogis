using ArcGIS.Core.CIM;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtilities
{
    public static class ColorUtils
    {
        public static CIMColor HexToCimColorRGB(string hexColor)
        {
            if (hexColor.StartsWith("#"))
            {
                hexColor = hexColor.Substring(1);
            }

            // Validar que el color tenga 6 caracteres
            if (hexColor.Length != 6)
            {
                throw new ArgumentException("El color hexadecimal debe tener 6 caracteres.", nameof(hexColor));
            }

            // Convertir las partes del color
            int r = int.Parse(hexColor.Substring(0, 2), NumberStyles.HexNumber);
            int g = int.Parse(hexColor.Substring(2, 2), NumberStyles.HexNumber);
            int b = int.Parse(hexColor.Substring(4, 2), NumberStyles.HexNumber);

            CIMColor resultColor = CIMColor.CreateRGBColor(r, g, b);
            return resultColor;
        }
    }
}
