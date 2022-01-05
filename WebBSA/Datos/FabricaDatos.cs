using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
   public class FabricaDatos
    {

        public static IdatosSuperAdmin GetDatosSuperAdmin()
        {
            return (DatosUserSuperAdmin.GetInstancia());
        }

        public static IdatosAdmin GetDatosrAdmin()
        {
            return (DatosUserAdmin.GetInstancia());
        }

        public static IdatosTemporada GetDatosTemporada()
        {
            return (DatosTemporadas.GetInstancia());
        }

        public static IdatosEquipos GetDatosEquipos()
        {
            return (DatosEquipos.GetInstancia());
        }

        public static IdatosCompetidores GetDatosCompetidores()
        {
            return (DatosCompetidores.GetInstancia());
        }
    }
}
