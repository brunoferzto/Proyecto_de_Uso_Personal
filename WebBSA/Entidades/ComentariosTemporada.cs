using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entidades
{
    [DataContract]
   public class ComentariosTemporada
    {
        int id;
        string comentario;
        UserAdministrador usuario;

        [DataMember]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public UserAdministrador Usuario
        {
            get { return usuario; }
            set
            {
                if (value != null)
                    usuario = value;
                else
                    throw new Exception("Se necesita Usuario");
            }

        }

        [DataMember]
        public string Comentario 
        {
            get { return comentario; }
            set
            {
                if (value.Length <= 300)
                    comentario = value;
                else
                    throw new Exception("Demasiados Caracteres");
            }

        }

        public ComentariosTemporada (int id, UserAdministrador usu, string comen)
        {
            ID = id;
            Usuario = usu;
            Comentario = comen;
        }



    }
}
