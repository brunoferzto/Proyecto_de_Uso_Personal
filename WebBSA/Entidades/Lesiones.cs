using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entidades
{
    [DataContract]
    public class Lesiones
    {
        int id, diaslesion;
        Temporadas temporada;
        Jugadores jugador;
        DateTime fechalesion, fechavuelta;
        bool curado;

        public bool Curado
        {
            get { return curado; }
            set { curado = value; }
        }
        public DateTime Fechavuelta
        {
            get { return fechavuelta; }
            set { fechavuelta = value; }
        }
        public DateTime Fechalesion
        {
            get { return fechalesion; }
            set { fechalesion = value; }
        }
        public Jugadores Jugador
        {
            get { return jugador; }
            set { jugador = value; }
        }
        public Temporadas Temporada
        {
            get { return temporada; }
            set { temporada = value; }
        }
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public int Diaslesion
        {
            get { return diaslesion; }
            set { diaslesion = value; }
        }

        public Lesiones (int id, Jugadores jugador, int diaslesion, DateTime fechaLesion,
                            DateTime fechaVuelta, Temporadas temporada, bool curado)
        {
            ID = id;
            Jugador = jugador;
            Diaslesion = diaslesion;
            Fechalesion = fechaLesion;
            Fechavuelta = fechaVuelta;
            Temporada = temporada;
            Curado = curado; 
        }

        public Lesiones() { }



    }
}
