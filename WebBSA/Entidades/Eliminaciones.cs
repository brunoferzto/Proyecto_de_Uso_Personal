using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entidades
{
    [DataContract]
    public class Eliminaciones
    {
        int id, pos;
        Equipos equipo;
        SubFases subfase; 

        [DataMember]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public int POS
        {
            get { return pos; }
            set { pos = value; }
        }

        [DataMember]
        public Equipos Equipo
        {
            get { return equipo; }
            set { if (value != null)
                    equipo = value;
                else
                    throw new Exception("Hace falta Equipo");
            }
        }

        [DataMember]
        public SubFases SubFase
        {
            get { return subfase; }
            set { if (value != null)
                    subfase = value;
                else
                    throw new Exception("Hace falta Fase");
            }
        }

        public Eliminaciones (int id, int pos, Equipos cuadro, SubFases subfase)
        {
            ID = id;
            POS = pos;
            Equipo = cuadro;
            SubFase = subfase;  
        }

        public Eliminaciones() { }
    }
}
