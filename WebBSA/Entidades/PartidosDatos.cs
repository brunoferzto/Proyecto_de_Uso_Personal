using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization; 

namespace Entidades
{
    [DataContract]
   public class PartidosDatos
    {
        int id, minuto;
        Jugadores jugador;
        Equipos equipo;
        string tipo;

        [DataMember]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public int Minuto
        {
            get { return minuto; }
            set { minuto = value; }
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

        public string Tipo
        {
            get { return tipo; }
            set
            {
                if (value == "Gol" || value == "Asistencia" || value == "Amarilla" || value == ""
                     || value == "Roja" || value == "Doble Amarilla" || value == "Porteria Imbatida" || value == "Gol de Penal"
                     || value == "Penal Atajado" || value == "Penal Errado" || value == "Gol Propia Porteria")
                    tipo = value;
                else
                    throw new Exception("Tipo de Dato incorrecto");
            }


        }

        public PartidosDatos (int id, Jugadores jugador, string tipo, int minuto, Equipos equipo)
        {
            ID = id;
            Jugador = jugador;
            Tipo = tipo;
            Minuto = minuto;
            Equipo = equipo;
        }

        public PartidosDatos() { }
    }
}
