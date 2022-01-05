using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entidades
{
    [DataContract]
   public class PremioFichajes:Premios
    {
        Jugadores jugador;
        Equipos equipo;
        string equiposale;

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
        public string EquipoSale
        {
            get { return equiposale; }
            set { equiposale = value; }
        }

        public PremioFichajes (Jugadores jugador, Equipos equipo, string equiposale, int id, string motivo, DateTime fecha, Competidores competidor, Competencias copa)
            :base(id,  motivo,  fecha,  competidor,  copa)
        {
            Jugador = jugador;
            Equipo = equipo;
            EquipoSale = equiposale;
        }
        public PremioFichajes() { }

    }
}
