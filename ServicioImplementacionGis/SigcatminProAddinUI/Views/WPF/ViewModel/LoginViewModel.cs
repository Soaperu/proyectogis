using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Sigcatmin.pro.Application.Dtos.Request;
using Sigcatmin.pro.Application.Interfaces;
using Sigcatmin.pro.Application.UsesCases;
using SigcatminProAddinUI.Handlers;
using SigcatminProAddinUI.Resources.Constants;
using SigcatminProAddinUI.Resources.Helpers;
using SigcatminProAddinUI.Resourecs.Constants;
using SigcatminProAddinUI.Services.Interfaces;

namespace SigcatminProAddinUI.Views.WPF.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly LoginUseCase _loginUseCase;
        private readonly ILoggerService _loggerService;
        private readonly IUIStateService _uIStateService;
        private readonly Window _myWindow;

        public LoginViewModel(Window myWindow)
        {
            _loginUseCase = Program.GetService<LoginUseCase>();
            _loggerService = Program.GetService<ILoggerService>();
            _uIStateService = Program.GetService<IUIStateService>();
            _myWindow = myWindow;
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
        }
        public ICommand LoginCommand { get; }

        private string _username;
        private string _password;

        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;

                switch (columnName)
                {
                    case nameof(Username):
                        if (string.IsNullOrWhiteSpace(Username))
                            result = "El nombre de usuario es obligatorio.";
                        break;

                    case nameof(Password):
                        if (string.IsNullOrWhiteSpace(Password))
                            result = "La contraseña es obligatoria.";
                        else if (Password.Length < 6)
                            result = "La contraseña debe tener al menos 6 caracteres.";
                        break;
                }

                return result;
            }
        }
        public string Error => throw new NotImplementedException();
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool CanExecuteLogin(object parameter)
        {
            // El botón estará habilitado solo si no hay errores en los campos
            return string.IsNullOrEmpty(this[nameof(Username)]) && string.IsNullOrEmpty(this[nameof(Password)]);
        }
        private async void ExecuteLogin(object parameter)
        {
            try
            {
                bool isValid = await _loginUseCase.Execute(new LoginRequestDto()
                {
                    UserName = Username,
                    Password = Password
                });
                if (isValid)
                {
                    _uIStateService.ActivateState(UIStateConstants.IsLoggedIn);
                    _myWindow.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowError(ErrorMessage.UnexpectedError, TitlesMessage.Error);
                _loggerService.LogError(ex);
            }
        }

    }
}
