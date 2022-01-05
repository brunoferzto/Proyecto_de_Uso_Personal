using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization; 

namespace Entidades
{
    [DataContract]
   public class PremioRetencion:Premios
    {
        Equipos equipo;
        bool usado;
        Jugadores jugador; 

        [DataMember]
        public Equipos Equipo
        {
            get { return equipo; }
            set { equipo = value; }
        }

        [DataMember]
        public bool Usado
        {
            get { return usado; }
            set { usado = value; }
        }

        [DataMember]
        public Jugadores Jugador
        {
            get { return jugador; }
            set { jugador = value; }
        }

        public PremioRetencion(Equipos equipo, bool usado, Jugadores jugador, int id, string motivo, DateTime fecha, Competidores competidor, Competencias copa)
            :base( id,  motivo,  fecha,  competidor,  copa)
        {
            Equipo = equipo;
            Usado = usado;
            Jugador = jugador; 
        }

        public PremioRetencion() { }
    }
}
