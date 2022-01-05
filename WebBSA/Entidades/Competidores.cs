using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entidades
{
   [DataContract]
   public class Competidores
    {
        string nombre;
        bool activo;

        [DataMember]
        public string Nombre
        {
            get { return nombre; }
            set
            {
                if (value.Length <= 20)
                    nombre = value;
                else
                throw new Exception("Nombre Incorrecto");               
            }
        }

        [DataMember]
        public bool Activo
        {
            get { return activo; }
            set { activo = value; }
        }

        public Competidores (string Nombre, bool Activo)
        {
            Nombre = nombre;
            Activo = activo;         
        }

        public Competidores()
        { }
    }
}
