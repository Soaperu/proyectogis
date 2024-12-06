using Sigcatmin.pro.Application.Interfaces;
using Sigcatmin.prop.Domain.Settings;

namespace Sigcatmin.pro.Application.UsesCases
{
    public class LoginUseCase
    {
        private readonly JWTSettings jWTSetting;
        public LoginUseCase(IOptions<JWTSettings> options)
        {
            jWTSetting = options.Value;
        }
        public bool Execute()
        {
            var x = jWTSetting;
                return true;
        }
    }
}
