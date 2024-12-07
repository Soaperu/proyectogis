﻿using Sigcatmin.prop.Domain.Entities;
using Sigcatmin.prop.Domain.Interfaces.Repositories;
using Sigcatmin.prop.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigcatmin.pro.Persistence.Configuration
{
    public class DbManagerFactory: IDbManagerFactory
    {
        private readonly IUserSessionService _userSession;

        public DbManagerFactory(IUserSessionService userSession)
        {
            _userSession = userSession ?? throw new ArgumentNullException(nameof(userSession));
        }

        public IDbManager CreateDbManager(string connectionString)
        {

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"No se encontró la cadena de conexión");
            }

            // Obtener las credenciales del archivo .dat o desde la sesión
            //var user = _userSession.GetUsername(); // Suponiendo que tienes una sesión de usuario
            //var password = _userSession.GetPassword(); // Igualmente, obteniendo la contraseña de la sesión

            UserSession user = _userSession.GetUserSession();


            return new DbManager(connectionString, user.UserName, user.Password);
        }
    }
}
