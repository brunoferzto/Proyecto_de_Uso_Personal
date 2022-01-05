using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
   public interface IdatosEliminaciones
    {
        void AgregarIndividual(Eliminaciones eli, User usr);
        void Modificar(Eliminaciones eli, UserSuperAdmin usa);
        void Eliminar(Eliminaciones eli, UserSuperAdmin usa);
        Eliminaciones Buscar(int idEl);
        List<Eliminaciones> EliminacionxEquipo(string nomEquipo);
        List<Eliminaciones> EliminacionxCompetencia(string idCopa);


    }
}
