using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entidades
{
    [DataContract]
   public class PremioInmunidad:Premios
    {
        Equipos equipo;
        bool usado;

        [DataMember]
        public Equipos Equipo
        {
            get { return equipo; }
            set { equipo = value; }
        }

        [DataMember]
        public bool  Usado
        {
            get { return usado; }
            set { usado = value; }
        }

        public PremioInmunidad (Equipos equipo, bool usado, int id, string motivo, DateTime fecha, Competidores competidor, Competencias copa)
            :base ( id,  motivo,  fecha,  competidor,  copa)
        {
            Equipo = equipo;
            Usado = usado; 
        }

        public PremioInmunidad() { }

    }
}
