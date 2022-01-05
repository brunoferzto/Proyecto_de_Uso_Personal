using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data.SqlClient;

namespace Datos
{
  public interface IdatosIntegrantes
    {
        List<Integrantes> IntegrantexSubfase(int idSF);

        Integrantes Buscar(int idI);

        void Modificar(Integrantes imod, UserSuperAdmin usa);

        void Agregar(Integrantes imod, int idSf, SqlTransaction sqlt);

        void Eliminar(Integrantes imod, User usr);
    }
}
