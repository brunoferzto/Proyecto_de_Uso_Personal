using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entidades
{
    [DataContract]
    public class Cruces
    {
        int id, valor;
        string nombre,detalles;
        Equipos equipo;
        Jugadores jugador;
        Competidores competidor;

        [DataMember]
        public Competidores Competidor
        {
            get { return competidor; }
            set { competidor = value; }
        }

        [DataMember]
        public Jugadores Jugador
        {
            get { return jugador; }
            set { jugador = value; }
        }

        [DataMember]
        public Equipos Equipo
        {
            get { return equipo; }
            set { equipo = value; }
        }

        [DataMember]
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        [DataMember]
        public string Detalles
        {
            get { return detalles; }
            set { detalles = value; }
        }

        [DataMember]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public int Valor
        {
            get { return valor; }
            set { valor = value; }
        }

        public Cruces(int id, string nombre, int valor, Competidores competidor, Equipos equipo, Jugadores jugador)
        {
            ID = id;
            Nombre = nombre;
            Valor = valor;
            Competidor = competidor;
            Equipo = equipo;
            Jugador = jugador;
            Detalles = detalles;
        }
        public Cruces() { }

    }
}
