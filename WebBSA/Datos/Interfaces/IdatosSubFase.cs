using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
   public interface IdatosSubFase
    {
        void AgregarIndependiente(SubFases sub, UserSuperAdmin usa);
        void Modificar(SubFases sub, UserSuperAdmin usa);
        void Eliminar(SubFases sub, UserSuperAdmin usa);
        List<SubFases> SubFasexFase(int idFA);
        SubFases BuscarIndividual(int idsf);
    }
}
