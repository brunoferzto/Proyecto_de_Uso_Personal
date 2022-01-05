using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades; 

namespace Datos
{
    interface IdatosReglas
    {
        void AgregarReglas(ReglasxPosicion reg, User usr);
        void EliminarRegla(ReglasxPosicion reg, UserSuperAdmin usa);
        void ModificarRegla(ReglasxPosicion reg, UserSuperAdmin usa);
        List<ReglasxPosicion> ReglasxFase(int faid);
        List<ReglasxPosicion> ReglasxSubFase(int faid);
    }
}
