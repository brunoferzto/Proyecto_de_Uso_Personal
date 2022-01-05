using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization; 

namespace Entidades
{
    public class Sustituciones
    {
        int id, minuto; 
        Jugadores jugadorsale;
        Jugadores jugadoringresa;

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
        public Jugadores Jugadorsale
        {
            get { return jugadorsale; }
            set { jugadorsale = value; }
        }

        [DataMember]
        public Jugadores Jugadoringresa
        {
            get { return jugadoringresa; }
            set { jugadoringresa = value; }
        }

        public Sustituciones (int id, Jugadores jugadorSALE, int minuto, Jugadores jugadorENTRA)
        {
            ID = id;
            Jugadorsale = jugadorSALE;
            Minuto = minuto;
            Jugadoringresa = jugadorENTRA;
        }


    }
}
