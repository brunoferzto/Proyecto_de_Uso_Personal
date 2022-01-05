using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entidades
{
    [DataContract]
    public class ReglasxPosicion
    {
        int id, posicion, ptstablaH;
        bool eliminado, desciende,clasifica;

        [DataMember]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public int Posicion
        {
            get { return posicion; }
            set { if (value >= 0)
                    posicion = value;
            else
                    throw new Exception("Posición");
            }
        }

        [DataMember]
        public int PtstablaH
        {
            get { return ptstablaH; }
            set { if (value >= 0)
                    ptstablaH = value;
                else
                    throw new Exception("Puntos Tabla Histórica");        
            }
        }

        [DataMember]
        public bool Eliminado
        {
            get { return eliminado; }
            set { eliminado = value; }
        }

        [DataMember]
        public bool Clasifica
        {
            get { return clasifica; }
            set { clasifica = value; }
        }

        [DataMember]
        public bool Desciende
        {
            get { return desciende; }
            set { desciende = value; }
        }

        public ReglasxPosicion (int id, int posicion, int ptsHist,bool clasifica, bool eliminado, bool desciende)
        {
            ID = id;
            Posicion = posicion;
            PtstablaH = ptsHist;
            Clasifica = clasifica; 
            Eliminado = eliminado;
            Desciende = desciende; 
        }

        public ReglasxPosicion() { }


    }
}
