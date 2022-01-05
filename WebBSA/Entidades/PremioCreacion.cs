using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entidades
{
    [DataContract]
  public  class PremioCreacion:Premios
    {
        Jugadores jugador;
        DateTime fechaNac;
        int global;

        [DataMember]
        public Jugadores Jugador
        {
            get { return jugador; }
            set { jugador = value; }
        }

        [DataMember]
        public DateTime Nacimiento
        {
            get { return fechaNac; }
            set { fechaNac = value; }
        }

        [DataMember]
        public int Global
        {
            get { return global; }
            set { global = value; }
        }

        public PremioCreacion(Jugadores jugador, DateTime fechaNac, int global, int id, string motivo, DateTime fecha, Competidores competidor, Competencias copa)
            : base(id, motivo, fecha, competidor, copa)
        {
            Jugador = jugador;
            Nacimiento = fechaNac;
            Global = global; 
        }

        public PremioCreacion() { }
    }
}
