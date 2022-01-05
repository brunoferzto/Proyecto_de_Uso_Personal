using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entidades
{
    [DataContract]
    public class Jugadores
    {
        int id;
        string nombre, posicion, pais, pierna;
        DateTime nacimiento;

        [DataMember]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public string Nombre
        {
            get { return nombre; }
            set
            {
                if (value.Length <= 50)
                    nombre = value;
                else
                    throw new Exception("Nombre Incorrecto");
            }
        }

        [DataMember]
        public string Posicion
        {
            get { return posicion; }
            set
            {
                if (value.Length <= 20)
                    posicion = value;
                else
                    throw new Exception("Posición");
            }
        }

        [DataMember]
        public string Pais
        {
            get { return pais; }
            set
            {
                if (value.Length <= 30)
                    pais = value;
                else
                    throw new Exception("Pais dato, Incorrecto");
            }
        }

        [DataMember]
        public string Pierna
        {
            get { return pierna; }
            set
            {
                if (value.Length <= 10)
                    pierna = value;
                else
                    throw new Exception("Pierna, dato Incorrecto");
            }
        }

        [DataMember]
        public DateTime Nacimiento
        {
            get { return nacimiento; }
            set { nacimiento = value; }
        }

        public Jugadores (int id, string nombre, string posicion, string pais, DateTime nacimiento)
        {
            ID = id;
            Nombre = nombre;
            Posicion = posicion;
            Pais = pais;
            Nacimiento = nacimiento; 
        }

        public Jugadores() { }

    }
}
