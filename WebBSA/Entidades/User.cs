using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entidades
{
    [KnownType(typeof(UserAdministrador))]
    [KnownType(typeof(UserSuperAdmin))]
    [DataContract]
     public class User
    {
        string nick, contraseña;
        bool activo; 

        [DataMember]
        public bool Activo
        {
            get { return activo; }
            set { activo = value; }
        }

        [DataMember]
        public string Nick
        {
            get { return nick; }
            set {
                if (value.Length <= 15)
                    nick = value;
                else
                    throw new Exception("Error en Nombre de Usuario");

            }
        }

        [DataMember]
        public string Contraseña
        {
            get { return contraseña; }
            set
            {
                if (value.All(char.IsLetter) && value.Length > 15 && value .Length < 5)
                    throw new Exception("Error en la Contraseña");
                else
                    contraseña = value;
            }
        }

        public User (string nick , string contra, bool activo)
        {
            Nick = nick;
            Contraseña = contra;
            Activo = activo; 
        }

        public User ()
        { }

    }
}
