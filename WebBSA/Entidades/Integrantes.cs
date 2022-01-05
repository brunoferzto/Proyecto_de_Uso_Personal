using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entidades
{
    [DataContract]
    public class Integrantes
    {
        int id, pts, pj, pg, pe, pp, gf, gc, dg,pos;
        Equipos equipo;
        
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
        public int PTS
        {
            get { return pts; }
            set { pts = value; }
        }

        [DataMember]
        public int PJ
        {
            get { return pj; }
            set { pj = value; }
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
        public int GF
        {
            get { return gf; }
            set { gf = value; }
        }

        [DataMember]
        public int GC
        {
            get { return gc; }
            set { gc = value; }
        }

        [DataMember]
        public int DG
        {
            get { return dg; }
            set { dg = value; }
        }

        [DataMember]
        public Equipos Equipo
        {
           get { return equipo; }
            set { if (value != null)
                    equipo = value;
                else
                    throw new Exception("Se necesita un Equipo");
                        
                    }
        }

        public Integrantes (int id,int pos, Equipos equipo, int pts, int pg, int pe, int pp, int gf, int gc, int dg )
        {
            ID = id;
            POS = pos;
            Equipo = equipo;
            PTS = pts;
            PG = pg;
            PE = pe;
            PP = pp;
            GF = gf;
            GC = gc;
            DG = dg; 
        }

        public Integrantes() { }

    }
}
