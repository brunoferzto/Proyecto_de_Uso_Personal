using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades; 

namespace Datos
{
  public interface IdatosAdmin
    {
        void AgregarAdministrador(UserAdministrador usAdm, UserSuperAdmin usa);
        void ModificarAdministrador(UserAdministrador usAdm, User user);
        void EliminarAdministrador(UserAdministrador usAdm, UserSuperAdmin usa);
        UserAdministrador Logueo(UserAdministrador usAdm);
        UserAdministrador Buscar(string nick);
    }
}
