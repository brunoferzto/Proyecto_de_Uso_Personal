using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
   public interface IdatosFases
    {
        Fases Buscar(int idFa);
        void Agregar(Fases fa, User usr);
        void Eliminar(Fases fa, UserSuperAdmin usa);
        void Modificar(Fases fa, User usr);


    }
}
