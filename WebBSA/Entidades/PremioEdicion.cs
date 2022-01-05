using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entidades
{
    [DataContract]
   public class PremioEdicion:Premios
    {
        Jugadores jugador;
        int pts; 

        [DataMember]
        public Jugadores Jugador
        {
            get { return jugador; }
            set { jugador = value; }
        }

        [DataMember]
        public int PTS
        {
            get { return pts; }
            set { pts = value; }
        }

        public PremioEdicion(Jugadores jugador, int pais, int id, string motivo, DateTime fecha, Competidores competidor, Competencias copa)
            :base( id,  motivo,  fecha,  competidor,  copa)
        {
            Jugador = jugador;
            PTS = pts; 
        }

        public PremioEdicion() { }
    }
}
