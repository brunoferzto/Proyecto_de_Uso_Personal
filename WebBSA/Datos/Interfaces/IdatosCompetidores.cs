using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
  public interface IdatosCompetidores
    {
        Competidores CompetidorBuscar(string nom);
        List<Competidores> CompetidorListar();
        void CompetidorAgregar(Competidores com, UserSuperAdmin usa);
        void CompetidorModificar(Competidores com, User user);
        void CompetidorEliminar(Competidores com, UserSuperAdmin usa);
    }
}
