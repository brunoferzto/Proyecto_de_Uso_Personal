using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entidades
{
    [DataContract]
    public class UserAdministrador : User
    {
      DateTime fechanto;
        


        [DataMember]
        public DateTime FechaNTO
        {
            get { return fechanto; }
            set { fechanto = value; }
        }

        public UserAdministrador(DateTime nacimiento, string nick, string contra, bool activo)
            :base(nick,  contra, activo)
        {
            FechaNTO = nacimiento;
        }

        public UserAdministrador()
        { }
    }
}