using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
  public interface IdatosCompetencia
    {
        void CompetenciaAgregar(Competencias copa, UserSuperAdmin usp);
        void CompetenciaModificar(Competencias copa, User usr);
        void CompetenciaEliminar(Competencias copa, UserSuperAdmin usa);
        Competencias CompetenciaBuscar(int id);
        List<Competencias> CompetenciaxTemporada(int idT);

    }
}
