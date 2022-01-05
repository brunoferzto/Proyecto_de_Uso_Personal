using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entidades
{
        [DataContract]
      public class PartidosPuntuacion
    {
        int id,pg,pe,pp, pts;
        Competidores competidor;
        Competencias competencia;

        [DataMember]
        public int PTS
        {
            get { return pts; }
            set { pts = value; }
        }

        [DataMember]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public int PG
        {
            get { return pg; }
            set { pg = value; }
        }

        [DataMember]
        public int PE
        {
            get { return pe; }
            set { pe = value; }
        }

        [DataMember]
        public int PP
        {
            get { return pp; }
            set { pp = value; }
        }

        [DataMember]
        public Competidores Competidor
        {
            get { return competidor; }
            set
            {
                if (value != null)
                    competidor = value;
                else
                    throw new Exception("No hay Competidor");
            }
        }

        [DataMember]
        public Competencias Competencia
        {
            get { return competencia; }
            set
            {
                if (value != null)
                    competencia = value;
                else
                    throw new Exception("Se necesita Competencia");
            }
        }

        public PartidosPuntuacion(int id, Competidores competidor,int pts, int pg, int pe, int pp, Competencias competencia)
        {
            ID = id;
            Competidor = competidor;
            PTS = pts; 
            PG = pg;
            PE = pe;
            PP = pp;
            Competencia = competencia;
        }

        public  PartidosPuntuacion() { }


    }
}
