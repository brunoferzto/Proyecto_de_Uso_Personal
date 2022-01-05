using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entidades
{
    [DataContract]
    public class UserSuperAdmin:User
    {
        string nombre;
        
        [DataMember]
        public string Nombre
        {
            get { return nombre; }
            set
            {
                if (nombre.Length <= 20)
                    nombre = value;
                else
                    throw new Exception("Nombre Incorrecto");

            }
        }


        public UserSuperAdmin (string nombre, string nick, string contra, bool activo)
            :base( nick, contra, activo)
        {
            Nombre = nombre;
        }

        public UserSuperAdmin()
        { }

    }
}
