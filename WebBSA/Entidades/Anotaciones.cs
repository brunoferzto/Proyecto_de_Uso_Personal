using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entidades
{
    [DataContract]
   public class Anotaciones
    {
        int id;
        string anotacion;
        UserAdministrador usuario;
        DateTime fecha;

        [DataMember]
        public string Anotacion
        {
            get { return anotacion; }
            set {
                if (value.Length <= 250)
                    anotacion = value;

                else
                    throw new Exception("Máximo 250 caracteres");

            }
        }


        [DataMember]
        public UserAdministrador Usuario
        {
            get { return usuario; }
            set {
                if (value != null)
                    usuario = value;
                else
                    throw new Exception("Se necesita Usuario");
            }
                 
        }

        [DataMember]
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        [DataMember]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public Anotaciones (int id, string anotacion, UserAdministrador usu, DateTime fecha)
        {
            ID = id;
            Anotacion = anotacion;
            Usuario = usu;
            Fecha = fecha;

        }

        public Anotaciones() { }


    }
}
