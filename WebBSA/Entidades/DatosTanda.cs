using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entidades
{
    [DataContract]
   public class DatosTanda
    {
        int id;
        string tipo;
        Jugadores jugador;

        [DataMember]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public Jugadores Jugador
        {
            get { return jugador; }
            set {if (value != null)
                    jugador = value;
                else
                    throw new Exception("Hace falta Jugador");
            }
        }

        [DataMember]
        public string Tipo
        {
            get { return tipo; }
            set { if(value == "Gol" || value == "Errado" || value =="Jugador")
                  tipo = value;
            else
                     throw new Exception("Tipo erroneo");
                }
        }

        public DatosTanda (int id, Jugadores jugador, string tipo)
        {
            ID = id;
            Jugador = jugador;
            Tipo = tipo;
        }




    }
}
