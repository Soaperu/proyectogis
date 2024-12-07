using Sigcatmin.pro.Application.Dtos;
using Sigcatmin.pro.Application.Interfaces;
using Sigcatmin.prop.Domain.Interfaces.Repositories;
using Sigcatmin.prop.Domain.Interfaces.Services;
using Sigcatmin.prop.Domain.Settings;

namespace Sigcatmin.pro.Application.UsesCases
{
    public class LoginUseCase
    {
        private readonly JWTSettings jWTSetting;
        private readonly IAuthService _authService;
        private readonly IAuthRepository _authRepository;
        public LoginUseCase(IOptions<JWTSettings> options, 
            IAuthService authService,
            IAuthRepository authRepository)
        {
            jWTSetting = options.Value;
            _authService = authService;
            _authRepository = authRepository;
        }
        public bool Execute(UserDto user)
        {
            var token = _authRepository.Authenticate(user.UserName, user.Password);
            if (token == null)
            {
                return false;
            }

            _authService.SaveSession(user.UserName, user.Password);
           var xd =  _authService.GetSession();
            return true;
        }
    }
}
