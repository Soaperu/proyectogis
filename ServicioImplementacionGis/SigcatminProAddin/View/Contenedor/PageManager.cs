
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SigcatminProAddin.View.Contenedor
{
    internal static class PageManager
    {
        private static readonly Dictionary<Type, Page> _pages = new Dictionary<Type, Page>();

        /// <summary>
        /// Obtiene o crea una instancia de la página según el tipo proporcionado.
        /// </summary>
        public static Page GetOrCreatePage(Type pageType)
        {
            if (!_pages.ContainsKey(pageType))
            {
                _pages[pageType] = (Page)Activator.CreateInstance(pageType);
            }
            return _pages[pageType];
        }

        public static void RemovePages()
        {
            _pages.Clear();
        }
    }
}
