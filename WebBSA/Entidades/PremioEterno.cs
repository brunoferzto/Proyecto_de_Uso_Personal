using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization; 

namespace Entidades
{
    [DataContract]
   public class PremioEterno:Premios
    {
        Equipos equipo;

        [DataMember]
        public Equipos Equipo
        {
            get { return equipo; }
            set { equipo = value; }
        }
        
        public PremioEterno(Equipos equipo, int id, string motivo, DateTime fecha, Competidores competidor, Competencias copa)
            :base( id,  motivo,  fecha,  competidor,  copa)
        {
            Equipo = equipo; 
        }

        public PremioEterno() { }
    
    }
}
