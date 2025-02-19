using ArcGIS.Desktop.Framework.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtilities
{
    public class ProgressBarUtils: IDisposable
    {
        private ProgressDialog _progressDialog;
        private bool _disposed;

        public ProgressBarUtils(string title)
        {
            _progressDialog = new ProgressDialog(title);
        }

        public void Show()
        {
            _progressDialog?.Show();
        }

        public void Hide()
        {
            _progressDialog?.Hide();
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            // Asegurarnos de ocultar el diálogo
            _progressDialog?.Hide();
            _progressDialog = null;

            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
