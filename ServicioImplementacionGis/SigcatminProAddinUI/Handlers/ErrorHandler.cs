using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Sigcatmin.pro.Application.Interfaces;
using SigcatminProAddinUI.Resourecs.Constants;

namespace SigcatminProAddinUI.Handlers
{
    public class ErrorHandler
    {
        private string _errorMessage = ErrorMessage.UnexpectedError;
        private readonly ILoggerService _loggerService;
        public ErrorHandler(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }
        public void RegisterGlobalException()
        {
            Application.Current.DispatcherUnhandledException += OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
        }
        public void ClearGlobalException()
        {
            Application.Current.DispatcherUnhandledException -= OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException -= OnUnhandledException;
            TaskScheduler.UnobservedTaskException -= OnUnobservedTaskException;
        }
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var exception = e.Exception;
            MessageBox.Show(_errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            _loggerService.LogError(exception, exception.Message);
            // Evita que la aplicación se cierre
            e.Handled = true;
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            MessageBox.Show(_errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            _loggerService.LogError(exception, exception.Message);
        }

        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            var exception = e.Exception as Exception;
            MessageBox.Show(_errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            _loggerService.LogError(exception, exception.Message);
        }
    }
}
