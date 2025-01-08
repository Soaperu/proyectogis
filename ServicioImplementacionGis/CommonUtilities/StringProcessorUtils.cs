using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommonUtilities
{
    public class StringProcessorUtils
    {
        public static String FormatStringCartaIgn(string input)
        {
             //= "'25-M',";

            // Usamos una expresión regular para detectar el patrón de los números y letras
            string pattern = @"'(\d{2})-(\w)'";

            // Reemplazamos el patrón encontrado
            string result = Regex.Replace(input, pattern, m =>
            {
                // Concatenamos el número y la letra, cambiando la letra a minúscula
                return m.Groups[1].Value + m.Groups[2].Value.ToLower();
            });

            // Eliminamos la coma final si es que existe
            if (result.EndsWith(","))
            {
                result = result.TrimEnd(',');
            }
            return result;
            // Mostramos el resultado
            //Console.WriteLine(result);  // Salida esperada: 25m,11j,01h
        }

        public static string FormatStringCartaIgnForSql(string input)
        {
            // Expresión regular que encuentra los patrones de texto dentro de las comillas
            string pattern = @"'(\d{2})-([A-Za-z])'";

            // Reemplazar el patrón: eliminar el guion, pasar la letra a minúsculas
            string result = Regex.Replace(input, pattern, match =>
            {
                string digits = match.Groups[1].Value;
                string letter = match.Groups[2].Value.ToLower(); // Convertir la letra a minúsculas
                return $"'{digits}{letter}'"; // Formatear y mantener las comillas simples
            });

            // Eliminar la coma final si está presente
            if (result.EndsWith(","))
            {
                result = result.Substring(0, result.Length - 1);
            }

            return result;
        }
        public static string FormatStringCartaIgnForTitle(string input)
        {
            string output = Regex.Replace(input, @"'", "");

            // Si la cadena termina con una coma extra, la eliminamos.
            if (output.EndsWith(","))
            {
                output = output.Substring(0, output.Length - 1);
            }
            return output;
        }

        public static int? GetScaleFromFormatsString(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                return null;

            // Definir el patrón de la expresión regular
            // Este patrón busca un número después de '/' y antes de ')'
            string patron = @"\/(\d+)\)";

            Match match = Regex.Match(cadena, patron);
            if (match.Success && match.Groups.Count > 1)
            {
                if (int.TryParse(match.Groups[1].Value, out int numero))
                    return numero;
            }

            return null;
        }
    }
}
