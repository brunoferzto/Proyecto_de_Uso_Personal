using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entidades
{
    [DataContract]
   public class THistorica
    {
        private  int id, pts;
        private Equipos equipo;
        private string nombreFase;
        private SubFases subfase; 

        [DataMember]
        public SubFases Subfase
        {
            get { return subfase; }
            set { if (value != null) subfase = value;
                   else throw new Exception("TablaHistorica.Subfase");
            }
        }
        [DataMember]
        public string NombreFase
        {
            get { return nombreFase; }
            set {if(value == "" || value.Length > 30 )
                    throw new Exception("TablaHistorica.NombreFase");              
                else
                    nombreFase = value;
            }
        }
        
        [DataMember]
        public Equipos Equipo
        {
            get { return equipo; }
            set
            {
                if (value != null)
                    equipo = value;
                else
                    throw new Exception("TablaHistorica.Equipo");

            }
        }

        [DataMember]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public int PTS
        {
            get { return pts; }
            set { pts = value; }
        }

        public THistorica(int id, Equipos equipo, int pts, SubFases subfase, string noFa)
        {
            ID = id;
            Equipo = equipo;
            PTS = pts;
            Subfase = subfase;
            NombreFase = nombreFase;
        }

        public THistorica() { }
    }
}
