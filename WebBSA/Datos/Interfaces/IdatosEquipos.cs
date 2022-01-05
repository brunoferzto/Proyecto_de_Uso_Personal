using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades; 

namespace Datos
{
  public interface IdatosEquipos
    {
        Equipos EquipoActivo(string nombre);
        List<Equipos> BuscarEquipos(string nombre);
        void EquipoAgregar(Equipos eqp, UserSuperAdmin usp);
        void EquipoModificar(Equipos eqp, User us);
        void EquipoEliminar(Equipos eqp, UserSuperAdmin usp);
        void EquipoRCompetidor(Equipos eqp, User us);
        void DescensoIndividual(Equipos eqp, User usu);
        Equipos BuscarEquipo(string nequipo, string ncompetidor);

    }
}
