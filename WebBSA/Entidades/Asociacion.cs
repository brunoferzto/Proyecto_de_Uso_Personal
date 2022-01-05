using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization; 

namespace Entidades
{
    [DataContract]
   public class Asociacion
    {
        int id;
        Equipos equipo;
        Jugadores jugador;
        bool estado;

        [DataMember]
        public int ID
        {
            get { return id; }
            set { id = value; }
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
        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public Asociacion (int id, Jugadores jugador, Equipos equipo, bool estado)
        {
            ID = id;
            Jugador = jugador;
            Equipo = equipo;
            Estado = estado; 
        }

        public Asociacion() { }


    }
}
