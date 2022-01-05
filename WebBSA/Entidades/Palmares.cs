using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entidades
{
    [DataContract]
    public class Palmares
    {
        int id; 
        Competencias competencia;
        string nombre;
        Equipos equipo;

        [DataMember]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public Competencias Competencia
        {
            get { return competencia; }
            set { competencia = value; }
        }

        [DataMember]
        public string Nombre
        {
            get { return nombre; }
            set { if (value == "Campeón" || value == "Sub-Campeón")
                    nombre = value;
                else
                    throw new Exception("Nombre incorrecto, Tipo");
                        }

        }

        [DataMember]
        public Equipos Equipo
        {
            get { return equipo;  }
            set { equipo = value; }
        }

        public Palmares (int id, string nombre, Equipos equipo, Competencias competencia)
        {
            ID = id;
            Nombre = nombre;
            Equipo = equipo;
            Competencia = competencia;
        }

        public Palmares() { }


    }
}
