using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CommonUtilities.LoginUtil
{
    public class SessionManager
    {
        private static readonly string AppDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyApp");
        private static readonly string SessionFilePath = Path.Combine(AppDataPath, "session.dat");

        public static void SaveSession(string encryptedCredentials)
        {
            if (!Directory.Exists(AppDataPath))
            {
                Directory.CreateDirectory(AppDataPath);
            }
            File.WriteAllText(SessionFilePath, encryptedCredentials); // Guardar el token cifrado
        }

        public static string? LoadSession()
        {
            if (File.Exists(SessionFilePath))
            {
                return File.ReadAllText(SessionFilePath); // Leer el token cifrado
            }
            return null;
        }

        public static void EndSession()
        {
            if (File.Exists(SessionFilePath))
            {
                File.Delete(SessionFilePath); // Eliminar el archivo de sesión
            }
        }
    }
}