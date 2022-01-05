using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
    public  interface IdatosSuperAdmin
    {
        void AgregarSuperAdministrador(UserSuperAdmin nuevousa, UserSuperAdmin usactual);
        void ModificarSuperAdministrador(UserSuperAdmin nuevousa, UserSuperAdmin usactual);
        void EliminarSuperAdministrador(UserSuperAdmin nuevousa, UserSuperAdmin usactual);
        UserSuperAdmin LogueoSuperAdministrador(UserAdministrador usAdm);
        UserSuperAdmin BuscarSuperAdmin(string nick);

    }
}
