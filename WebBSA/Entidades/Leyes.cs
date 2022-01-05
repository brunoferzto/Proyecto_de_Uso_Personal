using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entidades
{
    [DataContract]
   public class Leyes
    {
        int id;
        string ley, tipo;
        DateTime fecha;

        [DataMember]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public string Tipo
        {
            get { return tipo; }
            set
            {
                if (value == "Generales" || value == "Premios" || value == "BSA")
                   tipo = value;
                else
                    throw new Exception("Tipo de Dato incorrecto");
            }


        }

        [DataMember]
        public string Ley
        {
            get { return ley; }
            set{ley = value;
            }


        }

        [DataMember]
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public Leyes (int id, string ley, string tipo, DateTime fecha)
        {
            ID = id;
            Ley = ley;
            Tipo = tipo;
            Fecha = fecha;
        }

        public Leyes() { }

    }

}
