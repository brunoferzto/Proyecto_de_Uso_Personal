using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entidades
{
    [DataContract]
   public class DatosGenerales
    {
        int id, goles, asistencias, amarillas, rojas, global;
        Jugadores jugador;
        Equipos equipo;
        Competencias competencia;

        [DataMember]
        public Competencias Competencia
        {
            get { return competencia; }
            set
            {
                if (value != null)
                    competencia = value;
                else
                    throw new Exception("Se necesita Competencia");
            }
        }

        [DataMember]
        public Equipos Equipo
        {
            get { return equipo; }
            set { equipo = value; }
        }

        [DataMember]
        public Jugadores Jugador
        {
            get { return jugador; }
            set { jugador = value; }
        }

        [DataMember]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public int Goles
        {
            get { return goles; }
            set { goles = value; }
        }

        [DataMember]
        public int Asistencias
        {
            get { return asistencias; }
            set { asistencias = value; }
        }

        [DataMember]
        public int Amarillas
        {
            get { return amarillas; }
            set { amarillas = value; }
        }

        [DataMember]
        public int Rojas
        {
            get { return rojas; }
            set { rojas = value; }
        }

        [DataMember]
        public int Global
        {
            get { return global; }
            set { global = value; }
        }

        public DatosGenerales(int id, Jugadores jugador, int goles, int asistencias, int amarillas, int rojas, int global, Equipos equipo,Competencias competencia)
        {
            ID = id;
            Jugador = jugador;
            Goles = goles;
            Asistencias = asistencias;
            Rojas = rojas;
            Global = global;
            Equipo = equipo;
            Competencia = competencia;
        }
        public DatosGenerales() { }
    }
}
