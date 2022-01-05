using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
    public interface IdatosTemporada
    {
        void TemporadaAgregar(Temporadas tempo, UserSuperAdmin usa);
        void TemporadaModificar(Temporadas tempo, User usr);
        void TemporadaEliminar(Temporadas tempo, UserSuperAdmin usa);
        Temporadas TemporadaBuscar(int id);
        List<Temporadas> TemporadasListar();
        void AgregarComentario(string come, Temporadas tempo, UserAdministrador usAm);
        void ModificarComentario(ComentariosTemporada come, UserAdministrador usAm);
        void EliminarComentario(ComentariosTemporada come, User usr);
        List<ComentariosTemporada> ComentariosdeTemporada(int idT);




    }
}
