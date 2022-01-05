using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
   public interface IdatosTHistorica
    {
        void AgregarIndividual(THistorica tablaH, User usr);
        void Modificar(THistorica tablaH, UserSuperAdmin usa);
        void Eliminar(THistorica tablaH, UserSuperAdmin usa);
        List<THistorica> Listar();
        List<THistorica> ListarxEquipo(string nomEquipo);
    }
}
