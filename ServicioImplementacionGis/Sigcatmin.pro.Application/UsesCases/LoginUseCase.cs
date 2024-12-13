using Sigcatmin.pro.Application.Dtos;
using Sigcatmin.pro.Application.Interfaces;
using Sigcatmin.prop.Domain.Interfaces.Repositories;
using Sigcatmin.prop.Domain.Interfaces.Services;
using Sigcatmin.prop.Domain.Settings;

namespace Sigcatmin.pro.Application.UsesCases
{
    public class LoginUseCase
    {
        private readonly IAuthService _authService;
        private readonly IAuthRepository _authRepository;
        private readonly ILoggerService _loggerService;
        public LoginUseCase(
            IAuthService authService,
            IAuthRepository authRepository,
            ILoggerService loggerService)
        {
            _authService = authService;
            _authRepository = authRepository;
            _loggerService = loggerService;
        }
        public async ValueTask<bool> Execute(LoginRequestDto user)
        {
            try
            {
                _loggerService.LogInfo("hola");
                bool isValid = await _authRepository.Authenticate(user.UserName, user.Password);
                if (isValid)
                {
                    _authService.SaveSession(user.UserName, user.Password);
                }
                return isValid;

            } catch (Exception ex)
            {
                _loggerService.LogError("", ex);
                return false;
            }
        }
    }
}
