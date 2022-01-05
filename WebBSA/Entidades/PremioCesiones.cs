using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entidades
{
   public class PremioCesiones:Premios
    {
        string tipo;
        Jugadores jugador;
        Equipos equipo;

        [DataMember]
        public string Tipo
        {
            get { return tipo; }
            set
            {
                if (value == "Cesiones" || value == "Ley Porteros")
                    tipo = value;
                else
                    throw new Exception("Tipo de Dato incorrecto");
            }


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

        public PremioCesiones (string tipo, Jugadores jugador, Equipos equipo, int id, string motivo, DateTime fecha, Competidores competidor, Competencias copa)
            :base( id,  motivo,  fecha,  competidor,  copa)
        {
            Tipo = tipo;
            Jugador = jugador;
            Equipo = equipo; 
        }

        public PremioCesiones() { }

    }
}
